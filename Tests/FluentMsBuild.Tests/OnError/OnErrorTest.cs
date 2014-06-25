using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class OnErrorTest
    {
        OnError unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateOnErrorElement("InitialTestExecuteTargetsValue");
        }

        [TestClass]
        public class TheLoadMethod : OnErrorTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadEmptyItemDefinitionElement()
            {
                //Arraneg
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<OnError />" +
                                "</Target>" +
                            "</Project>";

                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOnError = root.Targets.Single().OnErrors.Single();
                actualOnError.ExecuteTargetsAttribute.Should().BeNull();
            }
            [TestMethod]
            public void ShouldLoadExecuteTargetsAttributeAttribute()
            {
                string expectedExecuteTargetsAttributeValue = "TestExecuteTargetsAttributeValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<OnError ExecuteTargets=\"" + expectedExecuteTargetsAttributeValue + "\" />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOnError = root.Targets.Single().OnErrors.Single();
                actualOnError.ExecuteTargetsAttribute.Should().Be(expectedExecuteTargetsAttributeValue);
            }
            [TestMethod]
            public void ShouldLoadConditionAttribute()
            {
                string expectedConditionValue = "TestConditionValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<OnError Condition=\"" + expectedConditionValue + "\" />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOnError = root.Targets.Single().OnErrors.Single();
                actualOnError.Condition.Should().Be(expectedConditionValue);
            }
            [TestMethod]
            public void ShouldLoadLabelAttribute()
            {
                string expectedLabelValue = "TestLabelValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<OnError Label=\"" + expectedLabelValue + "\" />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOnError = root.Targets.Single().OnErrors.Single();
                actualOnError.Label.Should().Be(expectedLabelValue);
            }
        }
        [TestClass]
        public class TheSaveMethod : OnErrorTest
        {
            [TestMethod]
            public void ShouldWriteTheExecuteTargetsAttribute()
            {
                StringWriter stringWriter;
                string expectedExecuteTargetsValue;
                //Arrange
                expectedExecuteTargetsValue = "TestExecuteTargetsValue";
                unit.ExecuteTargetsAttribute = expectedExecuteTargetsValue;
                var target = root.AddTarget(null).Element;
                target.AppendChild(unit);
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("ExecuteTargets", expectedExecuteTargetsValue));
            }
            [TestMethod]
            public void ShouldWriteTheLabelAttribute()
            {
                StringWriter stringWriter;
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                var target = root.AddTarget(null).Element;
                target.AppendChild(unit);
                unit.Label = expectedLabelValue;
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
                var target = root.AddTarget(null).Element;
                target.AppendChild(unit);
                unit.Condition = expectedConditionValue;
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