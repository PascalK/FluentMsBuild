using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class MetadataTest
    {
        Metadata unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateMetadataElement("InitialMetadataName");
        }

        [TestClass]
        public class TheNameProperty : MetadataTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Name = null;
                //Act
                var actualName = unit.Name;
                //Assert
                actualName.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheValueProperty : MetadataTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Value = null;
                //Act
                var actualValue = unit.Value;
                //Assert
                actualValue.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheLoadMethod : MetadataTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadEmptyItemDefinitionElement()
            {
                //Arraneg
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
                var actualMetadata = root.ItemDefinitionGroups.Single().ItemDefinitions.Single().Metadata.Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldLoadMetadataElement()
            {
                string metadataName;
                string metadataValue;
                //Arraneg
                metadataName = "TestMetadataName";
                metadataValue = "TestMetadataValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemDefinitionGroup>" +
                                    "<ItemDefinition>" +
                                        "<" + metadataName + ">" + metadataValue + "</" + metadataName + ">" +
                                    "</ItemDefinition>" +
                                "</ItemDefinitionGroup>" +
                            "</Project>";

                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualMetadata = root.ItemDefinitionGroups.Single().ItemDefinitions.Single().Metadata.Single();
                actualMetadata.Name.Should().Be(metadataName);
                actualMetadata.Value.Should().Be(metadataValue);
            }
            [TestMethod]
            public void ShouldLoadConditionAttribute()
            {
                string expectedConditionValue = "TestConditionValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemDefinitionGroup>" +
                                    "<ItemDefinition Condition=\"" + expectedConditionValue + "\" />" +
                                "</ItemDefinitionGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItemDefinition = root.ItemDefinitionGroups.Single().ItemDefinitions.Single();
                actualItemDefinition.Condition.Should().Be(expectedConditionValue);
            }
            [TestMethod]
            public void ShouldLoadLabelAttribute()
            {
                string expectedLabelValue = "TestLabelValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemDefinitionGroup>" +
                                    "<ItemDefinition Label=\"" + expectedLabelValue + "\" />" +
                                "</ItemDefinitionGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItemDefinition = root.ItemDefinitionGroups.Single().ItemDefinitions.Single();
                actualItemDefinition.Label.Should().Be(expectedLabelValue);
            }
        }
        [TestClass]
        public class TheSaveMethod : MetadataTest
        {
            [TestMethod]
            public void ShouldWriteTheLabelAttribute()
            {
                StringWriter stringWriter;
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                root = Root.Create();
                root.AddItem(null, null).Element.AddMetadata("MetadataName", null).Element.Label = expectedLabelValue;
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
                root.AddItem(null, null).Element.AddMetadata("MetadataName", null).Element.Condition = expectedConditionValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Condition", expectedConditionValue));
            }
            [TestMethod]
            public void ShouldWriteValueAsInnerText()
            {
                StringWriter stringWriter;
                string expectedMetadataValue;
                //Arrange
                expectedMetadataValue = "TestConditionValue";
                root = Root.Create();
                root.AddItem(null, null).Element.AddMetadata("MetadataName", expectedMetadataValue);
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(expectedMetadataValue);
            }
            string XmlAttribute(string name, string value)
            {
                return string.Format("{0}=\"{1}\"", name, value);
            }
        }
    }
}
