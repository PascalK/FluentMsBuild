using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class OtherwiseTest
    {
        Otherwise unit;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            unit = root.CreateOtherwiseElement();
        }

        [TestClass]
        public class TheConditionProperty : OtherwiseTest
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
        public class TheLoadChildElementMethod : OtherwiseTest
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
                                "<Choose>" +
                                    "<Otherwise />" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOtherwiseElement = root.ChooseElements.Single().OtherwiseElement;
                actualOtherwiseElement.Children.Should().BeEmpty(because: "the xml does not contain any children");
            }
            [TestMethod]
            public void ShouldLoadChooseElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<Otherwise>" +
                                        "<Choose />" +
                                    "</Otherwise>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOtherwiseElement = root.ChooseElements.Single().OtherwiseElement;
                actualOtherwiseElement.ChooseElements.Should().HaveCount(1, because: "it exists in the xml");
            }
            [TestMethod]
            public void ShouldLoadChooseElements()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<Otherwise>" +
                                        "<Choose />" +
                                        "<Choose />" +
                                    "</Otherwise>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOtherwiseElement = root.ChooseElements.Single().OtherwiseElement;
                actualOtherwiseElement.ChooseElements.Should().HaveCount(2, because: "they exist in the xml");
            }
            [TestMethod]
            public void ShouldLoadItemGroupElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<Otherwise>" +
                                        "<ItemGroup />" +
                                    "</Otherwise>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOtherwiseElement = root.ChooseElements.Single().OtherwiseElement;
                actualOtherwiseElement.ItemGroups.Should().HaveCount(1, because: "it exists in the xml");
            }
            [TestMethod]
            public void ShouldLoadItemGroupElements()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<Otherwise>" +
                                        "<ItemGroup />" +
                                        "<ItemGroup />" +
                                    "</Otherwise>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOtherwiseElement = root.ChooseElements.Single().OtherwiseElement;
                actualOtherwiseElement.ItemGroups.Should().HaveCount(2, because: "they exist in the xml");
            }
            [TestMethod]
            public void ShouldLoadPropertyGroupElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<Otherwise>" +
                                        "<PropertyGroup />" +
                                    "</Otherwise>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOtherwiseElement = root.ChooseElements.Single().OtherwiseElement;
                actualOtherwiseElement.PropertyGroups.Should().HaveCount(1, because: "it exists in the xml");
            }
            [TestMethod]
            public void ShouldLoadPropertyGroupElements()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<Otherwise>" +
                                        "<PropertyGroup />" +
                                        "<PropertyGroup />" +
                                    "</Otherwise>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOtherwiseElement = root.ChooseElements.Single().OtherwiseElement;
                actualOtherwiseElement.PropertyGroups.Should().HaveCount(2, because: "they exist in the xml");
            }
            [TestMethod]
            public void ShouldFailWhenLoadingUnknownTags()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<Otherwise>" +
                                        "<UnknownTag />" +
                                    "</Otherwise>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = ()=>Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidProjectFileException>();
            }
        }
    }
}