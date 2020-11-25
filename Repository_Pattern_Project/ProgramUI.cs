using RepositoryPattern_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Pattern_Project
{
    /* The single responsibility of ProgramUI is to interact with the user and make sure the
    user's input gets to the correct spot.*/
    class ProgramUI
    {
        /* Creating a StreamingContentRepository field that is accessible anywhere and will exist
        for the entire existence of the ProgramUI class until the application is closed. Content can
        be added and deleted from this field and it will persist.*/
        private StreamingContentRepository _contentRepo = new StreamingContentRepository();

        /*Method that runs/starts the application. We make it public so this class can be called and 
        ran. We split Run and Menu into two methods so we can set something up in the Run method
        before the Menu fires off.*/
        public void Run()
        {
            // The SeedContentList gives us content to work with before the menu appears.
            SeedContentList();
            Menu();
        }

        //Menu. It's private so someone can't skip the run method.
        private void Menu()
        {
            /* We want the Menu method to continue running until a specific condition is met.*/
            bool keepRunning = true;
            while (keepRunning)
            {

                //Display our options to the user
                /* Using numbers to make it easy for the user to select items. Anything our 
                repository does should be enabled from the UI.*/
                // \n will concatenate strings with a line break within quotes
                Console.WriteLine("Select a menu option:\n" +
                    "1. Create New Content\n" +
                    "2. View All Content\n" +
                    "3. View Content By Title\n" +
                    "4. Update Existing Content\n" +
                    "5. Delete Existing Content\n" +
                    "6. Exit");

                //Get the user's input. Whatever Console.Readline(); returns will save as input.
                string input = Console.ReadLine();

                /*Evaluate the user's input and act accordingly. The user has 6 options, so it's
                best to use a switch case here. It's evaluating their input (number 1-6), not our
                Console.Writeline();*/
                switch (input)
                {
                    // Calling each method to run them for their specific user input case.
                    case "1":
                        //Create New Content
                        CreateNewContent();
                        break;
                    case "2":
                        //View All Content
                        DisplayAllContent();
                        break;
                    case "3":
                        //View Content By Title
                        DisplayContentByTitle();
                        break;
                    case "4":
                        //Update Existing Content
                        UpdateExistingContent();
                        break;
                    case "5":
                        //Delete Existing Content
                        DeleteExistingContent();
                        break;
                    case "6":
                        //Exit
                        Console.WriteLine("Goodbye!");
                        // Breaks the while loop and exits the application.
                        keepRunning = false;
                        break;
                    //Need a default in case they enter an input that would return a null value
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;
                }
                /* Prompts user to do something. Once they press a key, the prompt clears and the
                console returns to the original menu without creating new menus everytime an input
                is given.*/
                Console.WriteLine("Please press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        /* All the methods below are void because we want them to do something, but not return
        anything to the menu. They just get, build, show, etc.*/

        //Create new StreamingContent
        private void CreateNewContent()
        {
            /* This Console.Clear method will clear the menu when the user selects the
            CreateNewContent method.*/
            Console.Clear();
            /* Had to right click on the 06_RepositoryPattern_Console and click Add then Reference
            then click 06_RepositoryPattern_Repository to create a reference that the 
            06_RepositoryPattern_Console can use within it. You write StreamingContent then Ctrl + .
            and select using_06_RepositoryPattern_Repository; This will create a using statement at
            the very top of the program and allow us to reference the StreamingContent objects from 
            another assembly.*/
            StreamingContent newContent = new StreamingContent();

            //Title
            // Prompting the user for what they want
            Console.WriteLine("Enter the title for the content:");
            /* We declared newContent as a new Streaming Content object so we could call its 
            properties right away. The Console.Readline() is a method that returns a string,
            newContent has a property called Title that is a string. Instead of capturing the input
            and setting it equal to a variable, we're able to (in 1 line of code) use the newContent
            object and access the Title property and assign a value to that property of whatever the 
            Console.ReadLine() method returns based on user input.*/
            newContent.Title = Console.ReadLine();

            //Description
            Console.WriteLine("Enter the desciption for the content:");
            newContent.Description = Console.ReadLine();

            //Maturity Rating
            Console.WriteLine("Enter the rating for the content (G, PG, PG-13, etc):");
            newContent.MaturityRating = Console.ReadLine();

            //Star Rating
            Console.WriteLine("Enter the star count for the content (5.8, 10, 1.5, etc):");
            /* Getting Star Rating is a little more difficult because it is set as a double. For this
            one we need to declare a new string variable and set it equal to Console.Readline(). Then
            we use the newContent object and access the StarRating property and set that equal to the
            starsAsString variable, but we parse (change) it to a double. Therefore, starsAsString
            and newContent.StarRating are now both doubles.*/
            string starsAsString = Console.ReadLine();
            newContent.StarRating = double.Parse(starsAsString);

            //Is Family Friendly
            Console.WriteLine("Is this content family friendly? (y/n)");
            // The ToLower method converts user input to all lower-cased so it's not case sensitive.
            string familyFriendlyString = Console.ReadLine().ToLower();

            /* The IsFamilyFriendly property is a boolean, so we have to evaluate the user's input 
            with a loop.*/
            if (familyFriendlyString == "y")
            {
                newContent.IsFamilyFriendly = true;
            }
            else
            {
                newContent.IsFamilyFriendly = false;
            }

            //GenreType
            Console.WriteLine("Enter the Genre Number:\n" +
                "1. Horror\n" +
                "2. RomCom\n" +
                "3. SciFi\n" +
                "4. Documentary\n" +
                "5. Bromance\n" +
                "6. Drama\n" +
                "7. Action");

            string genreAsString = Console.ReadLine();
            // Parsing genreAsString into a new integer variable
            int genreAsInt = int.Parse(genreAsString);
            /* This is called casting. Casting is taking a variable and plugging in a type right in
            front of it. In this case, the newContent object is accessing the TypeofGenre property, 
            which is an enum, so we can cast the enum GenreType before the genreAsInt variable and
            explicitly convert it to an enum type.*/
            newContent.TypeOfGenre = (GenreType)genreAsInt;

            _contentRepo.AddContentToList(newContent);
        }

        //View Current StreamingContent that is saved
        private void DisplayAllContent()
        {
            Console.Clear();

            /* First thing we need to do is get the content list. So we need a List of
            StreamingContent and we'll call it listOfContent. We set that equal to the actual 
            existing list inside our repository, which is _contentRepo, and use the method we built
            to pull the _contentRepo list. We'll save it as listOfContent.*/
            List<StreamingContent> listOfContent = _contentRepo.GetContentList();

            /* We want to show everything on the listOfContent now, so we'll build a foreach loop
            to do that. For each StreamingContent object, we'll call the variable content in the
            listOfContent, we want to show something on the console.*/
            foreach (StreamingContent content in listOfContent)
            {
                /* Using string interpolation to pull the Title and Description for each content
                item in the listOfContent.*/
                Console.WriteLine($"Title: {content.Title}\n" +
                    $" Description: {content.Description}");
            }
        }

        //View Existing Content By Title (gives more details about an individual title)
        private void DisplayContentByTitle()
        {
            Console.Clear();

            //Prompt the user to give me a title
            Console.WriteLine("Enter the title of the content you'd like to see");

            //Get the user's input
            string title = Console.ReadLine();

            //Find the content by that title
            StreamingContent content = _contentRepo.GetContentByTitle(title);

            //Display said content if it isn't null
            if (content != null)
            {
                Console.WriteLine($"Title: {content.Title}\n" +
                $" Description: {content.Description}\n" +
                $"Maturity Rating: {content.MaturityRating}\n" +
                $"Stars: {content.StarRating}\n" +
                $"Is Family Friendly: {content.IsFamilyFriendly}\n" +
                $"Genre: {content.TypeOfGenre}");
            }
            else
            {
                Console.WriteLine("No content by that title.");
            }
        }

        //Update Existing Content
        private void UpdateExistingContent()
        {
            //Display all content
            DisplayAllContent();

            //Ask for the title of the content to update
            Console.WriteLine("Enter the title of the content you'd like to update:");

            //Get that title
            string oldTitle = Console.ReadLine();

            /*We will build a new object. The code below the newContent StreamingContent object is
            basically all borrowed from our CreateNewContent method except the AddContentToList method
            at the end.*/
            StreamingContent newContent = new StreamingContent();

            //Title
            Console.WriteLine("Enter the title for the content:");
            newContent.Title = Console.ReadLine();

            //Description
            Console.WriteLine("Enter the desciption for the content:");
            newContent.Description = Console.ReadLine();

            //Maturity Rating
            Console.WriteLine("Enter the rating for the content (G, PG, PG-13, etc):");
            newContent.MaturityRating = Console.ReadLine();

            //Star Rating
            Console.WriteLine("Enter the star count for the content (5.8, 10, 1.5, etc):");
            string starsAsString = Console.ReadLine();
            newContent.StarRating = double.Parse(starsAsString);

            //Is Family Friendly
            Console.WriteLine("Is this content family friendly? (y/n)");
            string familyFriendlyString = Console.ReadLine().ToLower();

            if (familyFriendlyString == "y")
            {
                newContent.IsFamilyFriendly = true;
            }
            else
            {
                newContent.IsFamilyFriendly = false;
            }

            //GenreType
            Console.WriteLine("Enter the Genre Number:\n" +
                "1. Horror\n" +
                "2. RomCom\n" +
                "3. SciFi\n" +
                "4. Documentary\n" +
                "5. Bromance\n" +
                "6. Drama\n" +
                "7. Action");

            string genreAsString = Console.ReadLine();
            int genreAsInt = int.Parse(genreAsString);
            newContent.TypeOfGenre = (GenreType)genreAsInt;

            //Verify the update worked
            bool wasUpdated = _contentRepo.UpdateExistingContent(oldTitle, newContent);

            if (wasUpdated)
            {
                Console.WriteLine("Content successfully updated!");
            }
            else
            {
                Console.WriteLine("Could not update content");
            }
        }

        //Delete Existing Content
        private void DeleteExistingContent()
        {
            DisplayAllContent();

            //Get the title they want to remove
            /* The \n creates a line between the content properties in the console and the 
            Console.WriteLine method below.*/
            Console.WriteLine("\nEnter the title of the content you'd like to remove:");

            string input = Console.ReadLine();

            //Call the delete method
            /* The RemoveContentFromList method returns a boolean, so we need to set our variable
            type to be a boolean. We'll name it wasDeleted.*/
            bool wasDeleted = _contentRepo.RemoveContentFromList(input);

            /* If the content was deleted, say so. In other words, if the user input existed in the
            _contentRepo field and the RemoveContentFromList method deleted it, then wasDeleted will
            return true.*/
            if (wasDeleted)
            {
                Console.WriteLine("The content was successfully deleted.");
            }
            //Otherwise state it could not be deleted
            else
            {
                Console.WriteLine("The content could not be deleted");
            }
        }

        /*Seed method. This method seeds the database upon running. This way we don't have to create
        something before you manipulate it everytime you run the application.*/
        private void SeedContentList()
        {
            // Created a few StreamingContent objects to add to the _contentRepo list.
            StreamingContent sharknado = new StreamingContent("Sharknado", "Tornados, but with sharks.", "TV-14", 3.3, true, GenreType.Action);
            StreamingContent theRoom = new StreamingContent("The Room", "Banker's life gets turned upside down.", "R", 3.7, false, GenreType.Drama);
            StreamingContent rubber = new StreamingContent("Rubber", "Car tire comes to life and goes on killing spree.", "R", 5.8, false, GenreType.Documentary);

            _contentRepo.AddContentToList(sharknado);
            _contentRepo.AddContentToList(theRoom);
            _contentRepo.AddContentToList(rubber);
        }
    }
}
