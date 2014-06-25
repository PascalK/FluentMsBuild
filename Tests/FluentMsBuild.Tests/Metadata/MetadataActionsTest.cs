using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace FluentMsBuild
{
    [TestClass]
    public class MetadataActionsTest
    {
        MetadataActions unit;
        Metadata metadata;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            metadata = root.CreateMetadataElement( "InitialMetadataName");
            unit = new MetadataActions(metadata);
        }

        [TestClass]
        public class TheWithNameMethod : MetadataActionsTest
        {
            [TestMethod]
            public void ShouldSetTheNamePropertyOfTheItem()
            {
                string actualName;
                //Arrange
                actualName = "TestNameValue";
                //Act
                unit.WithName(actualName);
                //Assert
                metadata.Name.Should().Be(actualName);
            }
        }
        [TestClass]
        public class TheWithValueMethod : MetadataActionsTest
        {
            [TestMethod]
            public void ShouldSetTheValuePropertyOfTheItem()
            {
                string actualValue;
                //Arrange
                actualValue = "TestValueValue";
                //Act
                unit.WithValue(actualValue);
                //Assert
                metadata.Value.Should().Be(actualValue);
            }
        }
    }
}