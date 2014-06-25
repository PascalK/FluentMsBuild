using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class OutputActionsTest
    {
        OutputActions unit;
        Output output;

        [TestInitialize]
        public void TestInitialize()
        {
            //Some properties are only applicable when the Item is used within a target
            Root root = Root.Create();
            output = root.CreateOutputElement(string.Empty, string.Empty, string.Empty);
            unit = new OutputActions(output);
        }

        [TestClass]
        public class TheWithItemNameMethod : OutputActionsTest
        {
            [TestMethod]
            public void ShouldSetTheItemNamePropertyOfTheItem()
            {
                string actualItemName;
                //Arrange
                actualItemName = "TestItemNameValue";
                //Act
                unit.WithItemName(actualItemName);
                //Assert
                output.ItemName.Should().Be(actualItemName);
            }
        }
        [TestClass]
        public class TheWithPropertyNameMethod : OutputActionsTest
        {
            [TestMethod]
            public void ShouldSetThePropertyNamePropertyOfTheItem()
            {
                string actualPropertyName;
                //Arrange
                actualPropertyName = "TestPropertyNameValue";
                //Act
                unit.WithPropertyName(actualPropertyName);
                //Assert
                output.PropertyName.Should().Be(actualPropertyName);
            }
        }
        [TestClass]
        public class TheWithTaskParameterMethod : OutputActionsTest
        {
            [TestMethod]
            public void ShouldSetTheTaskParameterPropertyOfTheItem()
            {
                string actualTaskParameter;
                //Arrange
                actualTaskParameter = "TestTaskParameterValue";
                //Act
                unit.WithTaskParameter(actualTaskParameter);
                //Assert
                output.TaskParameter.Should().Be(actualTaskParameter);
            }
        }
    }
}