using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    [TestClass]
    public class ItemDefinitionActionsTest
    {
        ItemDefinitionActions unit;
        ItemDefinition itemDefinition;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            itemDefinition = root.CreateItemDefinitionElement(string.Empty);
            unit = new ItemDefinitionActions(itemDefinition);
        }

        [TestClass]
        public class TheWithMetadataMethod : ItemDefinitionActionsTest
        {
            [TestMethod]
            public void ShouldSetTheMetadataPropertyOfTheItemDefinition()
            {
                string actualName;
                string actualValue;
                //Arrange
                actualName = "TestNameValue";
                actualValue = "TestValueValue";
                //Act
                unit.WithMetadata(actualName, actualValue);
                //Assert
                var actualMetadataResult = itemDefinition.Metadata.Single();
                actualMetadataResult.Name.Should().Be(actualName);
                actualMetadataResult.Value.Should().Be(actualValue);
            }
            [TestMethod]
            public void ShouldSetTheMetadataPropertyOfTheItemDefinitionWhenCallenWithMultipleItemDefinitions()
            {
                string actualName1;
                string actualValue1;
                string actualName2;
                string actualValue2;
                IEnumerable<KeyValuePair<string, string>> actualMetadata;
                //Arrange
                actualName1 = "TestNameValue1";
                actualValue1 = "TestValueValue1";
                actualName2 = "TestNameValue2";
                actualValue2 = "TestValueValue2";
                actualMetadata = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>(actualName1, actualValue1),
                    new KeyValuePair<string, string>(actualName2, actualValue2),
                };
                //Act
                unit.WithMetadata(actualMetadata);
                //Assert
                itemDefinition.Metadata.Should().HaveCount(2);
                var actualFirstMetadataResult = itemDefinition.Metadata.First();
                var actualLastMetadataResult = itemDefinition.Metadata.Last();
                actualFirstMetadataResult.Name.Should().Be(actualName1);
                actualFirstMetadataResult.Value.Should().Be(actualValue1);
                actualLastMetadataResult.Name.Should().Be(actualName2);
                actualLastMetadataResult.Value.Should().Be(actualValue2);
            }
        }
        [TestClass]
        public class TheAddMetadataMethod : ItemDefinitionActionsTest
        {
            [TestMethod]
            public void ShouldAddAMetadataItemDefinition()
            {
                string actualName;
                string actualValue;
                //Arrange
                actualName = "TestNameValue";
                actualValue = "TestValueValue";
                //Act
                var actualResult = unit.AddMetadata(actualName, actualValue).Element;
                //Assert
                var actualMetadataResult = itemDefinition.Metadata.Single();
                actualMetadataResult.Should().Be(actualResult);
                actualMetadataResult.Name.Should().Be(actualName);
                actualMetadataResult.Value.Should().Be(actualValue);
            }
        }
    }
}