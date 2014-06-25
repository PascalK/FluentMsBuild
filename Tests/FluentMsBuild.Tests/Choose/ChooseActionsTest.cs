using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FluentMsBuild
{
    [TestClass]
    public class ChooseActionsTest
    {
        ChooseActions unit;
        Choose choose;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            choose = root.CreateChooseElement();
            unit = new ChooseActions(choose);
        }
        [TestClass]
        public class TheAddOtherwiseMethod : ChooseActionsTest
        {
            [TestMethod]
            public void ShouldAddANewOtherwiseElement()
            {
                //Arrange
                //Act
                var actualOtherwise = unit.AddOtherwise();
                //Assert
                choose.OtherwiseElement.Should().NotBeNull();
                choose.OtherwiseElement.Should().Be(actualOtherwise.Element);
            }
        }
        [TestClass]
        public class TheWithOtherwiseMethod : ChooseActionsTest
        {
            [TestMethod]
            public void ShouldSetANewOtherwiseElement()
            {
                //Arrange
                //Act
                unit.WithOtherwise();
                //Assert
                choose.OtherwiseElement.Should().NotBeNull();
            }
        }
        [TestClass]
        public class TheAddWhenMethod : ChooseActionsTest
        {
            [TestMethod]
            public void ShouldAddANewWhenElement()
            {
                string expectedCondition;
                //Arrange
                expectedCondition = "TestConditionValue";
                //Act
                unit.AddWhen(expectedCondition);
                //Assert
                choose.WhenElements.Single().Condition.Should().Be(expectedCondition);
            }
        }
        [TestClass]
        public class TheWithWhenMethod : ChooseActionsTest
        {
            [TestMethod]
            public void ShouldSetANewWhenElement()
            {
                string expectedCondition;
                //Arrange
                expectedCondition = "TestConditionValue";
                //Act
                var actualChoose = unit.WithWhen(expectedCondition);
                //Assert
                choose.WhenElements.Single().Condition.Should().Be(expectedCondition);
                choose.Should().Be(actualChoose.Element);
            }
        }
    }
}