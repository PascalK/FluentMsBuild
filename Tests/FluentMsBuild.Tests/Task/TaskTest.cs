using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class TaskTest
    {
        Task unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateTaskElement("InitialTestTaskNameValue");
        }

        [TestClass]
        public class TheContinueOnErrorProperty : TaskTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.ContinueOnError = null;
                //Act
                var actualContinueOnError = unit.ContinueOnError;
                //Assert
                actualContinueOnError.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheNameProperty : TaskTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit = root.CreateTaskElement(null);
                //Act
                var actualName = unit.Name;
                //Assert
                actualName.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheAddOutputItemMethod : TaskTest
        {
            [TestMethod]
            public void ShouldAddTheReturnedOutputItemToTheOutputsPropertyWithCorrespondingTaskParameterAndItemName()
            {
                string expectedTaskParameter;
                string expectedItemName;
                //Arrange
                expectedTaskParameter = "TestTaskParameterValue";
                expectedItemName = "TestItemTypeValue";
                //Act
                var actualAddedOutput = unit.AddOutputItem(expectedTaskParameter, expectedItemName).Element;
                //Assert
                var actualOutput = unit.Outputs.Single();
                actualOutput.Should().Be(actualAddedOutput);
                actualOutput.TaskParameter.Should().Be(expectedTaskParameter);
                actualOutput.ItemName.Should().Be(expectedItemName);
                actualOutput.Condition.Should().BeEmpty();
                actualOutput.PropertyName.Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldAddTheReturnedOutputItemToTheOutputsPropertyWithCorrespondingTaskParameterItemNameAndCondition()
            {
                string expectedTaskParameter;
                string expectedItemName;
                string expectedCondition;
                //Arrange
                expectedTaskParameter = "TestTaskParameterValue";
                expectedItemName = "TestItemTypeValue";
                expectedCondition = "TestConditionValue";
                //Act
                var actualAddedOutput = unit.AddOutputItem(expectedTaskParameter, expectedItemName, expectedCondition).Element;
                //Assert
                var actualOutput = unit.Outputs.Single();
                actualOutput.Should().Be(actualAddedOutput);
                actualOutput.TaskParameter.Should().Be(expectedTaskParameter);
                actualOutput.ItemName.Should().Be(expectedItemName);
                actualOutput.Condition.Should().Be(expectedCondition);
                actualOutput.PropertyName.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheAddOutputPropertyMethod : TaskTest
        {
            [TestMethod]
            public void ShouldAddTheReturnedOutputItemToTheOutputsPropertyWithCorrespondingTaskParameterAndPropertyName()
            {
                string expectedTaskParameter;
                string expectedPropertyName;
                //Arrange
                expectedTaskParameter = "TestTaskParameterValue";
                expectedPropertyName = "TestPropertyNameValue";
                //Act
                var actualAddedOutput = unit.AddOutputProperty(expectedTaskParameter, expectedPropertyName).Element;
                //Assert
                var actualOutput = unit.Outputs.Single();
                actualOutput.Should().Be(actualAddedOutput);
                actualOutput.TaskParameter.Should().Be(expectedTaskParameter);
                actualOutput.ItemName.Should().BeEmpty();
                actualOutput.Condition.Should().BeEmpty();
                actualOutput.PropertyName.Should().Be(expectedPropertyName);
            }
            [TestMethod]
            public void ShouldAddTheReturnedOutputItemToTheOutputsPropertyWithCorrespondingTaskParameterPropertyNameAndCondition()
            {
                string expectedTaskParameter;
                string expectedPropertyName;
                string expectedCondition;
                //Arrange
                expectedTaskParameter = "TestTaskParameterValue";
                expectedPropertyName = "TestPropertyNameValue";
                expectedCondition = "TestConditionValue";
                //Act
                var actualAddedOutput = unit.AddOutputProperty(expectedTaskParameter, expectedPropertyName, expectedCondition).Element;
                //Assert
                var actualOutput = unit.Outputs.Single();
                actualOutput.Should().Be(actualAddedOutput);
                actualOutput.TaskParameter.Should().Be(expectedTaskParameter);
                actualOutput.ItemName.Should().BeEmpty();
                actualOutput.Condition.Should().Be(expectedCondition);
                actualOutput.PropertyName.Should().Be(expectedPropertyName);
            }
        }
        [TestClass]
        public class TheGetParameterMethod : TaskTest
        {
            [TestMethod]
            public void ShouldReturnTheParameterValueCorrespondingToTheName()
            {
                string expectedParameterName;
                string expectedParameterValue;
                //Arrange
                expectedParameterName = "TestParameterNameValue";
                expectedParameterValue = "TestParameterValueValue";
                unit.Parameters.Add(expectedParameterName, expectedParameterValue);
                //Act
                var actualParameterValue = unit.GetParameter(expectedParameterName);
                //Assert
                actualParameterValue.Should().Be(expectedParameterValue);
            }
            [TestMethod]
            public void ShouldReturnAnEmptyStringWhenTheParameterNameDoesNotExist()
            {
                //Arrange
                //Act
                var actualParameterValue = unit.GetParameter("NonExisitngParameterName");
                //Assert
                actualParameterValue.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheRemoveAllParametersMethod : TaskTest
        {
            [TestMethod]
            public void ShouldClearAllParameters()
            {
                //Arrange
                unit.Parameters.Add("RandomParameterName", "RandomParameterValue");
                //Act
                unit.RemoveAllParameters();
                //Assert
                unit.Parameters.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheRemoveParameterMethod : TaskTest
        {
            [TestMethod]
            public void ShouldRemoveTheParameterWithTheCorrespondingName()
            {
                string expectedParameterName;
                //Arrange
                expectedParameterName = "TestParameterNameValue";
                unit.Parameters.Add(expectedParameterName, "RandomValue");
                //Act
                unit.RemoveParameter(expectedParameterName);
                //Assert
                unit.Parameters.Should().BeEmpty(because: "the only existing parameters should just have been removed");
            }
        }
        [TestClass]
        public class TheSetParameterMethod : TaskTest
        {
            [TestMethod]
            public void ShouldAddTheParameterWhenItDoesNotYetExist()
            {
                string expectedParameterName;
                string expectedParameterValue;
                //Arrange
                expectedParameterName = "TestParameterNameValue";
                expectedParameterValue = "TestParameterValueValue";
                //Act
                unit.SetParameter(expectedParameterName, expectedParameterValue);
                //Assert
                var actualParameter = unit.Parameters.Single();
                actualParameter.Key.Should().Be(expectedParameterName);
                actualParameter.Value.Should().Be(expectedParameterValue);
            }
        }
        [TestClass]
        public class TheLoadMethod : TaskTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadContinueOnErrorAttribute()
            {
                string expectedContinueOnErrorValue;
                //Arrange
                expectedContinueOnErrorValue = "TestContinueOnErrorValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<Task ContinueOnError=\"" + expectedContinueOnErrorValue + "\" />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTask = root.Targets.Single().Tasks.Single();
                actualTask.ContinueOnError.Should().Be(expectedContinueOnErrorValue);
            }
            //[TestMethod]
            //public void ShouldLoadExecuteTargetsAttribute()
            //{
            //    string expectedExecuteTargetsValue;
            //    //Arrange
            //    expectedExecuteTargetsValue = "TestExecuteTargetsValue";
            //    actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            //                "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
            //                    "<Target>" +
            //                        "<Task ExecuteTargets=\"" + expectedExecuteTargetsValue + "\" />" +
            //                    "</Target>" +
            //                "</Project>";
            //    reader = XmlReader.Create(new StringReader(actualXml));
            //    //Act
            //    root = Root.Create(reader);
            //    //Assert
            //    var actualTask = root.Targets.Single().Tasks.Single();
            //    actualTask.ExecuteTargets.Should().Be(expectedExecuteTargetsValue);
            //}
            [TestMethod]
            public void ShouldLoadMSBuildArchitectureAttribute()
            {
                string expectedMSBuildArchitectureValue;
                //Arrange
                expectedMSBuildArchitectureValue = "TestMSBuildArchitectureValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<Task MSBuildArchitecture=\"" + expectedMSBuildArchitectureValue + "\" />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTask = root.Targets.Single().Tasks.Single();
                actualTask.MSBuildArchitecture.Should().Be(expectedMSBuildArchitectureValue);
            }
            [TestMethod]
            public void ShouldLoadMSBuildRuntimeAttribute()
            {
                string expectedMSBuildRuntimeValue;
                //Arrange
                expectedMSBuildRuntimeValue = "TestMSBuildRuntimeValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<Task MSBuildRuntime=\"" + expectedMSBuildRuntimeValue + "\" />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTask = root.Targets.Single().Tasks.Single();
                actualTask.MSBuildRuntime.Should().Be(expectedMSBuildRuntimeValue);
            }
            [TestMethod]
            public void ShouldLoadLabelAttribute()
            {
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<Task Label=\"" + expectedLabelValue + "\" />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTask = root.Targets.Single().Tasks.Single();
                actualTask.Label.Should().Be(expectedLabelValue);
            }
            [TestMethod]
            public void ShouldLoadConditionAttribute()
            {
                string expectedConditionValue;
                //Arrange
                expectedConditionValue = "TestConditionValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<Task Condition=\"" + expectedConditionValue + "\" />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTask = root.Targets.Single().Tasks.Single();
                actualTask.Condition.Should().Be(expectedConditionValue);
            }
            [TestMethod]
            public void ShouldLoadUnknownAttributesAsParameters()
            {
                string expectedParameterName;
                string expectedParameterValue;
                //Arrange
                expectedParameterName = "TestParameterNameValue";
                expectedParameterValue = "TestParameterValueValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<Task " + expectedParameterName + "=\"" + expectedParameterValue + "\" />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTask = root.Targets.Single().Tasks.Single();
                actualTask.Parameters[expectedParameterName].Should().Be(expectedParameterValue);
            }
            [TestMethod]
            public void ShouldLoadOutputTagAsAnOutputElement()
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
                var actualTask = root.Targets.Single().Tasks.Single();
                actualTask.Outputs.Single();
            }
            [TestMethod]
            public void ShouldFailWhenElementIsUnknown()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<Task>" +
                                        "<UnknownTag />" +
                                    "</Task>" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = () => Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidProjectFileException>();
            }
        }
        [TestClass]
        public class TheSaveMethod : TaskTest
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
                target.AddTask("InitialTestTaskName").Element.Label = expectedLabelValue;
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
                var target = root.AddTarget(null).Element;
                target.AddTask("InitialTestTaskName").Element.Condition = expectedConditionValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Condition", expectedConditionValue));
            }
            [TestMethod]
            public void ShouldWriteTheContinueOnErrorAttribute()
            {
                StringWriter stringWriter;
                string expectedContinueOnErrorValue;
                //Arrange
                expectedContinueOnErrorValue = "TestContinueOnErrorValue";
                root = Root.Create();
                var target = root.AddTarget(null).Element;
                target.AddTask("InitialTestTaskName").Element.ContinueOnError = expectedContinueOnErrorValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("ContinueOnError", expectedContinueOnErrorValue));
            }
            //[TestMethod]
            //public void ShouldWriteTheExecuteTargetsAttribute()
            //{
            //    StringWriter stringWriter;
            //    string expectedExecuteTargetsValue;
            //    //Arrange
            //    expectedExecuteTargetsValue = "TestExecuteTargetsValue";
            //    root = Root.Create();
            //    var target = root.AddTarget(null).Element;
            //    target.AddTask("InitialTestTaskName").Element.ExecuteTargets = expectedExecuteTargetsValue;
            //    stringWriter = new StringWriter();
            //    //Act
            //    root.Save(stringWriter);
            //    //Assert
            //    var actualXml = stringWriter.ToString();
            //    actualXml.Should().Contain(XmlAttribute("ExecuteTargets", expectedExecuteTargetsValue));
            //}
            [TestMethod]
            public void ShouldWriteTheMSBuildArchitectureAttribute()
            {
                StringWriter stringWriter;
                string expectedMSBuildArchitectureValue;
                //Arrange
                expectedMSBuildArchitectureValue = "TestMSBuildArchitectureValue";
                root = Root.Create();
                var target = root.AddTarget(null).Element;
                target.AddTask("InitialTestTaskName").Element.MSBuildArchitecture = expectedMSBuildArchitectureValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("MSBuildArchitecture", expectedMSBuildArchitectureValue));
            }
            [TestMethod]
            public void ShouldWriteTheMSBuildRuntimeAttribute()
            {
                StringWriter stringWriter;
                string expectedMSBuildRuntimeValue;
                //Arrange
                expectedMSBuildRuntimeValue = "TestMSBuildRuntimeValue";
                root = Root.Create();
                var target = root.AddTarget(null).Element;
                target.AddTask("InitialTestTaskName").Element.MSBuildRuntime = expectedMSBuildRuntimeValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("MSBuildRuntime", expectedMSBuildRuntimeValue));
            }
            [TestMethod]
            public void ShouldWriteParametersAsAttributes()
            {
                StringWriter stringWriter;
                string expectedParameterName;
                string expectedParameterValue;
                //Arrange
                expectedParameterName = "TestParameterNameValue";
                expectedParameterValue = "TestParameterValueValue";
                root = Root.Create();
                var target = root.AddTarget(null).Element;
                var task = target.AddTask("InitialTestTaskName").Element;
                task.Parameters.Add(expectedParameterName, expectedParameterValue);
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute(expectedParameterName, expectedParameterValue));
            }
            string XmlAttribute(string name, string value)
            {
                return string.Format("{0}=\"{1}\"", name, value);
            }
        }
    }
}
