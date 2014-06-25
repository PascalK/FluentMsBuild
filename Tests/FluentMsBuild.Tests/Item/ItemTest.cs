using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class ItemTest
    {
        Item unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateItemElement(string.Empty);
        }

        [TestClass]
        public class TheExcludeProperty : ItemTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Exclude = null;
                //Act
                var actualExclude = unit.Exclude;
                //Assert
                actualExclude.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheHasMetadataProperty : ItemTest
        {
            [TestMethod]
            public void ShouldBeTrueWhenMetadataHasBeenAdded()
            {
                bool actualResult;
                //Arrange
                unit.AddMetadata(null, null);
                //Act
                actualResult = unit.HasMetadata;
                //Assert
                actualResult.Should().BeTrue();
            }
            [TestMethod]
            public void ShouldBeFalseWhenNoMetadataHasBeenAdded()
            {
                bool actualResult;
                //Arrange
                //Act
                actualResult = unit.HasMetadata;
                //Assert
                actualResult.Should().BeFalse();
            }
        }
        [TestClass]
        public class TheIncludeProperty : ItemTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Include = null;
                //Act
                var actualInclude = unit.Include;
                //Assert
                actualInclude.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheItemTypeProperty : ItemTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.ItemType = null;
                //Act
                var actualItemType = unit.ItemType;
                //Assert
                actualItemType.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheRemoveProperty : ItemTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Remove = null;
                //Act
                var actualRemove = unit.Remove;
                //Assert
                actualRemove.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheKeepDuplicatesProperty : ItemTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.KeepDuplicates = null;
                //Act
                var actualKeepDuplicates = unit.KeepDuplicates;
                //Assert
                actualKeepDuplicates.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheKeepMetadataProperty : ItemTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.KeepMetadata = null;
                //Act
                var actualKeepMetadata = unit.KeepMetadata;
                //Assert
                actualKeepMetadata.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheRemoveMetadataProperty : ItemTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.RemoveMetadata = null;
                //Act
                var actualRemoveMetadata = unit.RemoveMetadata;
                //Assert
                actualRemoveMetadata.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheAddMetadataMethod : ItemTest
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
        [TestClass]
        public class TheLoadMethod : ItemTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldFailWhenIncludeAndRemoveAttributesAreMissing()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup>" +
                                    "<Item />" +
                                "</ItemGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = () => Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidOperationException>();
            }
            [TestMethod]
            public void ShouldLoadIncludeAttribute()
            {
                string expectedIncludeValue;
                //Arrange
                expectedIncludeValue = "TestIncludeValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup>" +
                                    "<Item Include=\"" + expectedIncludeValue + "\" />" +
                                "</ItemGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItem = root.ItemGroups.Single().Items.Single();
                actualItem.Include.Should().Be(expectedIncludeValue);
            }
            [TestMethod]
            public void ShouldLoadRemoveAttribute()
            {
                string expectedRemoveValue = "TestRemoveValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup>" +
                                    "<Item Remove=\"" + expectedRemoveValue + "\" />" +
                                "</ItemGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItem = root.ItemGroups.Single().Items.Single();
                actualItem.Remove.Should().Be(expectedRemoveValue);
            }
            [TestMethod]
            public void ShouldLoadExcludeAttribute()
            {
                string expectedExcludeValue = "TestExcludeValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup>" +
                                    "<Item Include=\"NotImportant\" Exclude=\"" + expectedExcludeValue + "\" />" +
                                "</ItemGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItem = root.ItemGroups.Single().Items.Single();
                actualItem.Exclude.Should().Be(expectedExcludeValue);
            }
            [TestMethod]
            public void ShouldLoadKeepDuplicatesAttribute()
            {
                string expectedKeepDuplicatesValue = "TestKeepDuplicatesValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup>" +
                                    "<Item Include=\"NotImportant\" KeepDuplicates=\"" + expectedKeepDuplicatesValue + "\" />" +
                                "</ItemGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItem = root.ItemGroups.Single().Items.Single();
                actualItem.KeepDuplicates.Should().Be(expectedKeepDuplicatesValue);
            }
            [TestMethod]
            public void ShouldLoadKeepMetadataAttribute()
            {
                string expectedKeepMetadataValue = "TestKeepMetadataValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup>" +
                                    "<Item Include=\"NotImportant\" KeepMetadata=\"" + expectedKeepMetadataValue + "\" />" +
                                "</ItemGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItem = root.ItemGroups.Single().Items.Single();
                actualItem.KeepMetadata.Should().Be(expectedKeepMetadataValue);
            }
            [TestMethod]
            public void ShouldLoadRemoveMetadataAttribute()
            {
                string expectedRemoveMetadataValue = "TestRemoveMetadataValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup>" +
                                    "<Item Include=\"NotImportant\" RemoveMetadata=\"" + expectedRemoveMetadataValue + "\" />" +
                                "</ItemGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItem = root.ItemGroups.Single().Items.Single();
                actualItem.RemoveMetadata.Should().Be(expectedRemoveMetadataValue);
            }
            [TestMethod]
            public void ShouldLoadConditionAttribute()
            {
                string expectedConditionValue = "TestConditionValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup>" +
                                    "<Item Include=\"NotImportant\" Condition=\"" + expectedConditionValue + "\" />" +
                                "</ItemGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItem = root.ItemGroups.Single().Items.Single();
                actualItem.Condition.Should().Be(expectedConditionValue);
            }
            [TestMethod]
            public void ShouldLoadLabelAttribute()
            {
                string expectedLabelValue = "TestLabelValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup>" +
                                    "<Item Include=\"NotImportant\" Label=\"" + expectedLabelValue + "\" />" +
                                "</ItemGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItem = root.ItemGroups.Single().Items.Single();
                actualItem.Label.Should().Be(expectedLabelValue);
            }
            [TestMethod]
            public void ShouldLoadChildElementAsMetadata()
            {
                string expectedMetadataName;

                //Arrange
                expectedMetadataName = "TestMedatadatanameValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemGroup>" +
                                    "<Item Include=\"NotImportant\">" +
                                        "<" + expectedMetadataName + " />" +
                                    "</Item>" +
                                "</ItemGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualMetadata = root.ItemGroups.Single().Items.Single().Metadata.Single();
                actualMetadata.Name.Should().Be(expectedMetadataName);
            }
        }
        [TestClass]
        public class TheSaveMethod : ItemTest
        {
            [TestMethod]
            public void ShouldWriteTheLabelAttribute()
            {
                StringWriter stringWriter;
                string expectedLabelValue;
                //Arrange
                expectedLabelValue = "TestLabelValue";
                root = Root.Create();
                root.AddItem(null, null).Element.Label = expectedLabelValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Label", expectedLabelValue));
            }
            [TestMethod]
            public void ShouldWriteTheConditionAttribute()
            {
                StringWriter stringWriter;
                string expectedConditionValue;
                //Arrange
                expectedConditionValue = "TestConditionValue";
                root = Root.Create();
                root.AddItem(null, null).Element.Condition = expectedConditionValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Condition", expectedConditionValue));
            }
            [TestMethod]
            public void ShouldWriteTheIncludeAttribute()
            {
                StringWriter stringWriter;
                string expectedIncludeValue;
                //Arrange
                expectedIncludeValue = "TestIncludeValue";
                root = Root.Create();
                root.AddItem(null, null).Element.Include = expectedIncludeValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Include", expectedIncludeValue));
            }
            [TestMethod]
            public void ShouldWriteTheExcludeAttribute()
            {
                StringWriter stringWriter;
                string expectedExcludeValue;
                //Arrange
                expectedExcludeValue = "TestExcludeValue";
                root = Root.Create();
                root.AddItem(null, null).Element.Exclude = expectedExcludeValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Exclude", expectedExcludeValue));
            }
            [TestMethod]
            public void ShouldWriteTheKeepDuplicatesAttribute()
            {
                StringWriter stringWriter;
                string expectedKeepDuplicatesValue;
                //Arrange
                expectedKeepDuplicatesValue = "TestKeepDuplicatesValue";
                root = Root.Create();
                root.AddItem(null, null).Element.KeepDuplicates = expectedKeepDuplicatesValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("KeepDuplicates", expectedKeepDuplicatesValue));
            }
            [TestMethod]
            public void ShouldWriteTheKeepMetadataAttribute()
            {
                StringWriter stringWriter;
                string expectedKeepMetadataValue;
                //Arrange
                expectedKeepMetadataValue = "TestKeepMetadataValue";
                root = Root.Create();
                root.AddItem(null, null).Element.KeepMetadata = expectedKeepMetadataValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("KeepMetadata", expectedKeepMetadataValue));
            }
            [TestMethod]
            public void ShouldWriteTheRemoveMetadataAttribute()
            {
                StringWriter stringWriter;
                string expectedRemoveMetadataValue;
                //Arrange
                expectedRemoveMetadataValue = "TestRemoveMetadataValue";
                root = Root.Create();
                root.AddItem(null, null).Element.RemoveMetadata = expectedRemoveMetadataValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("RemoveMetadata", expectedRemoveMetadataValue));
            }
            [TestMethod]
            public void ShouldWriteTheRemoveAttribute()
            {
                StringWriter stringWriter;
                string expectedRemoveValue;
                //Arrange
                expectedRemoveValue = "TestRemoveValue";
                root = Root.Create();
                root.AddItem(null, null).Element.Remove = expectedRemoveValue;
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlAttribute("Remove", expectedRemoveValue));
            }
            string XmlAttribute(string name, string value)
            {
                return string.Format("{0}=\"{1}\"", name, value);
            }
        }
    }
}