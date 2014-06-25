using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    public partial class RootTest
    {
        [TestClass]
        public class TheSetPropertyMethod : RootTest
        {
            //[TestMethod]
            //public void ShouldAddNewPropertyWhenNoneWithCorrespondingNameExists()
            //{
            //    string actualName;
            //    string actualValue;
            //    //Arrange
            //    actualName = "TestNameValue";
            //    actualValue = "TestValueValue";
            //    //Act
            //    var actualResult = unit.SetProperty(actualName, actualValue);
            //    //Assert
            //    var actualProperty = unit.Properties.Single();
            //    actualProperty.Name.Should().Be(actualName);
            //    actualProperty.Value.Should().Be(actualValue);
            //    actualProperty.Condition.Should().BeEmpty();
            //    actualProperty.Label.Should().BeEmpty();
            //}
            //[TestMethod]
            //public void ShouldUpdateExistingPropertyWhenOneWithCorrespondingNameExists()
            //{
            //    string actualName;
            //    string actualValue1;
            //    string actualValue2;
            //    //Arrange
            //    actualName = "TestNameValue";
            //    actualValue1 = "TestValueValue";
            //    actualValue2 = "TestValueValue";
            //    unit.AddProperty(actualName, actualValue1);
            //    //Arrange - Check
            //    var expectedProperty = unit.Properties.Single();
            //    expectedProperty.Name.Should().Be(actualName);
            //    expectedProperty.Value.Should().Be(actualValue1);
            //    expectedProperty.Condition.Should().BeEmpty();
            //    expectedProperty.Label.Should().BeEmpty();
            //    //Act
            //    var actualResult = unit.SetProperty(actualName, actualValue2);
            //    //Assert
            //    var actualProperty = unit.Properties.Single();
            //    actualProperty.Name.Should().Be(actualName);
            //    actualProperty.Value.Should().Be(actualValue2);
            //    actualProperty.Condition.Should().BeEmpty();
            //    actualProperty.Label.Should().BeEmpty();
            //    actualProperty.Should().BeSameAs(expectedProperty);
            //}
        }
    }
}
