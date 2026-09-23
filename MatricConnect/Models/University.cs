// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Represents a university and stores its identifying
//                   information and associated programmes.

namespace MatricConnect.Models
{
    public class University
    {
        public int Id
        {
            //
            // Name            : property int Id
            // Purpose         : Provides access to the unique university ID.
            // Re-use          : None
            // Input Parameter : int value
            //                   - new value for the university ID
            // Output Type     : int
            //                   - value stored as the university ID
            //

            get; set;
        } // end property

        public string Name
        {
            //
            // Name            : property string Name
            // Purpose         : Provides access to the university name.
            // Re-use          : None
            // Input Parameter : string value
            //                   - new value for the university name
            // Output Type     : string
            //                   - value stored as the university name
            //

            get; set; } = string.Empty; // end property

        public string Province
        {
            //
            // Name            : property string Province
            // Purpose         : Provides access to the province in which
            //                   the university is located.
            // Re-use          : None
            // Input Parameter : string value
            //                   - new value for the university province
            // Output Type     : string
            //                   - value stored as the university province
            //

            get; set; } = string.Empty; // end property

        public string Website
        {
            //
            // Name            : property string Website
            // Purpose         : Provides access to the university website.
            // Re-use          : None
            // Input Parameter : string value
            //                   - new value for the university website
            // Output Type     : string
            //                   - value stored as the university website
            //

            get; set; } = string.Empty; // end property

        public ICollection<Programme> Programmes
        {
            //
            // Name            : property ICollection<Programme> Programmes
            // Purpose         : Provides access to programmes offered by
            //                   the university.
            // Re-use          : None
            // Input Parameter : ICollection<Programme> value
            //                   - new collection of university programmes
            // Output Type     : ICollection<Programme>
            //                   - programmes associated with the university
            //

            get; set; } = []; // end property
    } // end class University
} // end namespace MatricConnect.Models