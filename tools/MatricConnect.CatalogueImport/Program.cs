using System.Text.Json;
using System.Text.Json.Serialization;
using MatricConnect.CatalogueImport;
using MatricConnect.Data;
using MatricConnect.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

const string usage = "Usage: --database <MatricConnect.db> --batch <reviewed.json> [--apply] or --check-rules";
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

if (args is not ["--database", _, "--batch", _] &&
    args is not ["--database", _, "--batch", _, "--apply"])
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
    var apply = args.Length == 5;
    if (!File.Exists(database))
        throw new InvalidDataException("Database not found. Use an explicit path to an initialized Matric Connect database.");

    using var batchFile = File.OpenRead(batchPath);
    using var json = JsonDocument.Parse(batchFile);
    RejectDuplicateFields(json.RootElement);
    var batch = json.RootElement.Deserialize<CatalogueBatch>(new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
    }) ?? throw new InvalidDataException("Expected a batch object.");

    DateOnly today = DateOnly.FromDateTime(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(2)).DateTime);
    var errors = batch.Validate(today).ToArray();
    if (errors.Length != 0)
        throw new InvalidDataException(string.Join(Environment.NewLine, errors));

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
