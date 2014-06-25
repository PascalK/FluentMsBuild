using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class ChooseTest
    {
        Choose unit;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            unit = root.CreateChooseElement();
        }

        [TestClass]
        public class TheConditionProperty : ChooseTest
        {
            [TestMethod]
            public void ShouldNotBeSettable()
            {
                //Arrange
                //Act
                Action action = () => unit.Condition = null;
                //Assert
                action.ShouldThrow<InvalidOperationException>(because: "Conditions are not applicable to Choose elements");
            }
            [TestMethod]
            public void ShouldAlwaysReturnNull()
            {
                //Arrange
                //Act
                var actualResult = unit.Condition;
                //Assert
                actualResult.Should().BeNull();
            }
        }

        [TestClass]
        public class TheLoadChildElementMethod : ChooseTest
        {
            XmlReader reader;
            Root root;
            string actualXml;

            [TestMethod]
            public void ShouldLoadEmptyChildrenWhenThereIsNoContent()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualChooseElement = root.ChooseElements.Single();
                actualChooseElement.Children.Should().BeEmpty(because: "the xml does not contain any children");
            }
            [TestMethod]
            public void ShouldLoadOtherwiseElement()
            {

                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<Otherwise />" +
                                "</Choose>" +
                            "</Project>";
                //Arrange
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualChooseElement = root.ChooseElements.Single();
                actualChooseElement.OtherwiseElement.Should().NotBeNull(because: "it exists in the xml");
                actualChooseElement.WhenElements.Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldLoadWhenElement()
            {
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<When />" +
                                "</Choose>" +
                            "</Project>";
                //Arrange
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualChooseElement = root.ChooseElements.Single();
                actualChooseElement.OtherwiseElement.Should().BeNull(because: "it does not exists in the xml");
                actualChooseElement.WhenElements.Should().HaveCount(1, because: "it exists in the xml");
            }
            [TestMethod]
            public void ShouldLoadWhenElements()
            {
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<When />" +
                                    "<When />" +
                                "</Choose>" +
                            "</Project>";
                //Arrange
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualChooseElement = root.ChooseElements.Single();
                actualChooseElement.OtherwiseElement.Should().BeNull(because: "it does not exists in the xml");
                actualChooseElement.WhenElements.Should().HaveCount(2, because: "it they exist in the xml");
            }
            [TestMethod]
            public void ShouldFailOnUnknownElement()
            {
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<TestUnknownElement />" +
                                "</Choose>" +
                            "</Project>";
                //Arrange
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action =()=> Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidProjectFileException>();
            }
        }
    }
}