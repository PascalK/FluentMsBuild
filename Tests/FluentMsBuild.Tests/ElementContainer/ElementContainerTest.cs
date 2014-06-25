using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class ElementContainerTest
    {
        ElementContainer unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = new TestElementContainer();
        }
        [TestClass]
        public class TheCountProperty : ElementContainerTest
        {
            [TestMethod]
            public void ShouldReturnTheNumberOfChildElements()
            {
                //Arrange
                unit.AppendChild(root.CreateImportElement("TestProject1"));
                unit.AppendChild(root.CreateImportElement("TestProject2"));
                //Act
                var actualResult = unit.Count;
                //Assert
                actualResult.Should().Be(2);
            }
        }
        [TestClass]
        public class TheFirstChildProperty : ElementContainerTest
        {
            [TestMethod]
            public void ShouldReturnNullWhenItHasNoChildren()
            {
                //Arrange
                //Act
                var actualResult = unit.FirstChild;
                //Assert
                actualResult.Should().BeNull();
            }
            [TestMethod]
            public void ShouldReturnFirstAddedChild()
            {
                Element expectedFirstElement;
                //Arrange
                expectedFirstElement = root.CreateImportElement("TestProject1");
                unit.AppendChild(expectedFirstElement);
                unit.AppendChild(root.CreateImportElement("TestProject2"));
                //Act
                var actualResult = unit.FirstChild;
                //Assert
                actualResult.Should().Be(expectedFirstElement);
            }
        }
        [TestClass]
        public class TheInsertBeforeChildMethod : ElementContainerTest
        {
            [TestMethod]
            public void ShouldAppendTheElementWhenReferenceIsNull()
            {
                Element expectedElement;
                Element expectedFirstElement;
                //Arrange
                expectedElement = root.CreateImportElement("TestProject1");
                expectedFirstElement = root.CreateImportElement("TestProject2");
                unit.AppendChild(expectedFirstElement);
                //Act
                unit.InsertBeforeChild(expectedElement, null);
                //Assert
                unit.LastChild.Should().Be(expectedElement);
            }
            [TestMethod]
            public void ShouldInsertTheElementBeforeTheReferenceAndSetTheParent()
            {
                Element expectedElement;
                Element expectedFirstElement;
                //Arrange
                expectedElement = root.CreateImportElement("TestProject1");
                expectedFirstElement = root.CreateImportElement("TestProject2");
                unit.AppendChild(expectedFirstElement);
                //Act
                unit.InsertBeforeChild(expectedElement, expectedFirstElement);
                //Assert
                unit.FirstChild.Should().Be(expectedElement);
                unit.FirstChild.Parent.Should().Be(unit);
            }
        }
        [TestClass]
        public class TheRemoveAllChildrenMethod : ElementContainerTest
        {
            [TestMethod]
            public void ShouldRemoveAllChildrenAndResetRemovedChildrensParentProperty()
            {
                Element actualChild1;
                Element actualChild2;
                //Arrange
                actualChild1 = root.CreateImportElement("TestProject1");
                actualChild2 = root.CreateImportElement("TestProject2");
                unit.AppendChild(actualChild1);
                unit.AppendChild(actualChild2);
                //Act
                unit.RemoveAllChildren();
                //Assert
                unit.Children.Should().BeEmpty();
                actualChild1.Parent.Should().BeNull();
                actualChild2.Parent.Should().BeNull();
            }
        }
        [TestClass]
        public class TheRemoveChildMethod : ElementContainerTest
        {
            [TestMethod]
            public void ShouldRemoveChildAndResetChildsParentProperty()
            {
                Element actualChild1;
                Element actualChild2;
                //Arrange
                actualChild1 = root.CreateImportElement("TestProject1");
                actualChild2 = root.CreateImportElement("TestProject2");
                unit.AppendChild(actualChild1);
                unit.AppendChild(actualChild2);
                //Act
                unit.RemoveChild(actualChild1);
                //Assert
                unit.Children.Single().Should().Be(actualChild2);
                actualChild1.Parent.Should().BeNull();
            }
        }
        [TestClass]
        public class TheLoadMethod : ElementContainerTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldFailLoadingAnElementInTheWrongNamespace()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<ItemDefinition xmlns=\"InvalidNamespace\" />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = () => Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidProjectFileException>();
            }
        }

        class TestElementContainer : ElementContainer
        {
            protected override string XmlName
            {
                get { return "TestElementContainer"; }
            }
            protected override Element LoadChildElement(XmlReader reader)
            {
                throw new NotImplementedException();
            }
        }
    }
}