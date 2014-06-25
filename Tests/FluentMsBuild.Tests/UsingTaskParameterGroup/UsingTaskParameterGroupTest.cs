using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class UsingTaskParameterGroupTest
    {
        UsingTaskParameterGroup unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateUsingTaskParameterGroupElement();
        }

        [TestClass]
        public class TheConditionProperty : UsingTaskParameterGroupTest
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
        public class TheAddParameterMethod : UsingTaskParameterGroupTest
        {
            [TestMethod]
            public void ShouldAddTheReturnParameterToTheParametersPropertyWithCorrespondingName()
            {
                string expectedParameterName;
                //Arrange
                expectedParameterName = "TestParameterNameValue";
                //Act
                var actualAddedParameter = unit.AddParameter(expectedParameterName).Element;
                //Assert
                var actualParameter = unit.Parameters.Single();
                actualParameter.Should().Be(actualParameter);
                actualParameter.Name.Should().Be(expectedParameterName);
            }
            [TestMethod]
            public void ShouldAddTheReturnParameterToTheParametersPropertyWithCorrespondingValues()
            {
                string expectedParameterName;
                string expectedOutput;
                string expectedRequired;
                string expectedParameterType;
                //Arrange
                expectedParameterName = "TestParameterNameValue";
                expectedOutput = "TestOutputValue";
                expectedRequired = "TestRequiredValue";
                expectedParameterType = "TestParameterTypeValue";
                //Act
                var actualAddedParameter = unit.AddParameter(expectedParameterName, expectedOutput, expectedRequired, expectedParameterType).Element;
                //Assert
                var actualParameter = unit.Parameters.Single();
                actualParameter.Should().Be(actualParameter);
                actualParameter.Name.Should().Be(expectedParameterName);
                actualParameter.Output.Should().Be(expectedOutput);
                actualParameter.Required.Should().Be(expectedRequired);
                actualParameter.ParameterType.Should().Be(expectedParameterType);
            }
        }
        [TestClass]
        public class TheLoadMethod : UsingTaskParameterGroupTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadLabelAttribute()
            {
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask>" +
                                    "<ParameterGroup Label=\"" + expectedLabelValue + "\" />" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualParameterGroup = root.UsingTasks.Single().ParameterGroup;
                actualParameterGroup.Label.Should().Be(expectedLabelValue);
            }
            [TestMethod]
            public void ShouldLoadInnerElementAsParameter()
            {
                string expectedParameterName;
                //Arrange
                expectedParameterName = "TestParameterNameValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<UsingTask>" +
                                    "<ParameterGroup>" +
                                        "<" + expectedParameterName + " />" +
                                    "</ParameterGroup>" +
                                "</UsingTask>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualParameter = root.UsingTasks.Single().ParameterGroup.Parameters.Single();
                actualParameter.Name.Should().Be(expectedParameterName);
            }
        }
    }
}