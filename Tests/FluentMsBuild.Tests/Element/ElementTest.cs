using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class ElementTest
    {
        Element unit;
        Root root;

        ImportGroup importGroup;

        [TestInitialize]
        public void InitializeTest()
        {
            root = Root.Create();
            importGroup = root.AddImportGroup().Element;
            unit = importGroup.AddImport("InitialTestProjectValue").Element;
        }

        [TestClass]
        public class TheNextSiblingProperty : ElementTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldReturnNullWhenTheElementIsAnOnlyChild()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemDefinitionGroup>" +
                                    "<ItemDefinition />" +
                                "</ItemDefinitionGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItemDefinition = root.ItemDefinitionGroups.Single().ItemDefinitions.Single();
                actualItemDefinition.NextSibling.Should().BeNull();
            }
            [TestMethod]
            public void ShouldReturnTheNextElementsSibling()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemDefinitionGroup>" +
                                    "<ItemDefinition />" +
                                    "<ItemDefinition />" +
                                "</ItemDefinitionGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItemDefinitionGroup = root.ItemDefinitionGroups.Single();
                actualItemDefinitionGroup.ItemDefinitions.Should().HaveCount(2);
                var firstItemDefinition = actualItemDefinitionGroup.ItemDefinitions.First();
                var secondItemDefinition = actualItemDefinitionGroup.ItemDefinitions.Last();
                firstItemDefinition.NextSibling.Should().Be(secondItemDefinition);
            }
        }
        [TestClass]
        public class ThePreviousSiblingProperty : ElementTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldReturnNullWhenTheElementIsAnOnlyChild()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemDefinitionGroup>" +
                                    "<ItemDefinition />" +
                                "</ItemDefinitionGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItemDefinition = root.ItemDefinitionGroups.Single().ItemDefinitions.Single();
                actualItemDefinition.PreviousSibling.Should().BeNull();
            }
            [TestMethod]
            public void ShouldReturnThePreviousElementsSibling()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemDefinitionGroup>" +
                                    "<ItemDefinition />" +
                                    "<ItemDefinition />" +
                                "</ItemDefinitionGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItemDefinitionGroup = root.ItemDefinitionGroups.Single();
                actualItemDefinitionGroup.ItemDefinitions.Should().HaveCount(2);
                var firstItemDefinition = actualItemDefinitionGroup.ItemDefinitions.First();
                var secondItemDefinition = actualItemDefinitionGroup.ItemDefinitions.Last();
                secondItemDefinition.PreviousSibling.Should().Be(firstItemDefinition);
            }
        }
        [TestClass]
        public class TheAllParentsProperty : ElementTest
        {
            [TestMethod]
            public void ShouldReturnAllParents()
            {
                //Arrange
                //Act
                var actualResult = unit.AllParents;
                //Assert
                actualResult.Should().HaveCount(2);
                actualResult.Should().Contain(importGroup);
                actualResult.Should().Contain(root);
            }
        }
        [TestClass]
        public class TheLoadMethod
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldFailLoadingAnUnknownAttribute()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Import UnknownAttribute=\"RandomValue\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = () => Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidOperationException>();
            }
        }
    }
}