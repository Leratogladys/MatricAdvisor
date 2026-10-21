// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Represents a university programme and stores its
//                   admission requirements and application information.

namespace MatricConnect.Models
{
    public class Programme
    {
        public int Id
        {
            //
            // Name            : property int Id
            // Purpose         : Provides access to the unique programme ID.
            // Re-use          : None
            // Input Parameter : int value
            //                   - new value for the programme ID
            // Output Type     : int
            //                   - value stored as the programme ID
            //

            get; set;
        } // end property

        public string Name
        {
            //
            // Name            : property string Name
            // Purpose         : Provides access to the programme name.
            // Re-use          : None
            // Input Parameter : string value
            //                   - new value for the programme name
            // Output Type     : string
            //                   - value stored as the programme name
            //

            get; set;
        } = string.Empty; // end property

        public string FieldOfStudy
        {
            //
            // Name            : property string FieldOfStudy
            // Purpose         : Provides access to the programme's
            //                   field of study.
            // Re-use          : None
            // Input Parameter : string value
            //                   - new value for the field of study
            // Output Type     : string
            //                   - value stored as the field of study
            //

            get; set;
        } = string.Empty; // end property

        public int RequiredAPS
        {
            //
            // Name            : property int RequiredAPS
            // Purpose         : Provides access to the minimum APS required
            //                   for admission to the programme.
            // Re-use          : None
            // Input Parameter : int value
            //                   - new minimum APS requirement
            // Output Type     : int
            //                   - value stored as the required APS
            //

            get; set;
        } // end property

        public decimal RequiredMathematics
        {
            //
            // Name            : property decimal RequiredMathematics
            // Purpose         : Provides access to the minimum Mathematics
            //                   mark required by the programme.
            // Re-use          : None
            // Input Parameter : decimal value
            //                   - new Mathematics requirement
            // Output Type     : decimal
            //                   - value stored as the required
            //                     Mathematics mark
            //

            get; set;
        } // end property

        public decimal? RequiredPhysicalScience
        {
            //
            // Name            : property decimal? RequiredPhysicalScience
            // Purpose         : Provides access to the optional minimum
            //                   Physical Science requirement.
            // Re-use          : None
            // Input Parameter : decimal? value
            //                   - new Physical Science requirement or null
            //                     when Physical Science is not required
            // Output Type     : decimal?
            //                   - stored Physical Science requirement or null
            //

            get; set;
        } // end property

        public DateTime ApplicationDeadline
        {
            //
            // Name            : property DateTime ApplicationDeadline
            // Purpose         : Provides access to the programme
            //                   application deadline.
            // Re-use          : None
            // Input Parameter : DateTime value
            //                   - new application deadline
            // Output Type     : DateTime
            //                   - value stored as the application deadline
            //

            get; set;
        } // end property

        public int UniversityId
        {
            //
            // Name            : property int UniversityId
            // Purpose         : Provides access to the ID of the university
            //                   that offers the programme.
            // Re-use          : None
            // Input Parameter : int value
            //                   - new university ID
            // Output Type     : int
            //                   - value stored as the university ID
            //

            get; set;
        } // end property

        public University University
        {
            //
            // Name            : property University University
            // Purpose         : Provides access to the university associated
            //                   with the programme.
            // Re-use          : None
            // Input Parameter : University value
            //                   - new university associated with the programme
            // Output Type     : University
            //                   - university associated with the programme
            //

            get; set; } = null!; // end property

        public ICollection<SavedProgramme> SavedProgrammes
        {
            //
            // Name            : property ICollection<SavedProgramme>
            //                   SavedProgrammes
            // Purpose         : Provides access to saved programme records
            //                   associated with this programme.
            // Re-use          : None
            // Input Parameter : ICollection<SavedProgramme> value
            //                   - new collection of saved programme records
            // Output Type     : ICollection<SavedProgramme>
            //                   - saved programme records associated with
            //                     this programme
            //

            get; set; } = new List<SavedProgramme>(); // end property
    } // end class Programme
} // end namespace MatricConnect.Models