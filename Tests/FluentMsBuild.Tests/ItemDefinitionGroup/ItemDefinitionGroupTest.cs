using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class ItemDefinitionGroupTest
    {
        ItemDefinitionGroup unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateItemDefinitionGroupElement();
        }
        [TestClass]
        public class TheAddItemDefinitionMethod : ItemDefinitionGroupTest
        {
            [TestMethod]
            public void ShouldAddItemDefinitionToProperty()
            {
                string actualItemType;
                //Arrange
                actualItemType = "TestItemType";
                //Act
                unit.AddItemDefinition(actualItemType);
                //Assert
                unit.ItemDefinitions.Single().ItemType.Should().Be(actualItemType);
            }
        }
        [TestClass]
        public class TheLoadMethod : ItemDefinitionGroupTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadChildElementAsItemDefinition()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemDefinitionGroup>" +
                                    "<ItemDefinition />" +
                                "</ItemDefinitionGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualItemDefinition = root.ItemDefinitionGroups.Single().ItemDefinitions.Single();
                actualItemDefinition.Should().NotBeNull();
            }
        }
    }
}
