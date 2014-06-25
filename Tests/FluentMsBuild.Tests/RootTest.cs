using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public partial class RootTest
    {
        protected Root unit;

        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        [TestInitialize]
        public void TestInitialize()
        {
            unit = Root.Create();
        }

        //[TestMethod]
        //public void ShouldBeImplicitlyCastableToProjectRootElementReturningTheRootElementUsedWhenInitializing()
        //{
        //    ProjectRootElement actualRootElement;
        //    //Arrange
        //    //Act
        //    actualRootElement = unit;
        //    //Assert
        //    actualRootElement.Should().BeSameAs(originalRootElement);
        //}
        [TestClass]
        public class TheAddPropertyMethod : RootTest
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
            [TestMethod]
            public void ShouldAddAPropertyToANewPropertyGroupWhenNoPropertyGroupsExist()
            {
                string actualPropertyName;
                string actualPropertyValue;
                //Arrange
                actualPropertyName = "TestPropertyName1";
                actualPropertyValue = "TestPropertyValue1";
                //Act
                var actualAddedProperty = unit.AddProperty<string>(actualPropertyName, actualPropertyValue).Element;
                //Assert
                var actualProperty = unit.PropertyGroups.Single().Properties.Single();
                actualProperty.Should().Be(actualAddedProperty);
            }
            [TestMethod]
            public void ShouldAddAPropertyToAnExistingPropertyGroupWhenItDoesNotContainAPropertyWithTheSameName()
            {
                string actualPropertyName;
                string actualPropertyValue;
                //Arrange
                actualPropertyName = "TestPropertyName1";
                actualPropertyValue = "TestPropertyValue1";
                var actualAddedPropertyGroup = unit.AddPropertyGroup().Element;
                //Act
                var actualAddedProperty = unit.AddProperty<string>(actualPropertyName, actualPropertyValue).Element;
                //Assert
                var actualPropertyGroup = unit.PropertyGroups.Single();
                var actualProperty = actualPropertyGroup.Properties.Single();
                actualPropertyGroup.Should().Be(actualAddedPropertyGroup);
                actualProperty.Should().Be(actualAddedProperty);
            }
            [TestMethod]
            public void ShouldAddAPropertyToANewPropertyGroupWhenExistingOneHasACondition()
            {
                string actualPropertyName;
                string actualPropertyValue;
                //Arrange
                actualPropertyName = "TestPropertyName1";
                actualPropertyValue = "TestPropertyValue1";
                var actualAddedPropertyGroup = unit.AddPropertyGroup().Element;
                actualAddedPropertyGroup.Condition = "AnyCondition";
                //Act
                var actualAddedProperty = unit.AddProperty<string>(actualPropertyName, actualPropertyValue).Element;
                //Assert
                unit.PropertyGroups.Should().HaveCount(2);
                var actualPropertyGroup = unit.PropertyGroups.Last();
                var actualProperty = actualPropertyGroup.Properties.Single();
                actualPropertyGroup.Should().NotBe(actualAddedPropertyGroup);
                actualProperty.Should().Be(actualAddedProperty);
            }
            [TestMethod]
            public void ShouldUpdateAnExistingPropertyWithTheSameNameWhenItDoesNotHaveACondition()
            {
                string actualPropertyName;
                string actualPropertyValue1;
                string actualPropertyValue2;
                //Arrange
                actualPropertyName = "TestPropertyName1";
                actualPropertyValue1 = "TestPropertyValue1";
                actualPropertyValue2 = "TestPropertyValue2";
                var actualAddedProperty1 = unit.AddProperty(actualPropertyName, actualPropertyValue1).Element;
                //Act
                var actualAddedProperty2 = unit.AddProperty<string>(actualPropertyName, actualPropertyValue2).Element;
                //Assert
                var actualPropertyGroup = unit.PropertyGroups.Single();
                var actualProperty = actualPropertyGroup.Properties.Single();
                actualProperty.Should().Be(actualAddedProperty1).And.Be(actualAddedProperty2);
                actualProperty.Name.Should().Be(actualPropertyName);
                actualProperty.Value.Should().Be(actualPropertyValue2);
            }
            [TestMethod]
            public void ShouldAddANewPropertyWithTheSameNameWhenItDoesHaveACondition()
            {
                string actualPropertyName;
                string actualPropertyValue1;
                string actualPropertyValue2;
                //Arrange
                actualPropertyName = "TestPropertyName1";
                actualPropertyValue1 = "TestPropertyValue1";
                actualPropertyValue2 = "TestPropertyValue2";
                var actualAddedProperty1 = unit.AddProperty(actualPropertyName, actualPropertyValue1).Element;
                actualAddedProperty1.Condition = "AnyCondition";
                //Act
                var actualAddedProperty2 = unit.AddProperty<string>(actualPropertyName, actualPropertyValue2).Element;
                //Assert
                var actualPropertyGroup = unit.PropertyGroups.Single();
                actualPropertyGroup.Properties.Should().HaveCount(2);
                var actualProperty = actualPropertyGroup.Properties.Last();
                actualProperty.Should().NotBe(actualAddedProperty1);
                actualProperty.Should().Be(actualAddedProperty2);
                actualProperty.Name.Should().Be(actualPropertyName);
                actualProperty.Value.Should().Be(actualPropertyValue2);
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
            //[TestMethod]
            //public void ShouldAddPropertiesInOrder()
            //{
            //    string actualName1;
            //    string actualValue1;
            //    string actualName2;
            //    string actualValue2;
            //    Property actualFirst;
            //    Property actualSecond;
            //    //Arrange
            //    actualName1 = "TestProperty1NameValue";
            //    actualValue1 = "TestProperty1ValueValue";
            //    actualName2 = "TestProperty2NameValue";
            //    actualValue2 = "TestProperty2ValueValue";
            //    //Act
            //    unit.AddProperty(actualName1, actualValue1);
            //    unit.AddProperty(actualName2, actualValue2);
            //    //Assert
            //    unit.Properties.Count.Should().Be(2);
            //    actualFirst = unit.Properties.ElementAt(0);
            //    actualSecond = unit.Properties.ElementAt(1);
            //    actualFirst.Name.Should().Be(actualName1);
            //    actualFirst.Value.Should().Be(actualValue1);
            //    actualSecond.Name.Should().Be(actualName2);
            //    actualSecond.Value.Should().Be(actualValue2);

            //    unit.PropertiesReversed.Count.Should().Be(2);
            //    actualFirst = unit.PropertiesReversed.ElementAt(0);
            //    actualSecond = unit.PropertiesReversed.ElementAt(1);
            //    actualFirst.Name.Should().Be(actualName2);
            //    actualFirst.Value.Should().Be(actualValue2);
            //    actualSecond.Name.Should().Be(actualName1);
            //    actualSecond.Value.Should().Be(actualValue1);
            //}
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
        public class TheHasUnsavedChangesProperty : RootTest
        {
            [TestMethod]
            public void ShouldAlwaysReturnTrue()
            {
                //Arrange
                //Act
                var actualResult = unit.HasUnsavedChanges;
                //Assert
                actualResult.Should().BeTrue();
            }
        }
        [TestClass]
        public class TheLastWriteTimeWhenReadProperty : RootTest
        {
            [TestMethod]
            public void ShouldAlwaysReturnMinValue()
            {
                //Arrange
                //Act
                var actualResult = unit.LastWriteTimeWhenRead;
                //Assert
                actualResult.Should().Be(DateTime.MinValue);
            }
        }
        [TestClass]
        public class TheTimeLastChangedProperty : RootTest
        {
            [TestMethod]
            public void ShouldAlwaysReturnMinValue()
            {
                //Arrange
                //Act
                var actualResult = unit.TimeLastChanged;
                //Assert
                actualResult.Should().BeCloseTo(DateTime.Now);
            }
        }

        [TestClass]
        public class TheRawXmlProperty : RootTest
        {
            [TestMethod]
            public void ShouldReturnTheCurrentProjectAsXml()
            {
                string expectedImportProject;
                //Arrange
                expectedImportProject = "TestImportProject";
                unit.AddImport(expectedImportProject);
                //Act
                var actualResult = unit.RawXml;
                //Assert
                actualResult.Should().Contain(expectedImportProject);
            }
        }
        [TestClass]
        public class TheVersionProperty : RootTest
        {
            [TestMethod]
            public void ShouldAlwaysReturZero()
            {
                //Arrange
                //Act
                var actualResult = unit.Version;
                //Assert
                actualResult.Should().Be(0);
            }
        }
        [TestClass]
        public class TheToolsVersionProperty : RootTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.ToolsVersion = null;
                //Act
                var actualResult = unit.ToolsVersion;
                //Assert
                actualResult.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheConditionProperty : RootTest
        {
            [TestMethod]
            public void ShouldNotBeSettable()
            {
                //Arrange
                //Act
                Action action = () => unit.Condition = null;
                //Assert
                action.ShouldThrow<InvalidOperationException>(because: "Conditions are not applicable to Root elements");
            }
            [TestMethod]
            public void ShouldAlwaysReturnNull()
            {
                //Arrange
                //Act
                var actualResult = unit.Condition;
                //Assert
                actualResult.Should().BeNull();
            }
        }
        [TestClass]
        public class TheItemsProperty : RootTest
        {
            [TestMethod]
            public void ShouldReturnAllItemsInTheProjectRecursively()
            {
                //Arrange
                unit = Root.Create();
                var actualAddedItem1 = unit.AddItemGroup().Element.AddItem("Item1", "Include1").Element;
                var actualAddedItem2 = unit.AddItemGroup().Element.AddItem("Item2", "Include2").Element;
                //Act
                var actualResult = unit.Items;
                //Assert
                actualResult.Should().HaveCount(2);
                actualResult.Should().Contain(actualAddedItem1);
                actualResult.Should().Contain(actualAddedItem2);
            }
        }
        [TestClass]
        public class TheItemDefinitionsProperty : RootTest
        {
            [TestMethod]
            public void ShouldReturnAllItemDefinitionsInTheProjectRecursively()
            {
                //Arrange
                unit = Root.Create();
                var actualAddedItemDefinition1 = unit.AddItemDefinitionGroup().Element.AddItemDefinition("ItemType1").Element;
                var actualAddedItemDefinition2 = unit.AddItemDefinitionGroup().Element.AddItemDefinition("ItemType2").Element;
                //Act
                var actualResult = unit.ItemDefinitions;
                //Assert
                actualResult.Should().HaveCount(2);
                actualResult.Should().Contain(actualAddedItemDefinition1);
                actualResult.Should().Contain(actualAddedItemDefinition2);
            }
        }
        [TestClass]
        public class TheImportGroupsProperty : RootTest
        {
            [TestMethod]
            public void ShouldReturnImportGroupsInTheOrderTheyWhereAdded()
            {
                //Arrange
                //Act
                var firstImportGroup = unit.AddImportGroup().Element;
                var secondImportGroup = unit.AddImportGroup().Element;
                //Assert
                unit.ImportGroups.ElementAt(0).Should().Be(firstImportGroup);
                unit.ImportGroups.ElementAt(1).Should().Be(secondImportGroup);
            }
        }
        [TestClass]
        public class TheImportGroupsReversedProperty : RootTest
        {
            [TestMethod]
            public void ShouldReturnImportGroupsInReversedOrder()
            {
                //Arrange
                //Act
                var firstImportGroup = unit.AddImportGroup().Element;
                var secondImportGroup = unit.AddImportGroup().Element;
                //Assert
                unit.ImportGroupsReversed.ElementAt(0).Should().Be(secondImportGroup);
                unit.ImportGroupsReversed.ElementAt(1).Should().Be(firstImportGroup);
            }
        }
        [TestClass]
        public class TheItemDefinitionGroupsProperty : RootTest
        {
            [TestMethod]
            public void ShouldReturnItemDefinitionGroupsInTheOrderTheyWhereAdded()
            {
                //Arrange
                //Act
                var firstItemDefinitionGroup = unit.AddItemDefinitionGroup().Element;
                var secondItemDefinitionGroup = unit.AddItemDefinitionGroup().Element;
                //Assert
                unit.ItemDefinitionGroups.ElementAt(0).Should().Be(firstItemDefinitionGroup);
                unit.ItemDefinitionGroups.ElementAt(1).Should().Be(secondItemDefinitionGroup);
            }
        }
        [TestClass]
        public class TheItemDefinitionGroupsReversedProperty : RootTest
        {
            [TestMethod]
            public void ShouldReturnItemDefinitionGroupsInReversedOrder()
            {
                //Arrange
                //Act
                var firstItemDefinitionGroup = unit.AddItemDefinitionGroup().Element;
                var secondItemDefinitionGroup = unit.AddItemDefinitionGroup().Element;
                //Assert
                unit.ItemDefinitionGroupsReversed.ElementAt(0).Should().Be(secondItemDefinitionGroup);
                unit.ItemDefinitionGroupsReversed.ElementAt(1).Should().Be(firstItemDefinitionGroup);
            }
        }
        [TestClass]
        public class AddImportMethod : RootTest
        {
            [TestMethod]
            public void ShouldAddACorrespondingImportWhenCalledWithProject()
            {
                string actualProject;
                //Arrange
                actualProject = "TestProjectValue";
                //Act
                unit.AddImport(actualProject);
                //Assert
                var actualImport = unit.Imports.Single();
                actualImport.Project.Should().Be(actualProject);
                actualImport.Condition.Should().BeEmpty();
                actualImport.Label.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheAddItemMethod : RootTest
        {
            [TestMethod]
            public void ShouldAddAnItemToANewGroupWhenThereAreNoGroups()
            {
                string expectedItemType;
                //Arrange
                expectedItemType = "TestItemTypeValue";
                //Act
                var actualAddedItem = unit.AddItem(expectedItemType, null).Element;
                //Assert
                var actualItem = unit.ItemGroups.Single().Items.Single();
                actualAddedItem.Should().Be(actualItem);
            }
            [TestMethod]
            public void ShouldAddAnItemToAnExistingGroupWithItemsWithTheSameItemType()
            {
                string expectedItemType;
                //Arrange
                expectedItemType = "TestItemTypeValue";
                var actualAddedItemGroup = unit.AddItemGroup().Element;
                actualAddedItemGroup.AddItem(expectedItemType, null);
                //Act
                var actualAddedItem = unit.AddItem(expectedItemType, null).Element;
                //Assert
                var actualItemGroup = unit.ItemGroups.Single();
                actualItemGroup.Items.Should().Contain(actualAddedItem).And.HaveCount(2);
            }
            [TestMethod]
            public void ShouldAddAnItemToNewItemGroupWhenTheGroupWithItemsWithTheSameItemTypeHasACondition()
            {
                string expectedItemType;
                //Arrange
                expectedItemType = "TestItemTypeValue";
                var actualAddedItemGroup = unit.AddItemGroup().Element;
                actualAddedItemGroup.Condition = "RandomCondition";
                actualAddedItemGroup.AddItem(expectedItemType, null);
                //Act
                var actualAddedItem = unit.AddItem(expectedItemType, null).Element;
                //Assert
                unit.ItemGroups.Should().HaveCount(2);
                unit.ItemGroups.ElementAt(0).Items.Should().NotContain(actualAddedItem);
                unit.ItemGroups.ElementAt(1).Items.Should().Contain(actualAddedItem);
            }
        }
        [TestClass]
        public class TheAddItemDefinitionMethod : RootTest
        {
            [TestMethod]
            public void ShouldAddAnItemToANewGroupWhenThereAreNoGroups()
            {
                string expectedItemType;
                //Arrange
                expectedItemType = "TestItemTypeValue";
                //Act
                var actualAddedItemDefinition = unit.AddItemDefinition(expectedItemType).Element;
                //Assert
                var actualItemDefinition = unit.ItemDefinitionGroups.Single().ItemDefinitions.Single();
                actualAddedItemDefinition.Should().Be(actualItemDefinition);
            }
            [TestMethod]
            public void ShouldAddAnItemToAnExistingGroupWithItemsWithTheSameItemType()
            {
                string expectedItemType;
                //Arrange
                expectedItemType = "TestItemTypeValue";
                var actualAddedItemDefinitionGroup = unit.AddItemDefinitionGroup().Element;
                actualAddedItemDefinitionGroup.AddItemDefinition(expectedItemType);
                //Act
                var actualAddedItemDefinition = unit.AddItemDefinition(expectedItemType).Element;
                //Assert
                var actualItemDefinitionGroup = unit.ItemDefinitionGroups.Single();
                actualItemDefinitionGroup.ItemDefinitions.Should().Contain(actualAddedItemDefinition).And.HaveCount(2);
            }
            [TestMethod]
            public void ShouldAddAnItemToNewItemGroupWhenTheGroupWithItemsWithTheSameItemTypeHasACondition()
            {
                string expectedItemType;
                //Arrange
                expectedItemType = "TestItemTypeValue";
                var actualAddedItemDefinitionGroup = unit.AddItemDefinitionGroup().Element;
                actualAddedItemDefinitionGroup.Condition = "RandomCondition";
                actualAddedItemDefinitionGroup.AddItemDefinition(expectedItemType);
                //Act
                var actualAddedItemDefinition = unit.AddItemDefinition(expectedItemType).Element;
                //Assert
                unit.ItemDefinitionGroups.Should().HaveCount(2);
                unit.ItemDefinitionGroups.ElementAt(0).ItemDefinitions.Should().NotContain(actualAddedItemDefinition);
                unit.ItemDefinitionGroups.ElementAt(1).ItemDefinitions.Should().Contain(actualAddedItemDefinition);
            }
        }
        [TestClass]
        public class TheCreateMethod : RootTest
        {
            [TestMethod]
            public void ShouldCreatedARootWithTheSpecifiedFullPath()
            {
                string expectedPath;
                //Arrange
                expectedPath = Path.Combine(TestContext.TestResultsDirectory, "TestPath");
                //Act
                var actualResult = Root.Create(expectedPath);
                //Assert
                actualResult.FullPath.Should().Be(expectedPath);
            }
        }
        [TestClass]
        public class TheOpenMethod : RootTest
        {
            [TestMethod]
            [DeploymentItem(@"TestProjectFile.Open.xml")]
            public void ShouldLoadEmptyProjectFileFromSpecifiedPath()
            {
                string expectedPath;
                //Arrange
                expectedPath = Path.Combine(TestContext.TestDeploymentDir, "TestProjectFile.Open.xml");
                //Act
                var actualResult = Root.Open(expectedPath);
                //Assert
                actualResult.Should().NotBeNull();
                actualResult.FullPath.Should().Be(expectedPath);
            }
            [TestMethod]
            [DeploymentItem(@"TestProjectFile.Invalid.xml")]
            public void ShouldFailWhenTryingToLoadInvalidXml()
            {
                string expectedPath;
                //Arrange
                expectedPath = Path.Combine(TestContext.TestDeploymentDir, "TestProjectFile.Invalid.xml");
                //Act
                Action action = () => Root.Open(expectedPath);
                //Assert
                action.ShouldThrow<InvalidProjectFileException>();
            }
        }
        [TestClass]
        public class TheSaveMethod : RootTest
        {
            [TestMethod]
            public void ShouldFailIfNoPathHasBeenSpecified()
            {
                //Arrange
                unit = Root.Create();
                //Act
                Action action = () => unit.Save();
                //Assert
                action.ShouldThrow<InvalidOperationException>();
            }
            [TestMethod]
            public void ShouldSaveInTheProjectsPath()
            {
                string expectedFullPath;
                //Arrange
                expectedFullPath = Path.Combine(TestContext.TestRunResultsDirectory, string.Format("{0}.xml", TestContext.TestName));
                unit = Root.Create();
                unit.FullPath = expectedFullPath;
                //Act
                unit.Save();
                //Assert
                File.Exists(expectedFullPath).Should().BeTrue();
            }
            [TestMethod]
            public void ShouldSaveInTheSpecifiedPath()
            {
                string expectedFullPath;
                //Arrange
                expectedFullPath = Path.Combine(TestContext.TestRunResultsDirectory, string.Format("{0}.xml", TestContext.TestName));
                unit = Root.Create();
                //Act
                unit.Save(expectedFullPath);
                //Assert
                File.Exists(expectedFullPath).Should().BeTrue();
            }
            [TestMethod]
            public void ShouldSaveInTheSpecifiedPathWithEncoding()
            {
                string expectedFullPath;
                //Arrange
                expectedFullPath = Path.Combine(TestContext.TestRunResultsDirectory, string.Format("{0}.xml", TestContext.TestName));
                unit = Root.Create();
                //Act
                unit.Save(expectedFullPath, Encoding.UTF32);
                //Assert
                File.Exists(expectedFullPath).Should().BeTrue();
            }
        }
        [TestClass]
        public class TheCreateChooseElementMethod : RootTest
        {
            [TestMethod]
            public void ShouldCreateAChooseElementButNotAddItToTheChooseElementsCollection()
            {
                //Arrange
                //Act
                var actualResult = unit.CreateChooseElement();
                //Assert
                actualResult.Should().NotBeNull();
                unit.ChooseElements.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheAddChooseElementMethod : RootTest
        {
            [TestMethod]
            public void ShouldAddANewChooseElementAndAddItToTheChooseElementsCollection()
            {
                //Arrange
                //Act
                var actualResult = unit.AddChooseElement().Element;
                //Assert
                actualResult.Should().NotBeNull();
                unit.ChooseElements.Single().Should().Be(actualResult);
            }
        }
        [TestClass]
        public class TheCreateOtherwiseElementMethod : RootTest
        {
            [TestMethod]
            public void ShouldCreateAnOtherwiseElement()
            {
                //Arrange
                //Act
                var actualResult = unit.CreateOtherwiseElement();
                //Assert
                actualResult.Should().NotBeNull();
            }
        }
        [TestClass]
        public class TheCreateWhenElementMethod : RootTest
        {
            [TestMethod]
            public void ShouldCreateAWhenElementWithTheCorrespondingCondition()
            {
                string actualCondition;
                //Arrange
                actualCondition = "TestConditionValue";
                //Act
                var actualResult = unit.CreateWhenElement(actualCondition);
                //Assert
                actualResult.Should().NotBeNull();
                actualResult.Condition.Should().Be(actualCondition);
            }
        }
        [TestClass]
        public class TheLoadMethod : RootTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadAnEmptyProjectElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                unit = Root.Create(reader);
                //Assert
                unit.Should().NotBeNull();
            }
            [TestMethod]
            public void ShouldLoadDefaultTargetsAttribute()
            {
                string expectedDefaultTargetsValue = "TestDefaultTargetsValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\" DefaultTargets=\"" + expectedDefaultTargetsValue + "\">" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                unit = Root.Create(reader);
                //Assert
                unit.DefaultTargets.Should().Be(expectedDefaultTargetsValue);
            }
            [TestMethod]
            public void ShouldLoadInitialTargetsAttribute()
            {
                string expectedInitialTargetsValue = "TestInitialTargetsValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\" InitialTargets=\"" + expectedInitialTargetsValue + "\">" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                unit = Root.Create(reader);
                //Assert
                unit.InitialTargets.Should().Be(expectedInitialTargetsValue);
            }
            [TestMethod]
            public void ShouldLoadToolsVersionAttribute()
            {
                string expectedToolsVersionValue = "TestToolsVersionValue";
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\" ToolsVersion=\"" + expectedToolsVersionValue + "\">" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                unit = Root.Create(reader);
                //Assert
                unit.ToolsVersion.Should().Be(expectedToolsVersionValue);
            }
            [TestMethod]
            public void ShouldFailOnInCorrectXmlNamespace()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project xmlns=\"InvalidXmlns\">" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = () => Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidProjectFileException>();
            }
            [TestMethod]
            public void ShouldFailOnUnknownAttribute()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\" unknownAttributeName=\"unknownAttributeValue\">" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                Action action = () => Root.Create(reader);
                //Assert
                action.ShouldThrow<InvalidProjectFileException>();
            }
            [TestMethod]
            public void ShouldFailOnUnknownElement()
            {
                //Arrange
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<RandomUnknownTag />" +
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