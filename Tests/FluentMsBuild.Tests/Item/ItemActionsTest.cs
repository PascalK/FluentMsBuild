using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    [TestClass]
    public class ItemActionsTest
    {
        ItemActions unit;
        Item item;

        [TestInitialize]
        public void TestInitialize()
        {
            //Some properties are only applicable when the Item is used within a target
            Root root = Root.Create();
            item = root.CreateItemElement(string.Empty);
            unit = new ItemActions(item);
        }

        [TestClass]
        public class TheWithIncludeMethod : ItemActionsTest
        {
            [TestMethod]
            public void ShouldSetTheIncludePropertyOfTheItem()
            {
                string actualInclude;
                //Arrange
                actualInclude = "TestIncludeValue";
                //Act
                unit.WithInclude(actualInclude);
                //Assert
                item.Include.Should().Be(actualInclude);
            }
        }
        [TestClass]
        public class TheWithRemoveMethod : ItemActionsTest
        {
            [TestMethod]
            public void ShouldSetTheRemovePropertyOfTheItem()
            {
                string actualRemove;
                //Arrange
                actualRemove = "TestRemoveValue";
                unit.WithInclude(null); //Item can not have both include and Remove
                //Act
                unit.WithRemove(actualRemove);
                //Assert
                item.Remove.Should().Be(actualRemove);
            }
        }
        [TestClass]
        public class TheWithKeepDuplicatesMethod : ItemActionsTest
        {
            [TestMethod]
            public void ShouldSetTheKeepDuplicatesPropertyOfTheItem()
            {
                string actualKeepDuplicates;
                //Arrange
                actualKeepDuplicates = "TestKeepDuplicatesValue";
                //Act
                unit.WithKeepDuplicates(actualKeepDuplicates);
                //Assert
                item.KeepDuplicates.Should().Be(actualKeepDuplicates);
            }
        }
        [TestClass]
        public class TheWithKeepMetadataMethod : ItemActionsTest
        {
            [TestMethod]
            public void ShouldSetTheKeepMetadataPropertyOfTheItem()
            {
                string actualKeepMetadata;
                //Arrange
                actualKeepMetadata = "TestKeepMetadataValue";
                //Act
                unit.WithKeepMetadata(actualKeepMetadata);
                //Assert
                item.KeepMetadata.Should().Be(actualKeepMetadata);
            }
        }
        [TestClass]
        public class TheWithRemoveMetadataMethod : ItemActionsTest
        {
            [TestMethod]
            public void ShouldSetTheRemoveMetadataPropertyOfTheItem()
            {
                string actualRemoveMetadata;
                //Arrange
                actualRemoveMetadata = "TestRemoveMetadataValue";
                //Act
                unit.WithRemoveMetadata(actualRemoveMetadata);
                //Assert
                item.RemoveMetadata.Should().Be(actualRemoveMetadata);
            }
        }
        [TestClass]
        public class TheWithMetadataMethod : ItemActionsTest
        {
            [TestMethod]
            public void ShouldSetTheMetadataPropertyOfTheItem()
            {
                string actualName;
                string actualValue;
                //Arrange
                actualName = "TestNameValue";
                actualValue = "TestValueValue";
                //Act
                unit.WithMetadata(actualName, actualValue);
                //Assert
                var actualMetadataResult = item.Metadata.Single();
                actualMetadataResult.Name.Should().Be(actualName);
                actualMetadataResult.Value.Should().Be(actualValue);
            }
            [TestMethod]
            public void ShouldSetTheMetadataPropertyOfTheItemWhenCallenWithMultipleItems()
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
                item.Metadata.Should().HaveCount(2);
                var actualFirstMetadataResult = item.Metadata.First();
                var actualLastMetadataResult = item.Metadata.Last();
                actualFirstMetadataResult.Name.Should().Be(actualName1);
                actualFirstMetadataResult.Value.Should().Be(actualValue1);
                actualLastMetadataResult.Name.Should().Be(actualName2);
                actualLastMetadataResult.Value.Should().Be(actualValue2);
            }
        }
        [TestClass]
        public class TheAddMetadataMethod : ItemActionsTest
        {
            [TestMethod]
            public void ShouldAddAMetadataItem()
            {
                string actualName;
                string actualValue;
                //Arrange
                actualName = "TestNameValue";
                actualValue = "TestValueValue";
                //Act
                var actualResult = unit.AddMetadata(actualName, actualValue).Element;
                //Assert
                var actualMetadataResult = item.Metadata.Single();
                actualMetadataResult.Should().Be(actualResult);
                actualMetadataResult.Name.Should().Be(actualName);
                actualMetadataResult.Value.Should().Be(actualValue);
            }
        }
    }
}
