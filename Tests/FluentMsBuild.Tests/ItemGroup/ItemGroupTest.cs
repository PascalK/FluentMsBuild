using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class ItemGroupTest
    {
        ItemGroup unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateItemGroupElement();
        }
        [TestClass]
        public class TheAddItemMethod : ItemGroupTest
        {
            [TestMethod]
            public void ShouldAddACorrespondingItemWhenCalledWithANameAndValue()
            {
                string actualItemType;
                string actualInclude;
                //Arrange
                actualItemType = "TestItemTypeValue";
                actualInclude = "TestIncludeValue";
                //Act
                unit.AddItem(actualItemType, actualInclude);
                //Assert
                var actualItem = unit.Items.Single();
                actualItem.ItemType.Should().Be(actualItemType);
                actualItem.Include.Should().Be(actualInclude);
                actualItem.Exclude.Should().BeNullOrEmpty();
                actualItem.KeepDuplicates.Should().BeNullOrEmpty();
                actualItem.KeepMetadata.Should().BeNullOrEmpty();
                actualItem.Metadata.Should().BeEmpty();
                actualItem.Remove.Should().BeNullOrEmpty();
                actualItem.RemoveMetadata.Should().BeNullOrEmpty(); ;
            }
            [TestMethod]
            public void ShouldAddACorrespondingItemWhenCalledWithANameAndValueAnd1Metadata()
            {
                string actualItemType;
                string actualInclude;
                string actualMetadataName;
                string actualMetadataValue;
                IEnumerable<KeyValuePair<string, string>> actualMetadata;
                //Arrange
                actualItemType = "TestItemTypeValue";
                actualInclude = "TestIncludeValue";
                actualMetadataName = "TestMetadataNameValue";
                actualMetadataValue = "TestMetadataValueValue";
                actualMetadata = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>(actualMetadataName, actualMetadataValue),
                };
                //Act
                unit.AddItem(actualItemType, actualInclude, actualMetadata);
                //Assert
                var actualItem = unit.Items.Single();
                actualItem.ItemType.Should().Be(actualItemType);
                actualItem.Include.Should().Be(actualInclude);
                actualItem.Exclude.Should().BeNullOrEmpty();
                actualItem.KeepDuplicates.Should().BeNullOrEmpty();
                actualItem.KeepMetadata.Should().BeNullOrEmpty();
                actualItem.Remove.Should().BeNullOrEmpty();
                actualItem.RemoveMetadata.Should().BeNullOrEmpty(); ;
                var actualMetadataResult = actualItem.Metadata.Single();
                actualMetadataResult.Name.Should().Be(actualMetadataName);
                actualMetadataResult.Value.Should().Be(actualMetadataValue);
            }
            [TestMethod]
            public void ShouldAddACorrespondingItemWhenSortedByItemTypeAndThenByIncludeAttribute()
            {
                string actualItemType0;
                string actualItemType1;
                string actualItemType2;
                string actualItemType3;
                string actualInclude0;
                string actualInclude1;
                string actualInclude2;
                string actualInclude3;
                //Arrange
                actualItemType0 = "TestItemTypeValue1";
                actualItemType1 = "TestItemTypeValue1";
                actualItemType2 = "TestItemTypeValue2";
                actualItemType3 = "TestItemTypeValue2";
                actualInclude0 = "TestIncludeValue0";
                actualInclude1 = "TestIncludeValue1";
                actualInclude2 = "TestIncludeValue2";
                actualInclude3 = "TestIncludeValue1";
                //Act
                unit.AddItem(actualItemType0, actualInclude0);
                unit.AddItem(actualItemType2, actualInclude2);
                unit.AddItem(actualItemType1, actualInclude1);
                unit.AddItem(actualItemType3, actualInclude3);
                //Assert
                var actualFirstItem = unit.Items.ElementAt(0);
                var actualSecondItem = unit.Items.ElementAt(1);
                var actualThirdItem = unit.Items.ElementAt(2);
                var actualFourthItem = unit.Items.ElementAt(3);
                actualFirstItem.ItemType.Should().Be(actualItemType0);
                actualFirstItem.Include.Should().Be(actualInclude0);
                actualSecondItem.ItemType.Should().Be(actualItemType1);
                actualSecondItem.Include.Should().Be(actualInclude1);
                actualThirdItem.ItemType.Should().Be(actualItemType3);
                actualThirdItem.Include.Should().Be(actualInclude3);
                actualFourthItem.ItemType.Should().Be(actualItemType2);
                actualFourthItem.Include.Should().Be(actualInclude2);
            }
        }
        [TestClass]
        public class TheLoadMethod : ItemGroupTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadLabelAttribute()
            {
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup Label=\"" + expectedLabelValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItemGroup = root.ItemGroups.Single();
                actualItemGroup.Label.Should().Be(expectedLabelValue);
            }
            [TestMethod]
            public void ShouldLoadConditionAttribute()
            {
                string expectedConditionValue;
                //Arrange
                expectedConditionValue = "TestConditionValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup Condition=\"" + expectedConditionValue + "\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItemGroup = root.ItemGroups.Single();
                actualItemGroup.Condition.Should().Be(expectedConditionValue);
            }
            [TestMethod]
            public void ShouldLoadInnerItemTagAsAnItemElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup>" +
                                    "<Item Include=\"NotImportant\" />" +
                                "</ItemGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                root.ItemGroups.Single().Items.Single();
            }
        }
    }
}