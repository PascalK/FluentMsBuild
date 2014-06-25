using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class ExtensionsTest
    {
        Extensions unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateProjectExtensionsElement();
        }

        [TestClass]
        public class TheConditionProperty : ExtensionsTest
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
        public class TheLoadMethod : ExtensionsTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadEmptyProjectExtensionsTag()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ProjectExtensions />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                root.Children.Single().Should().BeOfType<Extensions>().Which.Content.Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldLoadAnyProjectExtensionsTag()
            {
                string expectedTag;
                string expectedInnerTag;
                //Arrange
                expectedTag = "RandomNonExistingTagName";
                expectedInnerTag = "RandomNonExistingInnerTagName";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ProjectExtensions>" +
                                    "<" + expectedTag + ">" +
                                        "<" + expectedInnerTag + ">" +
                                        "</" + expectedInnerTag + ">" +
                                    "</" + expectedTag + ">" +
                                "</ProjectExtensions>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                root.Children.Single().Should().BeOfType<Extensions>().Which
                    .Content.Should().NotBeEmpty().And
                        .Contain(expectedTag).And
                        .Contain(expectedInnerTag);
            }
            [TestMethod]
            public void ShouldIgnoreXmlThatIsNotOfTypeElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "This is Test InnerText" +
                                "<!--This is Test Comment-->" +
                                "<ProjectExtensions>" +
                                "</ProjectExtensions>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                root.Children.Single().Should().BeOfType<Extensions>().Which.Content.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheSaveMethod : ExtensionsTest
        {
            [TestMethod]
            public void ShouldWriteContentToElement()
            {
                StringWriter stringWriter;
                string expectedContentValue;
                //Arrange
                expectedContentValue = "TestContentValue";
                root = Root.Create();
                var extension = root.CreateProjectExtensionsElement();
                extension.Content = expectedContentValue;
                root.AppendChild(extension);
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(expectedContentValue);
            }
            string XmlAttribute(string name, string value)
            {
                return string.Format("{0}=\"{1}\"", name, value);
            }
        }
        [TestClass]
        public class TheIndexer : ExtensionsTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldReturXmlNodeValueByName()
            {
                string expectedTagName;
                string expectedNodeValue;
                //Arrange
                expectedTagName = "TestElementName";
                expectedNodeValue = "TestElementValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ProjectExtensions>" +
                                    "<" + expectedTagName + ">" +
                                        expectedNodeValue +
                                    "</" + expectedTagName + ">" +
                                "</ProjectExtensions>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                root.Children.Single().Should().BeOfType<Extensions>().Which[expectedTagName].Should().Be(expectedNodeValue);
            }
            [TestMethod]
            public void ShouldIgnoreSettingEmptyElement()
            {
                //Arrange
                //Act
                unit[string.Empty] = "IgnoredValue";
                //Assert
                unit[string.Empty].Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldAddChildElementWhenNameDoesNotYetExist()
            {
                string actualName;
                string actualValue;
                //Arrange
                actualName = "TestName";
                actualValue = "TestValue";
                //Act
                unit[actualName] = actualValue;
                //Assert
                unit.Content.Should().Contain(XmlElement(actualName, actualValue));
            }
            [TestMethod]
            public void ShouldSetInnerXmlFromElementWhenItAlreadyExists()
            {
                string actualName;
                string initialValue;
                string actualValue;
                //Arrange
                actualName = "TestName";
                initialValue = "InitialTestValue";
                actualValue = "TestValue";
                unit[actualName] = initialValue;
                //Act
                unit[actualName] = actualValue;
                //Assert
                unit.Content.Should()
                    .Contain(XmlElement(actualName, actualValue)).And
                    .NotContain(XmlElement(actualName, initialValue));
            }
            [TestMethod]
            public void ShouldRemoveElementWhenSettingAnEmptyValue()
            {
                string actualName;
                string initialValue;
                string actualValue;
                //Arrange
                actualName = "TestName";
                initialValue = "InitialTestValue";
                actualValue = string.Empty;
                unit[actualName] = initialValue;
                //Act
                unit[actualName] = actualValue;
                //Assert
                unit.Content.Should().BeEmpty();
            }
            string XmlElement(string tagName, string value)
            {
                return string.Format("<{0}>{1}</{0}>", tagName, value);
            }
        }
    }
}
