using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class UsingTaskParameterActionsTest
    {
        UsingTaskParameterActions unit;
        UsingTaskParameter usingTaskParameter;

        [TestInitialize]
        public void TestInitialize()
        {
            //Some properties are only applicable when the Item is used within a target
            Root root = Root.Create();
            usingTaskParameter = root.CreateUsingTaskParameterElement("InitialTestNameValue", "InitialTestOutputValue", "InitialTestRequiredValue", "InitialTestParameterTypeValue");
            unit = new UsingTaskParameterActions(usingTaskParameter);
        }
        [TestClass]
        public class TheWithOutputMethod : UsingTaskParameterActionsTest
        {
            [TestMethod]
            public void ShouldSetTheOutputPropertyOfTheItem()
            {
                string actualOutput;
                //Arrange
                actualOutput = "TestOutputValue";
                //Act
                unit.WithOutput(actualOutput);
                //Assert
                usingTaskParameter.Output.Should().Be(actualOutput);
            }
        }
        [TestClass]
        public class TheWithRequiredMethod : UsingTaskParameterActionsTest
        {
            [TestMethod]
            public void ShouldSetTheRequiredPropertyOfTheItem()
            {
                string actualRequired;
                //Arrange
                actualRequired = "TestRequiredValue";
                //Act
                unit.WithRequired(actualRequired);
                //Assert
                usingTaskParameter.Required.Should().Be(actualRequired);
            }
        }
        [TestClass]
        public class TheWithParameterTypeMethod : UsingTaskParameterActionsTest
        {
            [TestMethod]
            public void ShouldSetTheParameterTypePropertyOfTheItem()
            {
                string actualParameterType;
                //Arrange
                actualParameterType = "TestParameterTypeValue";
                //Act
                unit.WithParameterType(actualParameterType);
                //Assert
                usingTaskParameter.ParameterType.Should().Be(actualParameterType);
            }
        }
    }
}
