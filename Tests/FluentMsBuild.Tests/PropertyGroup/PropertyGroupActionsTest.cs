using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FluentMsBuild
{
    [TestClass]
    public class PropertyGroupActionsTest
    {
        PropertyGroupActions unit;
        PropertyGroup propertyGroup;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            propertyGroup = root.AddPropertyGroup().Element;
            unit = new PropertyGroupActions(propertyGroup);
        }

        [TestClass]
        public class TheAddPropertyMethod : PropertyGroupActionsTest
        {
            [TestMethod]
            public void ShouldAddANewPropertyWithTheCorrespondingPropertiesToThePropertyGroup()
            {
                string actualName;
                string actualValue;
                //Arrange
                actualName = "TestNameValue";
                actualValue = "TestValueValue";
                //Act
                unit.AddProperty(actualName, actualValue);
                //Assert
                var actualProperty = propertyGroup.Properties.Single();
                actualProperty.Name.Should().Be(actualName);
                actualProperty.Value.Should().Be(actualValue);
            }
        }
        [TestClass]
        public class TheSetPropertyMethod : PropertyGroupActionsTest
        {
            [TestMethod]
            public void ShouldAddANewPropertyWithTheCorrespondingProperties()
            {
                string actualName;
                string actualValue;
                //Arrange
                actualName = "TestNameValue";
                actualValue = "TestValueValue";
                //Act
                unit.SetProperty(actualName, actualValue);
                //Assert
                var actualProperty = propertyGroup.Properties.Single();
                actualProperty.Name.Should().Be(actualName);
                actualProperty.Value.Should().Be(actualValue);
            }
            [TestMethod]
            public void ShouldUpdateTheValueOfAnExistingPropertyWithTheSameName()
            {
                string actualName;
                string actualValue1;
                string actualValue2;
                //Arrange
                actualName = "TestNameValue";
                actualValue1 = "TestValueValue1";
                actualValue2 = "TestValueValue2";
                unit.AddProperty(actualName, actualValue1);
                //Act
                unit.SetProperty(actualName, actualValue2);
                //Assert
                var actualProperty = propertyGroup.Properties.Single();
                actualProperty.Name.Should().Be(actualName);
                actualProperty.Value.Should().Be(actualValue2);
            }
        }
    }
}
