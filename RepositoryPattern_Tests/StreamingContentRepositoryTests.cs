using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryPattern_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern_Tests
{
    [TestClass]
    public class StreamingContentRepositoryTests
    {
        private StreamingContentRepository _repo;
        private StreamingContent _content;

        /* TestInitialize says that whatever method is within it should run at the
        beginning of all unit tests. Must be a public method.*/
        [TestInitialize]
        public void Arrange()
        {
            _repo = new StreamingContentRepository();
            _content = new StreamingContent("Rubber", "A car trye comes to life with the power" +
                "to make people explode and goes on a murderous rampage through the California" +
                "desert.", "R", 5.8, false, GenreType.Drama);

            _repo.AddContentToList(_content);
        }

        /* Add method. We need to verify content has been added, so we can use either
        GetContentByTitle method or AddContentToList method.
        */
        [TestMethod]
        public void AddToList_ShouldGetNotNull()
        {
            // Arrange --> Setting up the playing field
            StreamingContent content = new StreamingContent();
            content.Title = "Toy Story";
            StreamingContentRepository repository = new StreamingContentRepository();

            // Act --> Get/run the code we want to test
            /* Using the dot operator to add content to our new StreamingContentRepository
            object, repository. Here we pass in the new StreamingContent object, content.*/
            repository.AddContentToList(content);

            /* Creating a new StreamingContent object called contentFromDirectory to catch
            the StreamingContentRepository object, repository's new properties, which we
            retrieved from the GetContentByTitle method.*/
            StreamingContent contentFromDirectory = repository.GetContentByTitle("Toy Story");

            // Assert --> Use the assert class to verify the expected outcome
            /* If the AddContentToList method worked, then "repository" should have had the 
            title "Toy Story" stored into it. "contentFromDirectory" should have retrieved
            the same repository object if the GetContentByTitle method worked. Therefore,
            contentFromDirectory should not be null.*/
            Assert.IsNotNull(contentFromDirectory);
        }

        // Update
        [TestMethod]
        public void UpdateExistingContent_ShouldReturnTrue()
        {
            // Arrange
            // TestInitialize
            // This is the new (updated) content
            StreamingContent newContent = new StreamingContent("Rubber", "A car trye comes to life with the power" +
                "to make people explode and goes on a murderous rampage through the California" +
                "desert.", "R", 10, false, GenreType.RomCom);

            // Act
            /* We're updating the actual content here using the original title and 
            new StreamingContent object*/
            bool updateResult = _repo.UpdateExistingContent("Rubber", newContent);

            // Assert
            /* Using the IsTrue method because the UpdateExistingContent method
            returns a bool*/
            Assert.IsTrue(updateResult);
        }

        // DataTestMethods allow data to be specified in-line with DataRows
        [DataTestMethod]
        [DataRow("Rubber", true)]
        [DataRow("Toy Story", false)]
        public void UpdateExistingContent_ShouldMatchGivenBool(string originalTitle, bool shouldUpdate)
        {
            // Arrange
            // TestInitialize
            StreamingContent newContent = new StreamingContent("Rubber", "A car trye comes to life with the power" +
                "to make people explode and goes on a murderous rampage through the California" +
                "desert.", "R", 10, false, GenreType.RomCom);

            // Act
            bool updateResult = _repo.UpdateExistingContent("Rubber", newContent);

            // Assert
            Assert.AreEqual(shouldUpdate, updateResult);
        }

        [TestMethod]
        public void DeleteContent_ShouldReturnTrue()
        {
            // Arrange
            // TestInitialize

            // Act
            bool deleteResult = _repo.RemoveContentFromList(_content.Title);

            // Assert
            Assert.IsTrue(deleteResult);
        }
    }
}
