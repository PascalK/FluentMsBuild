using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class PropertyGroupTest
    {
        PropertyGroup unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreatePropertyGroupElement();
        }

        [TestClass]
        public class AddPropertyMethod : PropertyGroupTest
        {
            //[TestMethod]
            //public void ShouldAddAnEmptyPropertyWhenCalledWithOnlyAName()
            //{
            //    string actualPropertyName;
            //    //Arrange
            //    actualPropertyName = "TestPropertyName1";
            //    //Act
            //    unit.AddProperty<string>(actualPropertyName);
            //    //Assert
            //    var actualProperty = unit.Properties.Single();
            //    actualProperty.Name.Should().Be(actualPropertyName);
            //    actualProperty.Value.Should().BeEmpty();
            //    actualProperty.Condition.Should().BeEmpty();
            //    actualProperty.Label.Should().BeEmpty();
            //}
            [TestMethod]
            public void ShouldAddACorrespondingPropertyWhenCalledWithANameAndValue()
            {
                string actualPropertyName;
                string actualPropertyValue;
                //Arrange
                actualPropertyName = "TestPropertyName1";
                actualPropertyValue = "TestPropertyValue1";
                //Act
                unit.AddProperty<string>(actualPropertyName, actualPropertyValue);
                //Assert
                var actualProperty = unit.Properties.Single();
                actualProperty.Name.Should().Be(actualPropertyName);
                actualProperty.Value.Should().Be(actualPropertyValue);
                actualProperty.Condition.Should().BeEmpty();
                actualProperty.Label.Should().BeEmpty();
            }
            //[TestMethod]
            //public void ShouldAddACorrespondingPropertyWhenCalledWithANameValueAndCondition()
            //{
            //    string actualPropertyName;
            //    string actualPropertyValue;
            //    string actualPropertyCondition;
            //    //Arrange
            //    actualPropertyName = "TestPropertyName1";
            //    actualPropertyValue = "TestPropertyValue1";
            //    actualPropertyCondition = "TestPropertyCondition1";
            //    //Act
            //    unit.AddProperty<string>(actualPropertyName, actualPropertyValue, actualPropertyCondition);
            //    //Assert
            //    var actualProperty = unit.Properties.Single();
            //    actualProperty.Name.Should().Be(actualPropertyName);
            //    actualProperty.Value.Should().Be(actualPropertyValue);
            //    actualProperty.Condition.Should().Be(actualPropertyCondition);
            //    actualProperty.Label.Should().BeEmpty();
            //}
            //[TestMethod]
            //public void ShouldAddACorrespondingPropertyWhenCalledWithANameValueConditionAndLabel()
            //{
            //    string actualPropertyName;
            //    string actualPropertyValue;
            //    string actualPropertyCondition;
            //    string actualPropertyLabel;
            //    //Arrange
            //    actualPropertyName = "TestPropertyName1";
            //    actualPropertyValue = "TestPropertyValue";
            //    actualPropertyCondition = "TestPropertyCondition";
            //    actualPropertyLabel = "TestPropertyLabel";
            //    //Act
            //    unit.AddProperty<string>(actualPropertyName, actualPropertyValue, actualPropertyCondition, actualPropertyLabel);
            //    //Assert
            //    var actualProperty = unit.Properties.Single();
            //    actualProperty.Name.Should().Be(actualPropertyName);
            //    actualProperty.Value.Should().Be(actualPropertyValue);
            //    actualProperty.Condition.Should().Be(actualPropertyCondition);
            //    actualProperty.Label.Should().Be(actualPropertyLabel);
            //}
            [TestMethod]
            public void ShouldAddPropertiesInOrder()
            {
                string actualName1;
                string actualValue1;
                string actualName2;
                string actualValue2;
                Property actualFirst;
                Property actualSecond;
                //Arrange
                actualName1 = "TestProperty1NameValue";
                actualValue1 = "TestProperty1ValueValue";
                actualName2 = "TestProperty2NameValue";
                actualValue2 = "TestProperty2ValueValue";
                //Act
                unit.AddProperty(actualName1, actualValue1);
                unit.AddProperty(actualName2, actualValue2);
                //Assert
                unit.Properties.Count.Should().Be(2);
                actualFirst = unit.Properties.ElementAt(0);
                actualSecond = unit.Properties.ElementAt(1);
                actualFirst.Name.Should().Be(actualName1);
                actualFirst.Value.Should().Be(actualValue1);
                actualSecond.Name.Should().Be(actualName2);
                actualSecond.Value.Should().Be(actualValue2);

                unit.PropertiesReversed.Count.Should().Be(2);
                actualFirst = unit.PropertiesReversed.ElementAt(0);
                actualSecond = unit.PropertiesReversed.ElementAt(1);
                actualFirst.Name.Should().Be(actualName2);
                actualFirst.Value.Should().Be(actualValue2);
                actualSecond.Name.Should().Be(actualName1);
                actualSecond.Value.Should().Be(actualValue1);
            }
            //[TestMethod]
            //public void CanBeChained()
            //{
            //    string actualPropertyName1;
            //    string actualPropertyName2;
            //    //Arrange
            //    actualPropertyName1 = "TestPropertyName1";
            //    actualPropertyName2 = "TestPropertyName2";
            //    //Act
            //    unit.AddProperty<string>(actualPropertyName1).And
            //        .AddProperty<string>(actualPropertyName2);
            //    //Assert
            //    unit.Properties.Count.Should().Be(2);
            //    unit.Properties.ElementAt(0).Name.Should().Be(actualPropertyName1);
            //    unit.Properties.ElementAt(1).Name.Should().Be(actualPropertyName2);
            //}
        }
        [TestClass]
        public class TheSetPropertyMethod : PropertyGroupTest
        {
            [TestMethod]
            public void ShouldAddNewPropertyWhenNoneWithCorrespondingNameExists()
            {
                string actualName;
                string actualValue;
                //Arrange
                actualName = "TestNameValue";
                actualValue = "TestValueValue";
                //Act
                var actualResult = unit.SetProperty(actualName, actualValue);
                //Assert
                var actualProperty = unit.Properties.Single();
                actualProperty.Name.Should().Be(actualName);
                actualProperty.Value.Should().Be(actualValue);
                actualProperty.Condition.Should().BeEmpty();
                actualProperty.Label.Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldUpdateExistingPropertyWhenOneWithCorrespondingNameExists()
            {
                string actualName;
                string actualValue1;
                string actualValue2;
                //Arrange
                actualName = "TestNameValue";
                actualValue1 = "TestValueValue";
                actualValue2 = "TestValueValue";
                var expectedProperty = unit.AddProperty(actualName, actualValue1).Element;
                //Act
                var actualResult = unit.SetProperty(actualName, actualValue2);
                //Assert
                var actualProperty = unit.Properties.Single();
                actualProperty.Name.Should().Be(actualName);
                actualProperty.Value.Should().Be(actualValue2);
                actualProperty.Condition.Should().BeEmpty();
                actualProperty.Label.Should().BeEmpty();
                actualProperty.Should().BeSameAs(expectedProperty);
            }
            [TestMethod]
            public void ShouldAddNewPropertyWhenOneWithCorrespondingNameExistsWichHasACondition()
            {
                string actualName;
                string actualValue1;
                string actualValue2;
                //Arrange
                actualName = "TestNameValue";
                actualValue1 = "TestValueValue";
                actualValue2 = "TestValueValue";
                var expectedProperty = unit.AddProperty(actualName, actualValue1).Element;
                expectedProperty.Condition = "AnyCondition";
                //Act
                var actualResult = unit.SetProperty(actualName, actualValue2);
                //Assert
                unit.Properties.Should().HaveCount(2);
                var actualProperty = unit.Properties.Last();
                actualProperty.Name.Should().Be(actualName);
                actualProperty.Value.Should().Be(actualValue2);
                actualProperty.Condition.Should().BeEmpty();
                actualProperty.Label.Should().BeEmpty();
                actualProperty.Should().NotBeSameAs(expectedProperty);
            }
        }
        [TestClass]
        public class TheLoadMethod : PropertyGroupTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadAnEmptyElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<PropertyGroup />" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualPropertyGroup = root.PropertyGroups.Single();
                actualPropertyGroup.Properties.Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldLoadInnerElementsAsProperties()
            {
                string propertyName;
                //Arrange
                propertyName = "TestPropertyNameValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<PropertyGroup>" +
                                    "<" + propertyName + " />" +
                                "</PropertyGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                var actualPropertyGroup = root.PropertyGroups.Single();
                actualPropertyGroup.Properties.First().Name.Should().Be(propertyName);
            }
            [TestMethod]
            public void ShouldFailOnPropertyWithInvalidName_ItemGroup()
            {
                string propertyName;
                //Arrange
                propertyName = "ItemGroup";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<PropertyGroup>" +
                                    "<" + propertyName + " />" +
                                "</PropertyGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = () => Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidProjectFileException>();
            }
            [TestMethod]
            public void ShouldFailOnPropertyWithInvalidName_PropertyGroup()
            {
                string propertyName;
                //Arrange
                propertyName = "PropertyGroup";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<PropertyGroup>" +
                                    "<" + propertyName + " />" +
                                "</PropertyGroup>" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = () => Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidProjectFileException>();
            }
        }
    }
}