using System.Text.Json;
using System.Text.Json.Serialization;
using MatricConnect.CatalogueImport;
using MatricConnect.Data;
using MatricConnect.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

const string usage = "Usage: --database <MatricConnect.db> (--batch|--references) <reviewed.json> [--apply] [--refresh (references only)] or --check-rules";
if (args is ["--help"] or ["-h"])
{
    Console.WriteLine(usage);
    return 0;
}

if (args is ["--check-rules"])
{
    if (ProgrammeComparisonRules.ConfigurationError is { } error)
    {
        Console.Error.WriteLine($"Invalid comparison rules: {error}");
        return 1;
    }

    Console.WriteLine("Comparison rules are valid.");
    return 0;
}

if (args.Length < 4 || args[0] != "--database" || args[2] is not ("--batch" or "--references") ||
    args.Skip(4).Any(flag => flag != "--apply" && (flag != "--refresh" || args[2] != "--references")) ||
    args.Skip(4).Distinct().Count() != args.Length - 4)
{
    Console.Error.WriteLine(usage);
    return 2;
}

try
{
    if (ProgrammeComparisonRules.ConfigurationError is { } configurationError)
        throw new InvalidDataException($"Comparison rules are invalid: {configurationError}");

    var database = Path.GetFullPath(args[1]);
    var batchPath = Path.GetFullPath(args[3]);
    var apply = args.Skip(4).Contains("--apply");
    if (!File.Exists(database))
        throw new InvalidDataException("Database not found. Use an explicit path to an initialized Matric Connect database.");

    using var batchFile = File.OpenRead(batchPath);
    using var json = JsonDocument.Parse(batchFile);
    RejectDuplicateFields(json.RootElement);
    var jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
    };
    DateOnly today = DateOnly.FromDateTime(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(2)).DateTime);

    var connection = new SqliteConnectionStringBuilder
    {
        DataSource = database,
        Mode = apply ? SqliteOpenMode.ReadWrite : SqliteOpenMode.ReadOnly,
        ForeignKeys = true
    };
    using var context = new MatricConnectContext(new DbContextOptionsBuilder<MatricConnectContext>()
        .UseSqlite(connection.ToString()).Options);
    using var transaction = apply ? context.Database.BeginTransaction() : null;
    if (context.Database.GetPendingMigrations().Any())
        throw new InvalidDataException("This database is missing Matric Connect migrations. Update a test copy through the normal app startup first.");

    if (args[2] == "--references")
    {
        var references = json.RootElement.Deserialize<ReferenceBatch>(jsonOptions)
            ?? throw new InvalidDataException("Expected a reference batch object.");
        var (additions, updates, unchanged) = references.Preview(context, today, args.Skip(4).Contains("--refresh"));
        Console.WriteLine($"{(apply ? "Apply reviewed references" : "Read-only preview")}: {references.Province}");
        Console.WriteLine($"Reviewed by {references.ReviewedBy}; checked {references.VerifiedOn:yyyy-MM-dd}.");
        foreach (var group in additions.GroupBy(item => (item.UniversityId, item.TvetCollegeId)))
        {
            var institutionName = group.Key.UniversityId is int id
                ? context.Universities.Find(id)!.Name : context.TvetColleges.Find(group.Key.TvetCollegeId)!.Name;
            Console.WriteLine($"{institutionName}: {group.Count()} to add");
            foreach (var item in group)
                Console.WriteLine($"  {item.Group}: {item.Name}");
        }

        foreach (var item in updates)
            Console.WriteLine($"  Update {item.Group}: {item.Name}");

        if (apply)
        {
            context.ProgrammeReferences.AddRange(additions);
            context.ProgrammeReferences.UpdateRange(updates);
            context.SaveChanges();
            transaction!.Commit();
        }

        var updateSummary = updates.Count == 0 ? "" : $"{updates.Count} {(apply ? "updated" : "to update")}; ";
        Console.WriteLine($"{additions.Count} {(apply ? "added" : "to add")}; {updateSummary}{unchanged} unchanged.");
        if (!apply)
            Console.WriteLine("No data was written.");
        return 0;
    }

    var batch = json.RootElement.Deserialize<CatalogueBatch>(jsonOptions)
        ?? throw new InvalidDataException("Expected a batch object.");
    var errors = batch.Validate(today).ToArray();
    if (errors.Length != 0)
        throw new InvalidDataException(string.Join(Environment.NewLine, errors));

    Console.WriteLine($"{(apply ? "Apply reviewed batch" : "Read-only preview")}: {batch.University}, {batch.IntakeYear}");
    Console.WriteLine($"Database: {database}");
    Console.WriteLine($"Reviewed by {batch.ReviewedBy}; checked {batch.VerifiedOn:yyyy-MM-dd}.");

    var plan = CatalogueImportPlan.Build(context, batch);
    if (plan.Errors.Count > 0)
        throw new InvalidDataException(string.Join(Environment.NewLine, plan.Errors));

    foreach (var entry in plan.Unchanged)
        Console.WriteLine($"UNCHANGED  {entry.Code}  {entry.Name}");
    foreach (var programme in plan.Additions)
        Console.WriteLine($"ADD        {programme.Intakes.Single().OfficialProgrammeCode}  {programme.Name} (requirements only)");

    if (apply)
    {
        context.Programmes.AddRange(plan.Additions);
        context.SaveChanges();
        transaction!.Commit();
    }

    Console.WriteLine($"{plan.Additions.Count} {(apply ? "added" : "to add")}; {plan.Unchanged.Count} unchanged.");
    if (!apply)
        Console.WriteLine("No data was written.");

    return 0;
}
catch (Exception error) when (error is IOException or UnauthorizedAccessException or ArgumentException or
    InvalidDataException or JsonException or SqliteException or DbUpdateException)
{
    Console.Error.WriteLine($"Batch rejected. No entries were added.{Environment.NewLine}{error.Message}");
    return 1;
}

static void RejectDuplicateFields(JsonElement value)
{
    if (value.ValueKind == JsonValueKind.Object)
    {
        var fields = new HashSet<string>(StringComparer.Ordinal);
        foreach (var field in value.EnumerateObject())
        {
            if (!fields.Add(field.Name))
                throw new InvalidDataException($"Duplicate JSON field: {field.Name}.");
            RejectDuplicateFields(field.Value);
        }
    }
    else if (value.ValueKind == JsonValueKind.Array)
    {
        foreach (var item in value.EnumerateArray())
            RejectDuplicateFields(item);
    }
}
