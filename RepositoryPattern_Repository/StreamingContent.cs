using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern_Repository
{
    // Enum has to be outside of the class
    public enum GenreType
    {
        /* Enums automatically have numerical values that start with 0. We reassigned the first
        genre type value to 1 here, and each subsequent value is 1 more than the last.*/
        Horror = 1,
        RomCom,
        SciFi,
        Documentary,
        Bromance,
        Drama,
        Action
    }

    //Plain Old C# Object -- POCO
    // Simple single responsibility object that just holds data
    public class StreamingContent
    {
        // Defining what data the POCO will hold
        // Use prop tab tab shortcut to scaffold out properties
        public string Title { get; set; }
        public string Description { get; set; }
        public string MaturityRating { get; set; }
        public double StarRating { get; set; }
        public bool IsFamilyFriendly { get; set; }

        /* Enum is used as a property type here. Property value is named basically the opposite of 
        enum type so it is clearly an instance of the type and not the type itself.*/
        public GenreType TypeOfGenre { get; set; }

        // Empty constructor on one line (it's cleaner that way)
        public StreamingContent() { }

        // ctor tab tab is the code snippet that will create a new constructor based on class name
        // Giving parameters that have the same names as the properties then assigning them to the properties
        public StreamingContent(string title, string description, string maturityRating, double starRating, bool isFamilyFriendly, GenreType genre)
        {
            Title = title;
            Description = description;
            MaturityRating = maturityRating;
            StarRating = starRating;
            IsFamilyFriendly = isFamilyFriendly;
            TypeOfGenre = genre;
        }
        /* Scope - you can't take something that's defined in a method (e.g., title parameter) and 
        reference it outside of its scope (e.g., Title property). However, you can do the opposite.
        You can always go "inward" when referencing things.*/
    }
}
