using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class OutputTest
    {
        Output unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            //unit = root.CreateOutputElement("InitialTestTaskParameterValue", "InitialTestItemNameValue", "InitialTestPropertyName");
            unit = root.CreateOutputElement(null, null, null);
        }

        [TestClass]
        public class TheItemNameProperty : OutputTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.ItemName = null;
                //Act
                var actualItemName = unit.ItemName;
                //Assert
                actualItemName.Should().BeEmpty();
            }
        }
        [TestClass]
        public class ThePropertyNameProperty : OutputTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.PropertyName = null;
                //Act
                var actualPropertyName = unit.PropertyName;
                //Assert
                actualPropertyName.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheTaskParameterProperty : OutputTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.TaskParameter = null;
                //Act
                var actualTaskParameter = unit.TaskParameter;
                //Assert
                actualTaskParameter.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheIsOutputItemProperty : OutputTest
        {
            [TestMethod]
            public void ShouldBeFalseWhenItemNameHasNoValue()
            {
                //Arrange
                //Act
                var actualResult = unit.IsOutputItem;
                //Assert
                actualResult.Should().BeFalse();
            }
            [TestMethod]
            public void ShouldBeTrueWhenItemNameHasAValue()
            {
                //Arrange
                unit.ItemName = "RandomValue";
                //Act
                var actualResult = unit.IsOutputItem;
                //Assert
                actualResult.Should().BeTrue();
            }
        }
        [TestClass]
        public class TheIsOutputPropertyProperty : OutputTest
        {
            [TestMethod]
            public void ShouldBeFalseWhenPropertyNameHasNoValue()
            {
                //Arrange
                //Act
                var actualResult = unit.IsOutputProperty;
                //Assert
                actualResult.Should().BeFalse();
            }
            [TestMethod]
            public void ShouldBeTrueWhenPropertyNameHasAValue()
            {
                //Arrange
                unit.PropertyName = "RandomValue";
                //Act
                var actualResult = unit.IsOutputProperty;
                //Assert
                actualResult.Should().BeTrue();
            }
        }
        [TestClass]
        public class TheLoadMethod : OutputTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadAnEmptyOutputElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<Task>" +
                                        "<Output />" +
                                    "</Task>" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOutput = root.Targets.Single().Tasks.Single().Outputs.Single();
                actualOutput.ItemName.Should().BeEmpty();
                actualOutput.PropertyName.Should().BeEmpty();
                actualOutput.TaskParameter.Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldLoadItemNameAttribute()
            {
                string actualItemName;
                //Arrange
                actualItemName = "TestItemNameValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<Task>" +
                                        "<Output ItemName=\"" + actualItemName + "\" />" +
                                    "</Task>" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOutput = root.Targets.Single().Tasks.Single().Outputs.Single();
                actualOutput.ItemName.Should().Be(actualItemName);
            }
            [TestMethod]
            public void ShouldLoadPropertyNameAttribute()
            {
                string actualPropertyName;
                //Arrange
                actualPropertyName = "TestPropertyNameValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<Task>" +
                                        "<Output PropertyName=\"" + actualPropertyName + "\" />" +
                                    "</Task>" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOutput = root.Targets.Single().Tasks.Single().Outputs.Single();
                actualOutput.PropertyName.Should().Be(actualPropertyName);
            }
            [TestMethod]
            public void ShouldLoadTaskParameterAttribute()
            {
                string actualTaskParameter;
                //Arrange
                actualTaskParameter = "TestTaskParameterValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<Task>" +
                                        "<Output TaskParameter=\"" + actualTaskParameter + "\" />" +
                                    "</Task>" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualOutput = root.Targets.Single().Tasks.Single().Outputs.Single();
                actualOutput.TaskParameter.Should().Be(actualTaskParameter);
            }
        }
        [TestClass]
        public class TheSaveMethod : OutputTest
        {
            [TestMethod]
            public void ShouldWriteTheLabelAttribute()
            {
                StringWriter stringWriter;
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                root = Root.Create();
                var target = root.AddTarget(null).Element;
                var task = target.AddTask("Task").Element;
                task.AddOutputItem(null, null).Element.Label = expectedLabelValue;
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
                var task = target.AddTask("Task").Element;
                task.AddOutputItem(null, null).Element.Condition = expectedConditionValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Condition", expectedConditionValue));
            }
            [TestMethod]
            public void ShouldWriteTheTaskParameterAttribute()
            {
                StringWriter stringWriter;
                string expectedTaskParameterValue;
                //Arrange
                expectedTaskParameterValue = "TestTaskParameterValue";
                var target = root.AddTarget(null).Element;
                var task = target.AddTask("Task").Element;
                task.AddOutputItem(null, null).Element.TaskParameter = expectedTaskParameterValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("TaskParameter", expectedTaskParameterValue));
            }
            [TestMethod]
            public void ShouldWriteThePropertyNameAttribute()
            {
                StringWriter stringWriter;
                string expectedPropertyNameValue;
                //Arrange
                expectedPropertyNameValue = "TestPropertyNameValue";
                var target = root.AddTarget(null).Element;
                var task = target.AddTask("Task").Element;
                task.AddOutputItem(null, null).Element.PropertyName = expectedPropertyNameValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("PropertyName", expectedPropertyNameValue));
            }
            [TestMethod]
            public void ShouldWriteTheItemNameAttribute()
            {
                StringWriter stringWriter;
                string expectedItemNameValue;
                //Arrange
                expectedItemNameValue = "TestItemNameValue";
                var target = root.AddTarget(null).Element;
                var task = target.AddTask("Task").Element;
                task.AddOutputItem(null, null).Element.ItemName = expectedItemNameValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("ItemName", expectedItemNameValue));
            }

            string XmlAttribute(string name, string value)
            {
                return string.Format("{0}=\"{1}\"", name, value);
            }
        }
    }
}
