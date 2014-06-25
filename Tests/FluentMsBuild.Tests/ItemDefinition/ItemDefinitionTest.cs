using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FluentMsBuild
{
    [TestClass]
    public class ItemDefinitionTest
    {
        ItemDefinition unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateItemDefinitionElement(string.Empty);
        }

        [TestClass]
        public class TheAddMetadataMethod : ItemDefinitionTest
        {
            [TestMethod]
            public void ShouldAddMetadataElementToProperty()
            {
                string actualName;
                string actualValue;
                //Arrange
                actualName = "TestNameValue";
                actualValue = "TestValueValue";
                //Act
                unit.AddMetadata(actualName, actualValue);
                //Assert
                var actualMetadata = unit.Metadata.Single();
                actualMetadata.Name.Should().Be(actualName);
                actualMetadata.Value.Should().Be(actualValue);
                actualMetadata.Label.Should().BeEmpty();
                actualMetadata.Condition.Should().BeEmpty();
            }
        }
    }
}
