using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class TargetActionsTest
    {
        TargetActions unit;
        Target target;

        [TestInitialize]
        public void TestInitialize()
        {
            //Some properties are only applicable when the Item is used within a target
            Root root = Root.Create();
            target = root.CreateTargetElement("InitialTestNameValue");
            unit = new TargetActions(target);
        }

        [TestClass]
        public class TheWithAfterTargetsMethod : TargetActionsTest
        {
            [TestMethod]
            public void ShouldSetTheAfterTargetsPropertyOfTheItem()
            {
                string actualAfterTargets;
                //Arrange
                actualAfterTargets = "TestAfterTargetsValue";
                //Act
                unit.WithAfterTargets(actualAfterTargets);
                //Assert
                target.AfterTargets.Should().Be(actualAfterTargets);
            }
        }
        [TestClass]
        public class TheWithBeforeTargetsMethod : TargetActionsTest
        {
            [TestMethod]
            public void ShouldSetTheBeforeTargetsPropertyOfTheItem()
            {
                string actualBeforeTargets;
                //Arrange
                actualBeforeTargets = "TestBeforeTargetsValue";
                //Act
                unit.WithBeforeTargets(actualBeforeTargets);
                //Assert
                target.BeforeTargets.Should().Be(actualBeforeTargets);
            }
        }
        [TestClass]
        public class TheWithDependsOnTargetsMethod : TargetActionsTest
        {
            [TestMethod]
            public void ShouldSetTheDependsOnTargetsPropertyOfTheItem()
            {
                string actualDependsOnTargets;
                //Arrange
                actualDependsOnTargets = "TestDependsOnTargetsValue";
                //Act
                unit.WithDependsOnTargets(actualDependsOnTargets);
                //Assert
                target.DependsOnTargets.Should().Be(actualDependsOnTargets);
            }
        }
        [TestClass]
        public class TheWithInputsMethod : TargetActionsTest
        {
            [TestMethod]
            public void ShouldSetTheInputsPropertyOfTheItem()
            {
                string actualInputs;
                //Arrange
                actualInputs = "TestInputsValue";
                //Act
                unit.WithInputs(actualInputs);
                //Assert
                target.Inputs.Should().Be(actualInputs);
            }
        }
        [TestClass]
        public class TheWithKeepDuplicateOutputsMethod : TargetActionsTest
        {
            [TestMethod]
            public void ShouldSetTheKeepDuplicateOutputsPropertyOfTheItem()
            {
                string actualKeepDuplicateOutputs;
                //Arrange
                actualKeepDuplicateOutputs = "TestKeepDuplicateOutputsValue";
                //Act
                unit.WithKeepDuplicateOutputs(actualKeepDuplicateOutputs);
                //Assert
                target.KeepDuplicateOutputs.Should().Be(actualKeepDuplicateOutputs);
            }
        }
        [TestClass]
        public class TheWithOutputsMethod : TargetActionsTest
        {
            [TestMethod]
            public void ShouldSetTheOutputsPropertyOfTheItem()
            {
                string actualOutputs;
                //Arrange
                actualOutputs = "TestOutputsValue";
                //Act
                unit.WithOutputs(actualOutputs);
                //Assert
                target.Outputs.Should().Be(actualOutputs);
            }
        }
        [TestClass]
        public class TheWithReturnsMethod : TargetActionsTest
        {
            [TestMethod]
            public void ShouldSetTheReturnsPropertyOfTheItem()
            {
                string actualReturns;
                //Arrange
                actualReturns = "TestReturnsValue";
                //Act
                unit.WithReturns(actualReturns);
                //Assert
                target.Returns.Should().Be(actualReturns);
            }
        }
    }
}
