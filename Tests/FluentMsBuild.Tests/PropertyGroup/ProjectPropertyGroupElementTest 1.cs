using FluentAssertions;
using Microsoft.Build.Construction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FluentMsBuild.PropertyGroup
{
    [TestClass]
    public class ProjectPropertyGroupElementTest
    {
        protected ProjectPropertyGroupElement unit;

        [TestInitialize]
        public void InitializeProjectPropertyGroupElement()
        {
            unit = ProjectRootElement.Create().AddPropertyGroup();
        }

        [TestClass]
        public class AddPropertyMethod : ProjectRootElementTest
        {
            [TestMethod]
            public void ShouldAddAnEmptyPropertyWhenCalledWithOnlyAName()
            {
                string actualPropertyName;
                //Arrange
                actualPropertyName = "TestPropertyName1";
                //Act
                unit.AddProperty<string>(actualPropertyName);
                //Assert
                var actualProperty = unit.Properties.Single();
                actualProperty.Name.Should().Be(actualPropertyName);
                actualProperty.Value.Should().BeEmpty();
                actualProperty.Condition.Should().BeEmpty();
                actualProperty.Label.Should().BeEmpty();
            }
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
            public void ShouldAddACorrespondingPropertyWhenCalledWithANameValueAndCondition()
            {
                string actualPropertyName;
                string actualPropertyValue;
                string actualPropertyCondition;
                //Arrange
                actualPropertyName = "TestPropertyName1";
                actualPropertyValue = "TestPropertyValue1";
                actualPropertyCondition = "TestPropertyCondition1";
                //Act
                unit.AddProperty<string>(actualPropertyName, actualPropertyValue, actualPropertyCondition);
                //Assert
                var actualProperty = unit.Properties.Single();
                actualProperty.Name.Should().Be(actualPropertyName);
                actualProperty.Value.Should().Be(actualPropertyValue);
                actualProperty.Condition.Should().Be(actualPropertyCondition);
                actualProperty.Label.Should().BeEmpty();
            }
            [TestMethod]
            public void ShouldAddACorrespondingPropertyWhenCalledWithANameValueConditionAndLabel()
            {
                string actualPropertyName;
                string actualPropertyValue;
                string actualPropertyCondition;
                string actualPropertyLabel;
                //Arrange
                actualPropertyName = "TestPropertyName1";
                actualPropertyValue = "TestPropertyValue";
                actualPropertyCondition = "TestPropertyCondition";
                actualPropertyLabel = "TestPropertyLabel";
                //Act
                unit.AddProperty<string>(actualPropertyName, actualPropertyValue, actualPropertyCondition, actualPropertyLabel);
                //Assert
                var actualProperty = unit.Properties.Single();
                actualProperty.Name.Should().Be(actualPropertyName);
                actualProperty.Value.Should().Be(actualPropertyValue);
                actualProperty.Condition.Should().Be(actualPropertyCondition);
                actualProperty.Label.Should().Be(actualPropertyLabel);
            }
            [TestMethod]
            public void CanBeChained()
            {
                string actualPropertyName1;
                string actualPropertyName2;
                //Arrange
                actualPropertyName1 = "TestPropertyName1";
                actualPropertyName2 = "TestPropertyName2";
                //Act
                unit.AddProperty<string>(actualPropertyName1).And
                    .AddProperty<string>(actualPropertyName2);
                //Assert
                unit.Properties.Count.Should().Be(2);
                unit.Properties.ElementAt(0).Name.Should().Be(actualPropertyName1);
                unit.Properties.ElementAt(1).Name.Should().Be(actualPropertyName2);
            }
        }
    }
}
