using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class OnErrorActionsTest
    {
        OnErrorActions unit;
        OnError onError;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            onError = root.CreateOnErrorElement(string.Empty);
            unit = new OnErrorActions(onError);
        }

        [TestClass]
        public class TheWithExecuteTargetsAttributeMethod : OnErrorActionsTest
        {
            [TestMethod]
            public void ShouldSetTheExecuteTargetsAttributePropertyOfTheItem()
            {
                string actualExecuteTargetsAttribute;
                //Arrange
                actualExecuteTargetsAttribute = "TestExecuteTargetsAttributeValue";
                //Act
                unit.WithExecuteTargetsAttribute(actualExecuteTargetsAttribute);
                //Assert
                onError.ExecuteTargetsAttribute.Should().Be(actualExecuteTargetsAttribute);
            }
        }
    }
}