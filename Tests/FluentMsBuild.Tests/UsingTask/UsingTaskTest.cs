using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class UsingTaskTest
    {
        UsingTask unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateUsingTaskElement(null, null, null);
        }

        [TestClass]
        public class TheAssemblyFileProperty : UsingTaskTest
        {

            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.AssemblyFile = null;
                //Act
                var actualAssemblyFile = unit.AssemblyFile;
                //Assert
                actualAssemblyFile.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheAssemblyNameProperty : UsingTaskTest
        {

            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.AssemblyName = null;
                //Act
                var actualAssemblyName = unit.AssemblyName;
                //Assert
                actualAssemblyName.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheTaskFactoryProperty : UsingTaskTest
        {

            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.TaskFactory = null;
                //Act
                var actualTaskFactory = unit.TaskFactory;
                //Assert
                actualTaskFactory.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheTaskNameProperty : UsingTaskTest
        {

            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.TaskName = null;
                //Act
                var actualTaskName = unit.TaskName;
                //Assert
                actualTaskName.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheAddParameterGroupMethod : UsingTaskTest
        {
            [TestMethod]
            public void ShouldAddTheReturnedParameterGroupToTheParameterGroupProperty()
            {
                //Arrange
                //Act
                var actualParameterGroup = unit.AddParameterGroup().Element;
                //Assert
                unit.ParameterGroup.Should().Be(actualParameterGroup);
            }
        }
        [TestClass]
        public class TheAddUsingTaskBodyMethod : UsingTaskTest
        {
            [TestMethod]
            public void ShouldAddTheReturnedUsingTaskBodyToTheUsingTaskBodyProperty()
            {
                //Arrange
                //Act
                var actualUsingTaskBody = unit.AddUsingTaskBody(null, null).Element;
                //Assert
                unit.TaskBody.Should().Be(actualUsingTaskBody);
            }
        }
        [TestClass]
        public class TheLoadMethod : UsingTaskTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadConditionAttribute()
            {
                string expectedConditionValue = "TestConditionValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask Condition=\"" + expectedConditionValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTask = root.UsingTasks.Single();
                actualUsingTask.Condition.Should().Be(expectedConditionValue);
            }
            [TestMethod]
            public void ShouldLoadLabelAttribute()
            {
                string expectedLabelValue = "TestLabelValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask Label=\"" + expectedLabelValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTask = root.UsingTasks.Single();
                actualUsingTask.Label.Should().Be(expectedLabelValue);
            }
            [TestMethod]
            public void ShouldLoadAssemblyNameAttribute()
            {
                string expectedAssemblyNameValue = "TestAssemblyNameValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask AssemblyName=\"" + expectedAssemblyNameValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTask = root.UsingTasks.Single();
                actualUsingTask.AssemblyName.Should().Be(expectedAssemblyNameValue);
            }
            [TestMethod]
            public void ShouldLoadAssemblyFileAttribute()
            {
                string expectedAssemblyFileValue = "TestAssemblyFileValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask AssemblyFile=\"" + expectedAssemblyFileValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTask = root.UsingTasks.Single();
                actualUsingTask.AssemblyFile.Should().Be(expectedAssemblyFileValue);
            }
            [TestMethod]
            public void ShouldLoadTaskFactoryAttribute()
            {
                string expectedTaskFactoryValue = "TestTaskFactoryValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask TaskFactory=\"" + expectedTaskFactoryValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTask = root.UsingTasks.Single();
                actualUsingTask.TaskFactory.Should().Be(expectedTaskFactoryValue);
            }
            [TestMethod]
            public void ShouldLoadTaskNameAttribute()
            {
                string expectedTaskNameValue = "TestTaskNameValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask TaskName=\"" + expectedTaskNameValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTask = root.UsingTasks.Single();
                actualUsingTask.TaskName.Should().Be(expectedTaskNameValue);
            }
            [TestMethod]
            public void ShouldLoadParameterGroupTagAsUsingTaskParameterGroupElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask>" +
                                    "<ParameterGroup />" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualParameterGroup = root.UsingTasks.Single().ParameterGroup;
                actualParameterGroup.Should().NotBeNull();
            }
            [TestMethod]
            public void ShouldLoadTaskTagAsUsingTaskBodyElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask>" +
                                    "<UnknownTagName />" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = () => Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidProjectFileException>();
            }
            [TestMethod]
            public void ShouldFailLoadingAnUnknownTag()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask>" +
                                    "<Task />" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualTaskBody = root.UsingTasks.Single().TaskBody;
                actualTaskBody.Should().NotBeNull();
            }
        }
        [TestClass]
        public class TheSaveMethod : UsingTaskTest
        {
            [TestMethod]
            public void ShouldWriteTheLabelAttribute()
            {
                StringWriter stringWriter;
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.Label = expectedLabelValue;
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
                root.AddUsingTask(null, null, null).Element.Condition = expectedConditionValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Condition", expectedConditionValue));
            }
            [TestMethod]
            public void ShouldWriteTheAssemblyNameAttribute()
            {
                StringWriter stringWriter;
                string expectedAssemblyNameValue;
                //Arrange
                expectedAssemblyNameValue = "TestAssemblyNameValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.AssemblyName = expectedAssemblyNameValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("AssemblyName", expectedAssemblyNameValue));
            }
            [TestMethod]
            public void ShouldWriteTheAssemblyFileAttribute()
            {
                StringWriter stringWriter;
                string expectedAssemblyFileValue;
                //Arrange
                expectedAssemblyFileValue = "TestAssemblyFileValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.AssemblyFile = expectedAssemblyFileValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("AssemblyFile", expectedAssemblyFileValue));
            }
            [TestMethod]
            public void ShouldWriteTheTaskFactoryAttribute()
            {
                StringWriter stringWriter;
                string expectedTaskFactoryValue;
                //Arrange
                expectedTaskFactoryValue = "TestTaskFactoryValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.TaskFactory = expectedTaskFactoryValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("TaskFactory", expectedTaskFactoryValue));
            }
            [TestMethod]
            public void ShouldWriteTheTaskNameAttribute()
            {
                StringWriter stringWriter;
                string expectedTaskNameValue;
                //Arrange
                expectedTaskNameValue = "TestTaskNameValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.TaskName = expectedTaskNameValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("TaskName", expectedTaskNameValue));
            }

            string XmlAttribute(string name, string value)
            {
                return string.Format("{0}=\"{1}\"", name, value);
            }
        }
    }
}
