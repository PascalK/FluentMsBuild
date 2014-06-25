using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class PropertyTest
    {
        Property unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreatePropertyElement("InitialPropertyNameValue");
        }
        [TestClass]
        public class TheNameProperty : PropertyTest
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
        public class TheValueProperty : PropertyTest
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
        public class TheXmlNameProperty : PropertyTest
        {
            [TestMethod]
            public void ShouldReturnTheNameProperty()
            {
                string expectedName;
                //Arrange
                expectedName = "TestNameValue";
                unit.Name = expectedName;
                //Act
                var actualName = unit.Name;
                //Assert
                actualName.Should().Be(expectedName);
            }
        }
        [TestClass]
        public class TheLoadMethod : PropertyTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadAnEmptyElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<PropertyGroup>" +
                                    "<TestProperty />" +
                                "</PropertyGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualProperty = root.PropertyGroups.Single().Properties.Single();
                actualProperty.Value.Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldLoadInnerTextAsValueProperty()
            {
                string expectedValue;
                //Arrange
                expectedValue = "TestValueValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<PropertyGroup>" +
                                    "<TestProperty>" +
                                        expectedValue +
                                    "</TestProperty>" +
                                "</PropertyGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualProperty = root.PropertyGroups.Single().Properties.Single();
                actualProperty.Value.Should().Be(expectedValue);
            }
            [TestMethod]
            public void ShouldLoadTagNameAsNameProperty()
            {
                string expectedTagName;
                //Arrange
                expectedTagName = "TestTagNameValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<PropertyGroup>" +
                                    "<" + expectedTagName + " />" +
                                "</PropertyGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualProperty = root.PropertyGroups.Single().Properties.Single();
                actualProperty.Name.Should().Be(expectedTagName);
            }
        }
        [TestClass]
        public class TheSaveMethod : PropertyTest
        {
            [TestMethod]
            public void ShouldWriteTheValueAsInnerText()
            {
                StringWriter stringWriter;
                string expectedName;
                string expectedValue;
                //Arrange
                expectedName = "TestTagName";
                expectedValue = "TestValue";
                root = Root.Create();
                root.AddProperty<string>(expectedName, expectedValue);
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlElement(expectedName, expectedValue));
            }
            [TestMethod]
            public void ShouldNotWriteTheValueWhenItIsNullspace()
            {
                StringWriter stringWriter;
                string expectedName;
                string expectedValue;
                //Arrange
                expectedName = "TestTagName";
                expectedValue = "  ";
                root = Root.Create();
                root.AddProperty<string>(expectedName, expectedValue);
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(string.Format("<{0} />",expectedName));
            }
            [TestMethod]
            public void ShouldWriteTheLabelAttribute()
            {
                StringWriter stringWriter;
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                root = Root.Create();
                root.AddProperty<string>("PropertyName", null).Element.Label = expectedLabelValue;
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
                root.AddProperty<string>("PropertyName", null).Element.Condition = expectedConditionValue;
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
            string XmlElement(string tagName, string value)
            {
                return string.Format("<{0}>{1}</{0}>", tagName, value);
            }
        }
    }
}
