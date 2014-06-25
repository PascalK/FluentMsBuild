using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class ImportTest
    {
        Import unit;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            unit = root.CreateImportElement("InitialTestProjectValue");
        }
        [TestClass]
        public class TheProjectProperty : ImportTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Project = null;
                //Act
                var actualProject = unit.Project;
                //Assert
                actualProject.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheLoadAttributeMethod : ImportTest
        {
            XmlReader reader;
            Root root;
            string actualXml;

            [TestMethod]
            public void ShouldFailWhenProjectAttributeIsMissing()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Import />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = () => Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidOperationException>();
            }
            [TestMethod]
            public void ShouldLoadProjectAttribute()
            {
                string expectedProjectValue = "TestProjectValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Import Project=\"" + expectedProjectValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualImport = root.Imports.Single();
                actualImport.Project.Should().Be(expectedProjectValue);
            }
            [TestMethod]
            public void ShouldLoadLabelAttribute()
            {
                string expectedLabelValue = "TestLabelValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Import Project=\"TestProjectValue\" Label=\"" + expectedLabelValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualImport = root.Imports.Single();
                actualImport.Label.Should().Be(expectedLabelValue);
            }
            [TestMethod]
            public void ShouldLoadConditionAttribute()
            {
                string expectedConditionValue = "TestConditionValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Import Project=\"TestProjectValue\" Condition=\"" + expectedConditionValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualImport = root.Imports.Single();
                actualImport.Condition.Should().Be(expectedConditionValue);
            }
        }
        [TestClass]
        public class TheSaveValueMethod : ImportTest
        {
            Root root;

            [TestMethod]
            public void ShouldWriteTheProjectAttribute()
            {
                StringWriter stringWriter;
                string expectedProjectValue;
                //Arrange
                expectedProjectValue = "TestProjectValue";
                root = Root.Create();
                root.AddImport(expectedProjectValue);
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Project", expectedProjectValue));
            }
            [TestMethod]
            public void ShouldWriteTheLabelAttribute()
            {
                StringWriter stringWriter;
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                root = Root.Create();
                root.AddImport(null).Element.Label = expectedLabelValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Label", expectedLabelValue));
            }
            [TestMethod]
            public void ShouldWriteTheConditionAttribute()
            {
                StringWriter stringWriter;
                string expectedConditionValue;
                //Arrange
                expectedConditionValue = "TestConditionValue";
                root = Root.Create();
                root.AddImport(null).Element.Condition = expectedConditionValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Condition", expectedConditionValue));
            }
            string XmlAttribute(string name, string value)
            {
                return string.Format("{0}=\"{1}\"", name, value);
            }
        }
    }
}