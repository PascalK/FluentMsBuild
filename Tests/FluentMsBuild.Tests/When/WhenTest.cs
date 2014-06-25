using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class WhenTest
    {
        When unit;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            unit = root.CreateWhenElement(string.Empty);
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
                                    "<When />" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualWhenElement = root.ChooseElements.Single().WhenElements.Single();
                actualWhenElement.Children.Should().BeEmpty(because: "the xml does not contain any children");
            }
            [TestMethod]
            public void ShouldLoadChooseElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<When>" +
                                        "<Choose />" +
                                    "</When>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualWhenElement = root.ChooseElements.Single().WhenElements.Single();
                actualWhenElement.ChooseElements.Should().HaveCount(1, because: "it exists in the xml");
            }
            [TestMethod]
            public void ShouldLoadChooseElements()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<When>" +
                                        "<Choose />" +
                                        "<Choose />" +
                                    "</When>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualWhenElement = root.ChooseElements.Single().WhenElements.Single();
                actualWhenElement.ChooseElements.Should().HaveCount(2, because: "they exist in the xml");
            }
            [TestMethod]
            public void ShouldLoadItemGroupElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<When>" +
                                        "<ItemGroup />" +
                                    "</When>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualWhenElement = root.ChooseElements.Single().WhenElements.Single();
                actualWhenElement.ItemGroups.Should().HaveCount(1, because: "it exists in the xml");
            }
            [TestMethod]
            public void ShouldLoadItemGroupElements()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<When>" +
                                        "<ItemGroup />" +
                                        "<ItemGroup />" +
                                    "</When>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualWhenElement = root.ChooseElements.Single().WhenElements.Single();
                actualWhenElement.ItemGroups.Should().HaveCount(2, because: "they exist in the xml");
            }
            [TestMethod]
            public void ShouldLoadPropertyGroupElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<When>" +
                                        "<PropertyGroup />" +
                                    "</When>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualWhenElement = root.ChooseElements.Single().WhenElements.Single();
                actualWhenElement.PropertyGroups.Should().HaveCount(1, because: "it exists in the xml");
            }
            [TestMethod]
            public void ShouldLoadPropertyGroupElements()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<When>" +
                                        "<PropertyGroup />" +
                                        "<PropertyGroup />" +
                                    "</When>" +
                                "</Choose>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualWhenElement = root.ChooseElements.Single().WhenElements.Single();
                actualWhenElement.PropertyGroups.Should().HaveCount(2, because: "they exist in the xml");
            }
            [TestMethod]
            public void ShouldFailLoadingWhenInnerTagIsUnknown()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Choose>" +
                                    "<When>" +
                                        "<UnknownTag />" +
                                    "</When>" +
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