using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern_Repository
{
    /* We need to be able to store the streaming content somewhere. This class is the manipulator
    and holder of the data.*/
    public class StreamingContentRepository
    {
        /* This is a field. A field is a class level variable, like a property. We're not creating a
        variable that defines the class. Rather, we're making a variable in the class that can
        be used everywhere.*/
        private List<StreamingContent> _listOfContent = new List<StreamingContent>();

        /* Methods should have a single responsibility principle (only do one thing). For example,
        our Create and Read methods should not be combined.*/

        //Create
        // Method signature // parameter type of Streaming Content object then name (content)
        public void AddContentToList(StreamingContent content)
        {
            // Calling list object that holds StreamingContent using the dot operator and Add method
            // We gave our field an underscore and camel case to denote it as a field
            _listOfContent.Add(content);
        }

        //Read
        /* Our list is private, so we need to build a bridge to get to it. A public method is
        accessible from outside. We don't need parameters because there's only one list to get.
        Now you can access the _listOfContent, but ONLY when you use the GetContentList method.*/
        public List<StreamingContent> GetContentList()
        {
            return _listOfContent;
        }

        //Update
        /* Need to be given what we want our new content to look like, but also need to know what 
        that content was before.*/
        // original content    // brand new StreamingContent object
        public bool UpdateExistingContent(string originalTitle, StreamingContent newContent)
        {
            //Find the content
            // Helper method again
            StreamingContent oldContent = GetContentByTitle(originalTitle);

            //Update the content
            /* Instead of ripping the old content out completely and adding new content, we're 
            leaving a placeholder there and just changing the properties, essentially.*/

            // If the old content isn't void (null), then replace its properties with new content's
            if (oldContent != null)
            {
                oldContent.Title = newContent.Title;
                oldContent.Description = newContent.Description;
                oldContent.MaturityRating = newContent.MaturityRating;
                oldContent.IsFamilyFriendly = newContent.IsFamilyFriendly;
                oldContent.StarRating = newContent.StarRating;
                oldContent.TypeOfGenre = newContent.TypeOfGenre;

                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete
        /* Need to find exact instance of StreamingContent for delete method. Ok, here's a title
        to remove. Go ahead and remove this object. Return a true or false value depending on
        what's done.*/
        public bool RemoveContentFromList(string title)
        {
            // Using the Helper Method here to find the correct title.
            StreamingContent content = GetContentByTitle(title);

            /* If the methods can't find the content, then we'll return false because we
            couldn't delete anything.*/
            if (content == null)
            {
                return false;
            }

            /* Once we get here, we know we have some content. We're getting the count from the
            list to see if it was successfully removed. We set the initialCount variable equal to
            the _listOfContent count, then we remove the selected content from _listOfContent.*/
            int initialCount = _listOfContent.Count;
            _listOfContent.Remove(content);

            /* This loop will work because the initialCount variable equals the _listOfContent count
            prior to deletion. The new _listOfContent count should be smaller than it was before
            after deletion, thereby being smaller than the initialCount variable now. If that is true,
            then the content was successfully removed.*/
            if (initialCount > _listOfContent.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Helper method

        /* It's job is to help the other methods (e.g., delete). It will get the correct streaming content
        based on some parameter. You have a content title in mind and are searching one by one
        until you find it, then you keep it and leave the loop.*/

        /*This public method returns a StreamingContent object based on its title.
        The problem is that the _listOfContent doesn't hold strings, it holds Streaming Content,
        so you have to go through each StreamingContent object to check if it's the right one.*/
        public StreamingContent GetContentByTitle(string title)
        {
            /* For each loop will take _listOfContent, iterate through it for each StreamingContent
            object, call it content, and do something with logic.*/
            foreach (StreamingContent content in _listOfContent)
            {
                // Use two == because you're comparing
                // .ToLower converts input to lower cased so searching isn't cased dependent
                if (content.Title.ToLower() == title.ToLower())
                {
                    return content;
                }
            }

            // Safety net in case you never find the object.
            return null;
        }
    }
}