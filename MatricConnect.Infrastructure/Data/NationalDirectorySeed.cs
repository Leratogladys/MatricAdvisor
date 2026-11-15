// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Adds the public university and TVET college directory without changing existing programme IDs.

using MatricConnect.Models;

namespace MatricConnect.Data
{
    internal static class NationalDirectorySeed
    {
        // Sources and province grouping are recorded in docs/institution-directory.md.
        private sealed record Entry(string Province, string Name, string Website, bool IsUniversityOfTechnology = false);

        private static readonly Entry[] Universities =
        [
            new("Eastern Cape", "Nelson Mandela University", "https://www.mandela.ac.za/"),
            new("Eastern Cape", "Rhodes University", "https://www.ru.ac.za/"),
            new("Eastern Cape", "University of Fort Hare", "https://www.ufh.ac.za/"),
            new("Eastern Cape", "Walter Sisulu University", "https://www.wsu.ac.za/"),
            new("Free State", "Central University of Technology", "https://www.cut.ac.za/", true),
            new("Free State", "University of the Free State", "https://www.ufs.ac.za/"),
            new("Gauteng", "Sefako Makgatho Health Sciences University", "https://www.smu.ac.za/"),
            new("Gauteng", "Tshwane University of Technology", "https://www.tut.ac.za/", true),
            new("Gauteng", "University of Johannesburg", "https://www.uj.ac.za/"),
            new("Gauteng", "University of Pretoria", "https://www.up.ac.za/"),
            new("Gauteng", "University of South Africa", "https://www.unisa.ac.za/"),
            new("Gauteng", "University of the Witwatersrand", "https://www.wits.ac.za/"),
            new("Gauteng", "Vaal University of Technology", "https://www.vut.ac.za/", true),
            new("KwaZulu-Natal", "Durban University of Technology", "https://www.dut.ac.za/", true),
            new("KwaZulu-Natal", "Mangosuthu University of Technology", "https://www.mut.ac.za/", true),
            new("KwaZulu-Natal", "University of KwaZulu-Natal", "https://www.ukzn.ac.za/"),
            new("KwaZulu-Natal", "University of Zululand", "https://www.unizulu.ac.za/"),
            new("Limpopo", "University of Limpopo", "https://www.ul.ac.za/"),
            new("Limpopo", "University of Venda", "https://www.univen.ac.za/"),
            new("Mpumalanga", "University of Mpumalanga", "https://www.ump.ac.za/"),
            new("North West", "North-West University", "https://www.nwu.ac.za/"),
            new("Northern Cape", "Sol Plaatje University", "https://www.spu.ac.za/"),
            new("Western Cape", "Cape Peninsula University of Technology", "https://www.cput.ac.za/", true),
            new("Western Cape", "Stellenbosch University", "https://www.sun.ac.za/"),
            new("Western Cape", "University of Cape Town", "https://www.uct.ac.za/"),
            new("Western Cape", "University of the Western Cape", "https://www.uwc.ac.za/"),
        ];

        private static readonly Entry[] TvetColleges =
        [
            new("Eastern Cape", "Buffalo City TVET College", "https://www.bccollege.co.za/"),
            new("Eastern Cape", "Eastcape Midlands TVET College", "https://www.emcol.co.za/"),
            new("Eastern Cape", "Ikhala TVET College", "https://ikhala.edu.za/"),
            new("Eastern Cape", "Ingwe TVET College", "https://ingwecollege.edu.za/"),
            new("Eastern Cape", "King Hintsa TVET College", "https://www.kinghintsacollege.edu.za/"),
            new("Eastern Cape", "King Sabata Dalindyebo TVET College", "https://ksdcollege.edu.za/"),
            new("Eastern Cape", "Lovedale TVET College", "https://lovedale.edu.za/"),
            new("Eastern Cape", "Port Elizabeth TVET College", "https://www.pecollege.edu.za/"),
            new("Free State", "Flavius Mareka TVET College", "https://flaviusmareka.net/"),
            new("Free State", "Goldfields TVET College", "https://goldfieldstvet.edu.za/"),
            new("Free State", "Maluti TVET College", "https://www.malutitvet.co.za/"),
            new("Free State", "Motheo TVET College", "https://www.motheotvet.co.za/"),
            new("Gauteng", "Central Johannesburg TVET College", "https://cjc.edu.za/"),
            new("Gauteng", "Ekurhuleni East TVET College", "https://www.eec.edu.za/"),
            new("Gauteng", "Ekurhuleni West TVET College", "https://www.ewc.edu.za/"),
            new("Gauteng", "Sedibeng TVET College", "https://www.sedcol.co.za/"),
            new("Gauteng", "South West Gauteng TVET College", "https://www.swgc.co.za/"),
            new("Gauteng", "Tshwane North TVET College", "https://tnc.edu.za/"),
            new("Gauteng", "Tshwane South TVET College", "https://www.tsc.edu.za/"),
            new("Gauteng", "Western TVET College", "https://www.westcol.co.za/"),
            new("KwaZulu-Natal", "Coastal KZN TVET College", "https://www.coastalkzn.co.za/"),
            new("KwaZulu-Natal", "Elangeni TVET College", "https://www.elangeni.edu.za/"),
            new("KwaZulu-Natal", "Esayidi TVET College", "https://esayiditvet.co.za/"),
            new("KwaZulu-Natal", "Majuba TVET College", "https://www.majuba.edu.za/"),
            new("KwaZulu-Natal", "Mnambithi TVET College", "https://www.mnambithicollege.co.za/"),
            new("KwaZulu-Natal", "Mthashana TVET College", "https://www.mthashanacollege.co.za/"),
            new("KwaZulu-Natal", "Thekwini TVET College", "https://www.thekwini.edu.za/"),
            new("KwaZulu-Natal", "Umfolozi TVET College", "https://www.umfolozicollege.co.za/"),
            new("KwaZulu-Natal", "Umgungundlovu TVET College", "https://utvet.co.za/"),
            new("Limpopo", "Capricorn TVET College", "https://www.capricorncollege.edu.za/"),
            new("Limpopo", "Lephalale TVET College", "https://leptvetcol.edu.za/"),
            new("Limpopo", "Letaba TVET College", "https://www.letcol.co.za/"),
            new("Limpopo", "Mopani South East TVET College", "https://www.mopanicollege.edu.za/"),
            new("Limpopo", "Sekhukhune TVET College", "https://www.sekhukhunetvet.edu.za/"),
            new("Limpopo", "Vhembe TVET College", "https://www.vhembecollege.edu.za/"),
            new("Limpopo", "Waterberg TVET College", "https://www.waterbergcollege.co.za/"),
            new("Mpumalanga", "Ehlanzeni TVET College", "https://www.ehlanzenicollege.co.za/"),
            new("Mpumalanga", "Gert Sibande TVET College", "https://gscollege.edu.za/"),
            new("Mpumalanga", "Nkangala TVET College", "https://ntc.edu.za/"),
            new("North West", "ORBIT TVET College", "https://www.orbitcollege.co.za/"),
            new("North West", "Taletso TVET College", "https://taletso.edu.za/"),
            new("North West", "Vuselela TVET College", "https://www.vuselelacollege.co.za/"),
            new("Northern Cape", "Northern Cape Rural TVET College", "https://www.ncrtvet.com/"),
            new("Northern Cape", "Northern Cape Urban TVET College", "https://www.ncutvet.edu.za/"),
            new("Western Cape", "Boland TVET College", "https://www.bolandcollege.com/"),
            new("Western Cape", "College of Cape Town for TVET", "https://www.cct.edu.za/"),
            new("Western Cape", "False Bay TVET College", "https://www.falsebaycollege.co.za/"),
            new("Western Cape", "Northlink TVET College", "https://www.northlink.co.za/"),
            new("Western Cape", "South Cape TVET College", "https://sccollege.co.za/"),
            new("Western Cape", "West Coast TVET College", "https://www.westcoastcollege.co.za/"),
        ];

        public static void AddMissingInstitutions(MatricConnectContext context)
        {
            var universityNames = context.Universities.Select(university => university.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
            foreach (var entry in Universities.Where(entry => universityNames.Add(entry.Name)))
            {
                context.Universities.Add(new University
                {
                    Name = entry.Name,
                    Province = entry.Province,
                    Website = entry.Website,
                    IsUniversityOfTechnology = entry.IsUniversityOfTechnology
                });
            }

            var collegeNames = context.TvetColleges.Select(college => college.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
            foreach (var entry in TvetColleges.Where(entry => collegeNames.Add(entry.Name)))
            {
                context.TvetColleges.Add(new TvetCollege
                {
                    Name = entry.Name,
                    Province = entry.Province,
                    Website = entry.Website
                });
            }

            context.SaveChanges();
        }
    }
}
