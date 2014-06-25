using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class ImportGroupTest
    {
        ImportGroup unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateImportGroupElement();
        }

        [TestClass]
        public class TheAddImportMethod : ImportGroupTest
        {
            [TestMethod]
            public void ShouldAddImportElementToImports()
            {
                string actualProject;
                //Arrange
                actualProject = "TestProject";
                //Act
                unit.AddImport(actualProject);
                //Assert
                unit.Imports.Single().Project.Should().Be(actualProject);
            }
        }
        [TestClass]
        public class TheLoadMethod : ImportGroupTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadAnEmptyImportGroupElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ImportGroup />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                root.ImportGroups.Single().Imports.Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldLoadLabelAttribute()
            {
                string expectedLabelValue = "TestLabelValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ImportGroup Label=\"" + expectedLabelValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualImportGroup = root.ImportGroups.Single();
                actualImportGroup.Label.Should().Be(expectedLabelValue);
            }
            [TestMethod]
            public void ShouldLoadConditionAttribute()
            {
                string expectedConditionValue = "TestConditionValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ImportGroup Condition=\"" + expectedConditionValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualImportGroup = root.ImportGroups.Single();
                actualImportGroup.Condition.Should().Be(expectedConditionValue);
            }
            [TestMethod]
            public void ShouldLoadChildrenAsImportElements()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ImportGroup>" +
                                    "<Import Project=\"Test\" />" +
                                "</ImportGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualImport = root.ImportGroups.Single().Imports.Single();
                //actualImport.na.Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldFailLoadingUnknownChildElements()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ImportGroup>" +
                                    "<ImportTest Project=\"Test\" />" +
                                "</ImportGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = () => Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidProjectFileException>();
            }
        }
    }
}