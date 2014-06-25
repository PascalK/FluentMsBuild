using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class TaskActionsTest
    {
        TaskActions unit;
        Task task;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            task = root.CreateTaskElement("InitialTestNameValue");
            unit = new TaskActions(task);
        }
        [TestClass]
        public class TheWithContinueOnErrorMethod : TaskActionsTest
        {
            [TestMethod]
            public void ShouldSetTheContinueOnErrorPropertyOfTheItem()
            {
                string actualContinueOnError;
                //Arrange
                actualContinueOnError = "TestContinueOnErrorValue";
                //Act
                unit.WithContinueOnError(actualContinueOnError);
                //Assert
                task.ContinueOnError.Should().Be(actualContinueOnError);
            }
        }
        [TestClass]
        public class TheWithMSBuildArchitectureMethod : TaskActionsTest
        {
            [TestMethod]
            public void ShouldSetTheMSBuildArchitecturePropertyOfTheItem()
            {
                string actualMSBuildArchitecture;
                //Arrange
                actualMSBuildArchitecture = "TestMSBuildArchitectureValue";
                //Act
                unit.WithMSBuildArchitecture(actualMSBuildArchitecture);
                //Assert
                task.MSBuildArchitecture.Should().Be(actualMSBuildArchitecture);
            }
        }
        [TestClass]
        public class TheWithMSBuildRuntimeMethod : TaskActionsTest
        {
            [TestMethod]
            public void ShouldSetTheMSBuildRuntimePropertyOfTheItem()
            {
                string actualMSBuildRuntime;
                //Arrange
                actualMSBuildRuntime = "TestMSBuildRuntimeValue";
                //Act
                unit.WithMSBuildRuntime(actualMSBuildRuntime);
                //Assert
                task.MSBuildRuntime.Should().Be(actualMSBuildRuntime);
            }
        }
    }
}
