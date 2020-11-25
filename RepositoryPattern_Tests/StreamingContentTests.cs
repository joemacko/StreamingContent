using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryPattern_Repository;

namespace RepositoryPattern_Tests
{
    [TestClass]
    public class StreamingContentTests
    {
        [TestMethod]
        public void SetTitle_ShouldSetCorrectString()
        {
            // Arrange
            StreamingContent content = new StreamingContent();
            content.Title = "Toy Story";

            // Act
            string expected = "Toy Story";
            string actual = content.Title;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
