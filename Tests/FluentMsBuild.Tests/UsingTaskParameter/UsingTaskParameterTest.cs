using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class UsingTaskParameterTest
    {
        UsingTaskParameter unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateUsingTaskParameterElement(null, null, null, null);
        }

        [TestClass]
        public class TheConditionProperty : UsingTaskParameterTest
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
        public class TheXmlNameProperty : UsingTaskParameterTest
        {
            [TestMethod]
            public void ShouldReturnTheNameProperty()
            {
                string expectedName;
                //Arrange
                expectedName = "TestNameValue";
                unit.Name = expectedName;
                //Act
                var actualName = typeof(UsingTaskParameter).GetProperty("XmlName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty).GetValue(unit).ToString();
                //Assert
                actualName.Should().Be(expectedName);
            }
        }
        [TestClass]
        public class TheNameProperty : UsingTaskParameterTest
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
        public class TheOutputProperty : UsingTaskParameterTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Output = null;
                //Act
                var actualOutput = unit.Output;
                //Assert
                actualOutput.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheParameterTypeProperty : UsingTaskParameterTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.ParameterType = null;
                //Act
                var actualParameterType = unit.ParameterType;
                //Assert
                actualParameterType.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheRequiredProperty : UsingTaskParameterTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Required = null;
                //Act
                var actualRequired = unit.Required;
                //Assert
                actualRequired.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheLoadMethod : UsingTaskParameterTest
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
                                    "<ParameterGroup>" +
                                        "<TestUsingTaskParameterName Label=\"" + expectedLabelValue + "\" />" +
                                    "</ParameterGroup>" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTaskParameter = root.UsingTasks.Single().ParameterGroup.Parameters.Single();
                actualUsingTaskParameter.Label.Should().Be(expectedLabelValue);
            }
            [TestMethod]
            public void ShouldLoadParameterTypeAttribute()
            {
                string expectedParameterTypeValue = "TestParameterTypeValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask>" +
                                    "<ParameterGroup>" +
                                        "<TestUsingTaskParameterName ParameterType=\"" + expectedParameterTypeValue + "\" />" +
                                    "</ParameterGroup>" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTaskParameter = root.UsingTasks.Single().ParameterGroup.Parameters.Single();
                actualUsingTaskParameter.ParameterType.Should().Be(expectedParameterTypeValue);
            }
            [TestMethod]
            public void ShouldLoadOutputAttribute()
            {
                string expectedOutputValue = "TestOutputValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask>" +
                                    "<ParameterGroup>" +
                                        "<TestUsingTaskParameterName Output=\"" + expectedOutputValue + "\" />" +
                                    "</ParameterGroup>" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTaskParameter = root.UsingTasks.Single().ParameterGroup.Parameters.Single();
                actualUsingTaskParameter.Output.Should().Be(expectedOutputValue);
            }
            [TestMethod]
            public void ShouldLoadRequiredAttribute()
            {
                string expectedRequiredValue = "TestRequiredValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask>" +
                                    "<ParameterGroup>" +
                                        "<TestUsingTaskParameterName Required=\"" + expectedRequiredValue + "\" />" +
                                    "</ParameterGroup>" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualUsingTaskParameter = root.UsingTasks.Single().ParameterGroup.Parameters.Single();
                actualUsingTaskParameter.Required.Should().Be(expectedRequiredValue);
            }
        }
        [TestClass]
        public class TheSaveMethod : UsingTaskParameterTest
        {
            [TestMethod]
            public void ShouldWriteTheLabelAttribute()
            {
                StringWriter stringWriter;
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.AddParameterGroup().Element.AddParameter("Name").Element.Label = expectedLabelValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Label", expectedLabelValue));
            }
            [TestMethod]
            public void ShouldWriteTheParameterTypeAttribute()
            {
                StringWriter stringWriter;
                string expectedParameterTypeValue;
                //Arrange
                expectedParameterTypeValue = "TestParameterTypeValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.AddParameterGroup().Element.AddParameter("Name").Element.ParameterType = expectedParameterTypeValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("ParameterType", expectedParameterTypeValue));
            }
            [TestMethod]
            public void ShouldWriteTheRequiredAttribute()
            {
                StringWriter stringWriter;
                string expectedRequiredValue;
                //Arrange
                expectedRequiredValue = "TestRequiredValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.AddParameterGroup().Element.AddParameter("Name").Element.Required = expectedRequiredValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Required", expectedRequiredValue));
            }
            [TestMethod]
            public void ShouldWriteTheOutputAttribute()
            {
                StringWriter stringWriter;
                string expectedOutputValue;
                //Arrange
                expectedOutputValue = "TestOutputValue";
                root = Root.Create();
                root.AddUsingTask(null, null, null).Element.AddParameterGroup().Element.AddParameter("Name").Element.Output = expectedOutputValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Output", expectedOutputValue));
            }
            string XmlAttribute(string name, string value)
            {
                return string.Format("{0}=\"{1}\"", name, value);
            }
        }
    }
}
