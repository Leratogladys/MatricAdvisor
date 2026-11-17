using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatricConnect.Migrations
{
    /// <inheritdoc />
    public partial class ReviewUmpAgricultureReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE ProgrammeReferences
                SET SourceUrl = 'https://www.ump.ac.za/Study-with-us/Faculties-and-Schools/Faculty-of-Agriculture-and-Natural-Sciences/School-of-Agricultural-Sciences/Bachelor-of-Agriculture-in-Agricultural-Extension-.aspx',
                    Availability = 'UMP publishes requirements for this programme without identifying an intake year. Confirm which intake the requirements apply to with the university.',
                    EntryRequirements = 'NSC route: UMP lists an NSC bachelor’s pass, APS 26 with Mathematics or APS 28 with Mathematical Literacy, and English at Level 4. Both UMP publications list Mathematics at Level 4 for the Mathematics route.
                Check the full subject combination with UMP: its admissions guide gives Level 6 for Mathematical Literacy, while its programme page does not specify a level for that route. The sources also give different science subject requirements.
                The programme page mentions recognition of prior learning and marks the NC(V) route as not applicable.',
                    VerifiedOn = '2026-11-17'
                WHERE UniversityId IN (
                    SELECT Id FROM Universities
                    WHERE Name = 'University of Mpumalanga' AND Province = 'Mpumalanga'
                )
                  AND Name = 'Bachelor of Agriculture in Agricultural Extension and Rural Resource Management'
                  AND "Group" = 'Faculty of Agriculture and Natural Sciences'
                  AND TvetCollegeId IS NULL
                  AND IntakeYear IS NULL
                  AND SourceUrl IS 'https://www.ump.ac.za/Study-with-us/Faculties-and-Schools/Faculty-of-Agriculture-and-Natural-Sciences/School-of-Agricultural-Sciences.aspx'
                  AND Availability IS 'Listed by UMP. Check the university’s application page and confirm availability and requirements for this programme.'
                  AND EntryRequirements IS NULL
                  AND VerifiedOn IS '2026-11-16';
                """);
            migrationBuilder.Sql("""
                UPDATE ProgrammeReferences
                SET SourceUrl = 'https://www.ump.ac.za/Study-with-us/Faculties-and-Schools/Faculty-of-Agriculture-and-Natural-Sciences/School-of-Agricultural-Sciences/Diploma-in-Agriculture-in-Plant-Production',
                    Availability = 'UMP publishes requirements for this programme without identifying an intake year. Confirm which intake the requirements apply to with the university.',
                    EntryRequirements = 'NSC route: Meet the general diploma admission requirements. UMP lists APS 23 with Mathematics or APS 24 with Mathematical Literacy.
                NSC subjects: English as Home Language or First Additional Language at Level 4; Mathematics at Level 3 or Mathematical Literacy at Level 4; and at least Level 4 in one of Agriculture, Geography or Life Sciences.
                NC(V) route: Complete NC(V) Level 4 in Primary Agriculture, with at least 50% in English (Home Language or First Additional Language), and 40% in Mathematics or 70% in Mathematical Literacy.',
                    Campuses = 'Mbombela (first two years)',
                    VerifiedOn = '2026-11-17'
                WHERE UniversityId IN (
                    SELECT Id FROM Universities
                    WHERE Name = 'University of Mpumalanga' AND Province = 'Mpumalanga'
                )
                  AND Name = 'Diploma in Agriculture in Plant Production'
                  AND "Group" = 'Faculty of Agriculture and Natural Sciences'
                  AND TvetCollegeId IS NULL
                  AND IntakeYear IS NULL
                  AND SourceUrl IS 'https://www.ump.ac.za/Study-with-us/Faculties-and-Schools/Faculty-of-Agriculture-and-Natural-Sciences/School-of-Agricultural-Sciences.aspx'
                  AND Availability IS 'Listed by UMP. Check the university’s application page and confirm availability and requirements for this programme.'
                  AND EntryRequirements IS NULL
                  AND Campuses IS NULL
                  AND VerifiedOn IS '2026-11-16';
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE ProgrammeReferences
                SET SourceUrl = 'https://www.ump.ac.za/Study-with-us/Faculties-and-Schools/Faculty-of-Agriculture-and-Natural-Sciences/School-of-Agricultural-Sciences.aspx',
                    Availability = 'Listed by UMP. Check the university’s application page and confirm availability and requirements for this programme.',
                    EntryRequirements = NULL,
                    VerifiedOn = '2026-11-16'
                WHERE UniversityId IN (
                    SELECT Id FROM Universities
                    WHERE Name = 'University of Mpumalanga' AND Province = 'Mpumalanga'
                )
                  AND Name = 'Bachelor of Agriculture in Agricultural Extension and Rural Resource Management'
                  AND "Group" = 'Faculty of Agriculture and Natural Sciences'
                  AND TvetCollegeId IS NULL
                  AND IntakeYear IS NULL
                  AND SourceUrl IS 'https://www.ump.ac.za/Study-with-us/Faculties-and-Schools/Faculty-of-Agriculture-and-Natural-Sciences/School-of-Agricultural-Sciences/Bachelor-of-Agriculture-in-Agricultural-Extension-.aspx'
                  AND Availability IS 'UMP publishes requirements for this programme without identifying an intake year. Confirm which intake the requirements apply to with the university.'
                  AND EntryRequirements IS 'NSC route: UMP lists an NSC bachelor’s pass, APS 26 with Mathematics or APS 28 with Mathematical Literacy, and English at Level 4. Both UMP publications list Mathematics at Level 4 for the Mathematics route.
                Check the full subject combination with UMP: its admissions guide gives Level 6 for Mathematical Literacy, while its programme page does not specify a level for that route. The sources also give different science subject requirements.
                The programme page mentions recognition of prior learning and marks the NC(V) route as not applicable.'
                  AND VerifiedOn IS '2026-11-17';
                """);
            migrationBuilder.Sql("""
                UPDATE ProgrammeReferences
                SET SourceUrl = 'https://www.ump.ac.za/Study-with-us/Faculties-and-Schools/Faculty-of-Agriculture-and-Natural-Sciences/School-of-Agricultural-Sciences.aspx',
                    Availability = 'Listed by UMP. Check the university’s application page and confirm availability and requirements for this programme.',
                    EntryRequirements = NULL,
                    Campuses = NULL,
                    VerifiedOn = '2026-11-16'
                WHERE UniversityId IN (
                    SELECT Id FROM Universities
                    WHERE Name = 'University of Mpumalanga' AND Province = 'Mpumalanga'
                )
                  AND Name = 'Diploma in Agriculture in Plant Production'
                  AND "Group" = 'Faculty of Agriculture and Natural Sciences'
                  AND TvetCollegeId IS NULL
                  AND IntakeYear IS NULL
                  AND SourceUrl IS 'https://www.ump.ac.za/Study-with-us/Faculties-and-Schools/Faculty-of-Agriculture-and-Natural-Sciences/School-of-Agricultural-Sciences/Diploma-in-Agriculture-in-Plant-Production'
                  AND Availability IS 'UMP publishes requirements for this programme without identifying an intake year. Confirm which intake the requirements apply to with the university.'
                  AND EntryRequirements IS 'NSC route: Meet the general diploma admission requirements. UMP lists APS 23 with Mathematics or APS 24 with Mathematical Literacy.
                NSC subjects: English as Home Language or First Additional Language at Level 4; Mathematics at Level 3 or Mathematical Literacy at Level 4; and at least Level 4 in one of Agriculture, Geography or Life Sciences.
                NC(V) route: Complete NC(V) Level 4 in Primary Agriculture, with at least 50% in English (Home Language or First Additional Language), and 40% in Mathematics or 70% in Mathematical Literacy.'
                  AND Campuses IS 'Mbombela (first two years)'
                  AND VerifiedOn IS '2026-11-17';
                """);
        }
    }
}
