using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class InvalidProjectFileExceptionTest
    {
        InvalidProjectFileException unit;
        protected readonly string initialMessageValue = "InitialTestMessageValue";

        [TestInitialize]
        public void InitializeTest()
        {
            unit = new InvalidProjectFileException(initialMessageValue);
        }

        [TestClass]
        public class TheMessageProperty : InvalidProjectFileExceptionTest
        {
            [TestMethod]
            public void ShouldReturnTheExceptionMessageWhenNoProjectFileIsKnown()
            {
                //Arrange
                //Act
                var actualResult = unit.Message;
                //Assert
                actualResult.Should().Be(initialMessageValue);
            }
            [TestMethod]
            public void ShouldReturnTheExceptionMessageWithTheProjectLocationWhenAProjectFileIsProvided()
            {
                string expectedProjectFile;
                string expectedMessage;
                //Arrange
                expectedProjectFile = "TestProjectFileValue";
                expectedMessage = "TestMessageValue";
                unit = new InvalidProjectFileException(expectedProjectFile, 0, 0, 0, 0, expectedMessage, null, null, null);
                //Act
                var actualResult = unit.Message;
                //Assert
                actualResult.Should().Contain(expectedMessage).And.Contain(expectedProjectFile);
            }
        }
        [TestClass]
        public class TheBaseMessageProperty : InvalidProjectFileExceptionTest
        {
            [TestMethod]
            public void ShouldReturnTheBaseExceptionsMessage()
            {
                string expectedProjectFile;
                string expectedMessage;
                //Arrange
                expectedProjectFile = "TestProjectFileValue";
                expectedMessage = "TestMessageValue";
                unit = new InvalidProjectFileException(expectedProjectFile, 0, 0, 0, 0, expectedMessage, null, null, null);
                //Act
                var actualResult = unit.BaseMessage;
                //Assert
                actualResult.Should().Be(expectedMessage);
            }
        }
        [TestClass]
        public class TheGetLocationMethod : InvalidProjectFileExceptionTest
        {
            [TestMethod]
            public void ShouldReturnAllProvidedLocationInformation()
            {
                string expectedProjectFile;
                string expectedMessage;
                int expectedLineNumber;
                int expectedColumnNumber;
                int expectedEndLineNumber;
                int expectedEndColumnNumber;
                //Arrange
                expectedProjectFile = "TestProjectFileValue";
                expectedLineNumber = 1;
                expectedColumnNumber = 2;
                expectedEndLineNumber = 3;
                expectedEndColumnNumber = 4;
                expectedMessage = "TestMessageValue";
                unit = new InvalidProjectFileException(expectedProjectFile, expectedLineNumber, expectedColumnNumber, expectedEndLineNumber, expectedEndColumnNumber, expectedMessage, null, null, null);
                //Act
                var actualResult = unit.GetLocation();
                //Assert
                actualResult.Should().Be(string.Format(" at: {0} (1,2 - 3,4)", expectedProjectFile));
            }
            [TestMethod]
            public void ShouldContainStartLineLocation()
            {
                string expectedProjectFile;
                string expectedMessage;
                int expectedLineNumber;
                int expectedColumnNumber;
                int expectedEndLineNumber;
                int expectedEndColumnNumber;
                //Arrange
                expectedProjectFile = "TestProjectFileValue";
                expectedLineNumber = 1;
                expectedColumnNumber = 2;
                expectedEndLineNumber = 0;
                expectedEndColumnNumber = 0;
                expectedMessage = "TestMessageValue";
                unit = new InvalidProjectFileException(expectedProjectFile, expectedLineNumber, expectedColumnNumber, expectedEndLineNumber, expectedEndColumnNumber, expectedMessage, null, null, null);
                //Act
                var actualResult = unit.GetLocation();
                //Assert
                actualResult.Should().Contain("(1,2)");
            }
            [TestMethod]
            public void ShouldContainEndLineLocation()
            {
                string expectedProjectFile;
                string expectedMessage;
                int expectedLineNumber;
                int expectedColumnNumber;
                int expectedEndLineNumber;
                int expectedEndColumnNumber;
                //Arrange
                expectedProjectFile = "TestProjectFileValue";
                expectedLineNumber = 0;
                expectedColumnNumber = 0;
                expectedEndLineNumber = 1;
                expectedEndColumnNumber = 2;
                expectedMessage = "TestMessageValue";
                unit = new InvalidProjectFileException(expectedProjectFile, expectedLineNumber, expectedColumnNumber, expectedEndLineNumber, expectedEndColumnNumber, expectedMessage, null, null, null);
                //Act
                var actualResult = unit.GetLocation();
                //Assert
                actualResult.Should().Contain("1,2)");
            }
            [TestMethod]
            public void ShouldContainLineNumber()
            {
                string expectedProjectFile;
                string expectedMessage;
                int expectedLineNumber;
                int expectedColumnNumber;
                int expectedEndLineNumber;
                int expectedEndColumnNumber;
                //Arrange
                expectedProjectFile = "TestProjectFileValue";
                expectedLineNumber = 1;
                expectedColumnNumber = 0;
                expectedEndLineNumber = 0;
                expectedEndColumnNumber = 0;
                expectedMessage = "TestMessageValue";
                unit = new InvalidProjectFileException(expectedProjectFile, expectedLineNumber, expectedColumnNumber, expectedEndLineNumber, expectedEndColumnNumber, expectedMessage, null, null, null);
                //Act
                var actualResult = unit.GetLocation();
                //Assert
                actualResult.Should().Contain("(1)");
            }
            [TestMethod]
            public void ShouldContainEndLineNumber()
            {
                string expectedProjectFile;
                string expectedMessage;
                int expectedLineNumber;
                int expectedColumnNumber;
                int expectedEndLineNumber;
                int expectedEndColumnNumber;
                //Arrange
                expectedProjectFile = "TestProjectFileValue";
                expectedLineNumber = 0;
                expectedColumnNumber = 0;
                expectedEndLineNumber = 1;
                expectedEndColumnNumber = 0;
                expectedMessage = "TestMessageValue";
                unit = new InvalidProjectFileException(expectedProjectFile, expectedLineNumber, expectedColumnNumber, expectedEndLineNumber, expectedEndColumnNumber, expectedMessage, null, null, null);
                //Act
                var actualResult = unit.GetLocation();
                //Assert
                actualResult.Should().Contain("1)");
            }
            [TestMethod]
            public void ShouldReturnProjectFileNameWhenProvided()
            {
                string expectedProjectFile;
                string expectedMessage;
                int expectedLineNumber;
                int expectedColumnNumber;
                int expectedEndLineNumber;
                int expectedEndColumnNumber;
                //Arrange
                expectedProjectFile = "TestProjectFileValue";
                expectedLineNumber = 1;
                expectedColumnNumber = 2;
                expectedEndLineNumber = 3;
                expectedEndColumnNumber = 4;
                expectedMessage = "TestMessageValue";
                unit = new InvalidProjectFileException(expectedProjectFile, expectedLineNumber, expectedColumnNumber, expectedEndLineNumber, expectedEndColumnNumber, expectedMessage, null, null, null);
                //Act
                var actualResult = unit.GetLocation();
                //Assert
                actualResult.Should().Contain(string.Format(" at: {0} ", expectedProjectFile));
            }
            [TestMethod]
            public void ShouldReturnUnknownFileWhenNotProvided()
            {
                string expectedProjectFile;
                string expectedMessage;
                int expectedLineNumber;
                int expectedColumnNumber;
                int expectedEndLineNumber;
                int expectedEndColumnNumber;
                //Arrange
                expectedProjectFile = null;
                expectedLineNumber = 1;
                expectedColumnNumber = 2;
                expectedEndLineNumber = 3;
                expectedEndColumnNumber = 4;
                expectedMessage = "TestMessageValue";
                unit = new InvalidProjectFileException(expectedProjectFile, expectedLineNumber, expectedColumnNumber, expectedEndLineNumber, expectedEndColumnNumber, expectedMessage, null, null, null);
                //Act
                var actualResult = unit.GetLocation();
                //Assert
                actualResult.Should().Contain(string.Format(" at: {0} ", "[UnknownFile]"));
            }
        }
    }
}