using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class PropertyActionsTest
    {
        PropertyActions<string> unit;
        Property property;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            property = root.AddProperty<string>("InitialTestNameValue", "InitialTestValueValue").Element;
            unit = new PropertyActions<string>(property);
        }
        [TestClass]
        public class TheWithValueMethod : PropertyActionsTest
        {
            [TestMethod]
            public void ShouldSetTheValueOfTheProperty()
            {
                string actualValue;
                //Arrange
                actualValue = "TestValueValue";
                //Act
                unit.WithValue(actualValue);
                //Assert
                property.Value.Should().Be(actualValue);
            }
        }
        [TestClass]
        public class TheWithNameMethod : PropertyActionsTest
        {
            [TestMethod]
            public void ShouldSetTheValueOfTheProperty()
            {
                string actualName;
                //Arrange
                actualName = "TestNameValue";
                //Act
                unit.WithName(actualName);
                //Assert
                property.Name.Should().Be(actualName);
            }
        }

    }
}