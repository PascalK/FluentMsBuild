using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class UsingTaskActionsTest
    {
        UsingTaskActions unit;
        UsingTask usingTask;

        [TestInitialize]
        public void TestInitialize()
        {
            //Some properties are only applicable when the Item is used within a target
            Root root = Root.Create();
            usingTask = root.CreateUsingTaskElement("InitialTestNameValue", "InitialTestAssemblyFileValue", "InitialTestAssemblyNameValuemk");
            unit = new UsingTaskActions(usingTask);
        }
        [TestClass]
        public class TheWithTaskFactoryMethod : UsingTaskActionsTest
        {
            [TestMethod]
            public void ShouldSetTheTaskFactoryPropertyOfTheItem()
            {
                string actualTaskFactory;
                //Arrange
                actualTaskFactory = "TestTaskFactoryValue";
                //Act
                unit.WithTaskFactory(actualTaskFactory);
                //Assert
                usingTask.TaskFactory.Should().Be(actualTaskFactory);
            }
        }
    }
}
