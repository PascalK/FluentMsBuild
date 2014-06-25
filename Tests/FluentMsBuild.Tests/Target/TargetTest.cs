using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class TargetTest
    {
        Target unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateTargetElement("InitialTestTargetNameValue");
        }
        [TestClass]
        public class TheAfterTargetsProperty : TargetTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.AfterTargets = null;
                //Act
                var actualAfterTargets = unit.AfterTargets;
                //Assert
                actualAfterTargets.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheBeforeTargetsProperty : TargetTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.BeforeTargets = null;
                //Act
                var actualBeforeTargets = unit.BeforeTargets;
                //Assert
                actualBeforeTargets.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheDependsOnTargetsProperty : TargetTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.DependsOnTargets = null;
                //Act
                var actualDependsOnTargets = unit.DependsOnTargets;
                //Assert
                actualDependsOnTargets.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheInputsProperty : TargetTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Inputs = null;
                //Act
                var actualInputs = unit.Inputs;
                //Assert
                actualInputs.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheKeepDuplicateOutputsProperty : TargetTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.KeepDuplicateOutputs = null;
                //Act
                var actualKeepDuplicateOutputs = unit.KeepDuplicateOutputs;
                //Assert
                actualKeepDuplicateOutputs.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheNameProperty : TargetTest
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
        public class TheOutputsProperty : TargetTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Outputs = null;
                //Act
                var actualOutputs = unit.Outputs;
                //Assert
                actualOutputs.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheReturnsProperty : TargetTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Returns = null;
                //Act
                var actualReturns = unit.Returns;
                //Assert
                actualReturns.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheAddItemGroupMethod : TargetTest
        {
            [TestMethod]
            public void ShouldAddTheReturnedItemGroupToTheItemGroupsProperty()
            {
                //Arrange
                //Act
                var actualItemGroup = unit.AddItemGroup().Element;
                //Assert
                unit.ItemGroups.Single().Should().Be(actualItemGroup);
            }
        }
        [TestClass]
        public class TheAddPropertyGroupMethod : TargetTest
        {
            [TestMethod]
            public void ShouldAddTheReturnedPropertyGroupToThePropertyGroupsProperty()
            {
                //Arrange
                //Act
                var actualPropertyGroup = unit.AddPropertyGroup().Element;
                //Assert
                unit.PropertyGroups.Single().Should().Be(actualPropertyGroup);
            }
        }
        [TestClass]
        public class TheAddTaskMethod : TargetTest
        {
            [TestMethod]
            public void ShouldAddTheReturnedTaskToTheTasksProperty()
            {
                string expectedTaskName;
                //Arrange
                expectedTaskName = "TestTaskNameValue";
                //Act
                var actualAddedTask = unit.AddTask(expectedTaskName).Element;
                //Assert
                var actualTask = unit.Tasks.Single();
                actualAddedTask.Should().Be(actualTask);
                actualAddedTask.Name.Should().Be(expectedTaskName);
            }
        }
        [TestClass]
        public class TheLoadMethod : TargetTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldAddEmptyTargets()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                root.Targets.Single();
            }
            [TestMethod]
            public void ShouldLoadNameAttribute()
            {
                string expectedNameValue;
                //Arrange
                expectedNameValue = "TestNameValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target Name=\"" + expectedNameValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTarget = root.Targets.Single();
                actualTarget.Name.Should().Be(expectedNameValue);
            }
            [TestMethod]
            public void ShouldLoadDependsOnTargetsAttribute()
            {
                string expectedDependsOnTargetsValue;
                //Arrange
                expectedDependsOnTargetsValue = "TestDependsOnTargetsValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target DependsOnTargets=\"" + expectedDependsOnTargetsValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTarget = root.Targets.Single();
                actualTarget.DependsOnTargets.Should().Be(expectedDependsOnTargetsValue);
            }
            [TestMethod]
            public void ShouldLoadReturnsAttribute()
            {
                string expectedReturnsValue;
                //Arrange
                expectedReturnsValue = "TestReturnsValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target Returns=\"" + expectedReturnsValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTarget = root.Targets.Single();
                actualTarget.Returns.Should().Be(expectedReturnsValue);
            }
            [TestMethod]
            public void ShouldLoadInputsAttribute()
            {
                string expectedInputsValue;
                //Arrange
                expectedInputsValue = "TestInputsValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target Inputs=\"" + expectedInputsValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTarget = root.Targets.Single();
                actualTarget.Inputs.Should().Be(expectedInputsValue);
            }
            [TestMethod]
            public void ShouldLoadOutputsAttribute()
            {
                string expectedOutputsValue;
                //Arrange
                expectedOutputsValue = "TestOutputsValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target Outputs=\"" + expectedOutputsValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTarget = root.Targets.Single();
                actualTarget.Outputs.Should().Be(expectedOutputsValue);
            }
            [TestMethod]
            public void ShouldLoadBeforeTargetsAttribute()
            {
                string expectedBeforeTargetsValue;
                //Arrange
                expectedBeforeTargetsValue = "TestBeforeTargetsValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target BeforeTargets=\"" + expectedBeforeTargetsValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTarget = root.Targets.Single();
                actualTarget.BeforeTargets.Should().Be(expectedBeforeTargetsValue);
            }
            [TestMethod]
            public void ShouldLoadAfterTargetsAttribute()
            {
                string expectedAfterTargetsValue;
                //Arrange
                expectedAfterTargetsValue = "TestAfterTargetsValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target AfterTargets=\"" + expectedAfterTargetsValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTarget = root.Targets.Single();
                actualTarget.AfterTargets.Should().Be(expectedAfterTargetsValue);
            }
            [TestMethod]
            public void ShouldLoadKeepDuplicateOutputsAttribute()
            {
                string expectedKeepDuplicateOutputsValue;
                //Arrange
                expectedKeepDuplicateOutputsValue = "TestKeepDuplicateOutputsValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target KeepDuplicateOutputs=\"" + expectedKeepDuplicateOutputsValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTarget = root.Targets.Single();
                actualTarget.KeepDuplicateOutputs.Should().Be(expectedKeepDuplicateOutputsValue);
            }
            [TestMethod]
            public void ShouldLoadLabelAttribute()
            {
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target Label=\"" + expectedLabelValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTarget = root.Targets.Single();
                actualTarget.Label.Should().Be(expectedLabelValue);
            }
            [TestMethod]
            public void ShouldLoadConditionAttribute()
            {
                string expectedConditionValue;
                //Arrange
                expectedConditionValue = "TestConditionValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target Condition=\"" + expectedConditionValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTarget = root.Targets.Single();
                actualTarget.Condition.Should().Be(expectedConditionValue);
            }
            [TestMethod]
            public void ShouldLoadOnErrorTagAsAnOnErrorElement()
            {
                //Arrange
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
                root.Targets.Single().OnErrors.Single();
            }
            [TestMethod]
            public void ShouldLoadPropertyGroupTagAsAnPropertyGroupElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<PropertyGroup />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                root.Targets.Single().PropertyGroups.Single();
            }
            [TestMethod]
            public void ShouldLoadItemGroupTagAsAnItemGroupElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<ItemGroup />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                root.Targets.Single().ItemGroups.Single();
            }
            [TestMethod]
            public void ShouldLoadUnknownTagAsAnTaskElementWithACorrespondingName()
            {
                string expectedTaskName;
                //Arrange
                expectedTaskName = "TestTaskNameValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<Target>" +
                                    "<" + expectedTaskName + " />" +
                                "</Target>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTask = root.Targets.Single().Tasks.Single();
                actualTask.Name.Should().Be(expectedTaskName);
            }
        }
        [TestClass]
        public class TheSaveMethod : TargetTest
        {
            [TestMethod]
            public void ShouldWriteTheLabelAttribute()
            {
                StringWriter stringWriter;
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                root = Root.Create();
                root.AddTarget(null).Element.Label = expectedLabelValue;
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
                root.AddTarget(null).Element.Condition = expectedConditionValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Condition", expectedConditionValue));
            }
            [TestMethod]
            public void ShouldWriteTheNameAttribute()
            {
                StringWriter stringWriter;
                string expectedNameValue;
                //Arrange
                expectedNameValue = "TestNameValue";
                root = Root.Create();
                root.AddTarget(null).Element.Name = expectedNameValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Name", expectedNameValue));
            }
            [TestMethod]
            public void ShouldWriteTheDependsOnTargetsAttribute()
            {
                StringWriter stringWriter;
                string expectedDependsOnTargetsValue;
                //Arrange
                expectedDependsOnTargetsValue = "TestDependsOnTargetsValue";
                root = Root.Create();
                root.AddTarget(null).Element.DependsOnTargets = expectedDependsOnTargetsValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("DependsOnTargets", expectedDependsOnTargetsValue));
            }
            [TestMethod]
            public void ShouldWriteTheReturnsAttribute()
            {
                StringWriter stringWriter;
                string expectedReturnsValue;
                //Arrange
                expectedReturnsValue = "TestReturnsValue";
                root = Root.Create();
                root.AddTarget(null).Element.Returns = expectedReturnsValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Returns", expectedReturnsValue));
            }
            [TestMethod]
            public void ShouldWriteTheInputsAttribute()
            {
                StringWriter stringWriter;
                string expectedInputsValue;
                //Arrange
                expectedInputsValue = "TestInputsValue";
                root = Root.Create();
                root.AddTarget(null).Element.Inputs = expectedInputsValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Inputs", expectedInputsValue));
            }
            [TestMethod]
            public void ShouldWriteTheOutputsAttribute()
            {
                StringWriter stringWriter;
                string expectedOutputsValue;
                //Arrange
                expectedOutputsValue = "TestOutputsValue";
                root = Root.Create();
                root.AddTarget(null).Element.Outputs = expectedOutputsValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Outputs", expectedOutputsValue));
            }
            [TestMethod]
            public void ShouldWriteTheBeforeTargetsAttribute()
            {
                StringWriter stringWriter;
                string expectedBeforeTargetsValue;
                //Arrange
                expectedBeforeTargetsValue = "TestBeforeTargetsValue";
                root = Root.Create();
                root.AddTarget(null).Element.BeforeTargets = expectedBeforeTargetsValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("BeforeTargets", expectedBeforeTargetsValue));
            }
            [TestMethod]
            public void ShouldWriteTheAfterTargetsAttribute()
            {
                StringWriter stringWriter;
                string expectedAfterTargetsValue;
                //Arrange
                expectedAfterTargetsValue = "TestAfterTargetsValue";
                root = Root.Create();
                root.AddTarget(null).Element.AfterTargets = expectedAfterTargetsValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("AfterTargets", expectedAfterTargetsValue));
            }
            [TestMethod]
            public void ShouldWriteTheKeepDuplicateOutputsAttribute()
            {
                StringWriter stringWriter;
                string expectedKeepDuplicateOutputsValue;
                //Arrange
                expectedKeepDuplicateOutputsValue = "TestKeepDuplicateOutputsValue";
                root = Root.Create();
                root.AddTarget(null).Element.KeepDuplicateOutputs = expectedKeepDuplicateOutputsValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("KeepDuplicateOutputs", expectedKeepDuplicateOutputsValue));
            }
            string XmlAttribute(string name, string value)
            {
                return string.Format("{0}=\"{1}\"", name, value);
            }
        }
    }
}
