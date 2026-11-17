namespace MatricConnect.Data;

// The existing 2027 faculty assignments. Kept separate from intake and eligibility data.
internal static class ReviewedFacultyAssignments
{
    public const string Sql = """
        WITH assignments(UniversityName, Faculty, ProgrammeCode) AS (
            VALUES
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA00'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA03'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA08'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA09'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA10'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA13'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA15'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA20'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA21'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA22'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA23'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA24'),
                ('University of the Witwatersrand', 'Faculty of Science', 'SBA25'),
                ('Central University of Technology', 'Faculty of Engineering, Built Environment and Information Technology', 'HC_ITC'),
                ('Central University of Technology', 'Faculty of Engineering, Built Environment and Information Technology', 'DP_CMN'),
                ('Central University of Technology', 'Faculty of Engineering, Built Environment and Information Technology', 'DP_ITC'),
                ('Nelson Mandela University', 'Faculty of Engineering, the Built Environment and Technology', '72001'),
                ('Nelson Mandela University', 'Faculty of Engineering, the Built Environment and Technology', '7224'),
                ('Nelson Mandela University', 'Faculty of Engineering, the Built Environment and Technology', '7228'),
                ('Nelson Mandela University', 'Faculty of Engineering, the Built Environment and Technology', '70003'),
                ('Nelson Mandela University', 'Faculty of Science', '20053'),
                ('University of Johannesburg', 'Faculty of Science', 'B2I01Q'),
                ('University of Johannesburg', 'Faculty of Science', 'B2I02Q'),
                ('University of Johannesburg', 'Faculty of Science', 'B2I04Q'),
                ('University of Pretoria', 'Faculty of Engineering, Built Environment and Information Technology', '12134002'),
                ('University of Pretoria', 'Faculty of Engineering, Built Environment and Information Technology', '12133214'),
                ('University of the Free State', 'Faculty of Natural and Agricultural Sciences', 'B4361'),
                ('University of the Free State', 'Faculty of Natural and Agricultural Sciences', 'B4363')
        ),
        matched AS (
            SELECT i.ProgrammeId AS Id, a.Faculty
            FROM assignments a
            JOIN Universities u ON u.Name = a.UniversityName
            JOIN Programmes p ON p.UniversityId = u.Id
            JOIN ProgrammeIntakes i ON i.ProgrammeId = p.Id AND i.IntakeYear = 2027
                AND i.OfficialProgrammeCode = a.ProgrammeCode
        )
        UPDATE Programmes
        SET Faculty = (SELECT matched.Faculty FROM matched WHERE matched.Id = Programmes.Id)
        WHERE Faculty IS NULL AND Id IN (SELECT Id FROM matched);
        """;
}
