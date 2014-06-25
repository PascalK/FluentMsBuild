using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class UsingTaskBodyTest
    {
        UsingTaskBody unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateUsingTaskBodyElement(null, null);
        }

        [TestClass]
        public class TheConditionProperty : UsingTaskBodyTest
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
        public class TheEvaluateProperty : UsingTaskBodyTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Evaluate = null;
                //Act
                var actualEvaluate = unit.Evaluate;
                //Assert
                actualEvaluate.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheTaskBodyProperty : UsingTaskBodyTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.TaskBody = null;
                //Act
                var actualTaskBody = unit.TaskBody;
                //Assert
                actualTaskBody.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheLoadMethod : UsingTaskBodyTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadLabelAttribute()
            {
                string expectedLabelValue = "TestLabelValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask>" +
                                    "<Task Label=\"" + expectedLabelValue + "\" />" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTaskBody = root.UsingTasks.Single().TaskBody;
                actualUsingTaskBody.Label.Should().Be(expectedLabelValue);
            }
            [TestMethod]
            public void ShouldLoadEvaluateAttribute()
            {
                string expectedEvaluateValue = "TestEvaluateValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask>" +
                                    "<Task Evaluate=\"" + expectedEvaluateValue + "\" />" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTaskBody = root.UsingTasks.Single().TaskBody;
                actualUsingTaskBody.Evaluate.Should().Be(expectedEvaluateValue);
            }
            [TestMethod]
            public void ShouldLoadInnerXmlAsTaskBody()
            {
                string expectedTaskBodyValue = "TestTaskBodyValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask>" +
                                    "<Task>" +
                                        expectedTaskBodyValue +
                                    "</Task>" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTaskBody = root.UsingTasks.Single().TaskBody;
                actualUsingTaskBody.TaskBody.Should().Be(expectedTaskBodyValue);
            }
        }
        [TestClass]
        public class TheSaveMethod : UsingTaskBodyTest
        {
            [TestMethod]
            public void ShouldWriteTheLabelAttribute()
            {
                StringWriter stringWriter;
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.AddUsingTaskBody(null, null).Element.Label = expectedLabelValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Label", expectedLabelValue));
            }
            [TestMethod]
            public void ShouldWriteTheEvaluateAttribute()
            {
                StringWriter stringWriter;
                string expectedEvaluateValue;
                //Arrange
                expectedEvaluateValue = "TestEvaluateValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.AddUsingTaskBody(null, null).Element.Evaluate = expectedEvaluateValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Evaluate", expectedEvaluateValue));
            }
            [TestMethod]
            public void ShouldWriteTaskBodyAsInnerXml()
            {
                StringWriter stringWriter;
                string expectedTaskBodyValue;
                //Arrange
                expectedTaskBodyValue = "TestTaskBodyValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.AddUsingTaskBody(null, null).Element.TaskBody = expectedTaskBodyValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(expectedTaskBodyValue);
            }

            string XmlAttribute(string name, string value)
            {
                return string.Format("{0}=\"{1}\"", name, value);
            }
        }
    }
}