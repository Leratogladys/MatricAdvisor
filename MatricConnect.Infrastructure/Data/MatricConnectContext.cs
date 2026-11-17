// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Represents the Entity Framework Core database context
//                   and provides access to Matric Connect database tables.

using MatricConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace MatricConnect.Data
{
    public class MatricConnectContext : DbContext
    {
        public MatricConnectContext(
            DbContextOptions<MatricConnectContext> options)
            : base(options)
        {
            //
            // Name              : MatricConnectContext(
            //                     DbContextOptions<MatricConnectContext> options)
            // Purpose           : Initializes the Matric Connect database
            //                     context using the supplied database options.
            // Re-use            : None
            // Method Parameters : DbContextOptions<MatricConnectContext> options
            //                     - database configuration options used by
            //                       Entity Framework Core
            // Output Type       : None
            //
        } // end method

        public DbSet<Student> Students
        {
            //
            // Name            : property DbSet<Student> Students
            // Purpose         : Provides access to student records in
            //                   the database.
            // Re-use          : None
            // Input Parameter : DbSet<Student> value
            //                   - new student entity set
            // Output Type     : DbSet<Student>
            //                   - student records managed by the context
            //

            get; set;
        } // end property

        public DbSet<University> Universities
        {
            //
            // Name            : property DbSet<University> Universities
            // Purpose         : Provides access to university records
            //                   in the database.
            // Re-use          : None
            // Input Parameter : DbSet<University> value
            //                   - new university entity set
            // Output Type     : DbSet<University>
            //                   - university records managed by the context
            //

            get; set;
        } // end property

        public DbSet<Programme> Programmes
        {
            //
            // Name            : property DbSet<Programme> Programmes
            // Purpose         : Provides access to programme records
            //                   in the database.
            // Re-use          : None
            // Input Parameter : DbSet<Programme> value
            //                   - new programme entity set
            // Output Type     : DbSet<Programme>
            //                   - programme records managed by the context
            //

            get; set;
        } // end property

        public DbSet<TvetCollege> TvetColleges => Set<TvetCollege>();

        public DbSet<ProgrammeReference> ProgrammeReferences => Set<ProgrammeReference>();

        public DbSet<SavedProgrammeReference> SavedProgrammeReferences => Set<SavedProgrammeReference>();

        public DbSet<ProgrammeIntake> ProgrammeIntakes => Set<ProgrammeIntake>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TvetCollege>().HasIndex(college => college.Name).IsUnique();

            modelBuilder.Entity<ProgrammeReference>(reference =>
            {
                reference.ToTable(table => table.HasCheckConstraint("CK_ProgrammeReference_OneInstitution",
                    "(UniversityId IS NULL) <> (TvetCollegeId IS NULL)"));
                reference.HasOne(item => item.University).WithMany().HasForeignKey(item => item.UniversityId);
                reference.HasOne(item => item.TvetCollege).WithMany(college => college.ProgrammeReferences)
                    .HasForeignKey(item => item.TvetCollegeId);
                reference.HasIndex(item => new { item.UniversityId, item.Group, item.Name }).IsUnique();
                reference.HasIndex(item => new { item.TvetCollegeId, item.Group, item.Name }).IsUnique();
            });

            modelBuilder.Entity<SavedProgrammeReference>(saved =>
            {
                saved.HasKey(item => new { item.StudentId, item.ProgrammeReferenceId });
                saved.HasOne<Student>().WithMany().HasForeignKey(item => item.StudentId);
                saved.HasOne(item => item.Reference).WithMany().HasForeignKey(item => item.ProgrammeReferenceId);
            });

            modelBuilder.Entity<ProgrammeIntake>().HasKey(intake => new { intake.ProgrammeId, intake.IntakeYear });
            modelBuilder.Entity<Programme>()
                .HasMany(programme => programme.Intakes)
                .WithOne()
                .HasForeignKey(intake => intake.ProgrammeId);
        } // end method OnModelCreating

        public DbSet<SavedProgramme> SavedProgrammes
        {
            //
            // Name            : property DbSet<SavedProgramme>
            //                   SavedProgrammes
            // Purpose         : Provides access to saved programme records
            //                   in the database.
            // Re-use          : None
            // Input Parameter : DbSet<SavedProgramme> value
            //                   - new saved programme entity set
            // Output Type     : DbSet<SavedProgramme>
            //                   - saved programme records managed
            //                     by the context
            //

            get; set;
        } // end property
    } // end class MatricConnectContext
} // end namespace MatricConnect.Data