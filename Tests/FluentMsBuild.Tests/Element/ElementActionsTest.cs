using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class ElementActionsTest
    {
        ElementActions<IImportActions, Import> unit;
        Import import;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            import = root.CreateImportElement(string.Empty);
            unit = new ImportActions(import);
        }
        [TestClass]
        public class TheWithConditionMethod : ElementActionsTest
        {
            [TestMethod]
            public void ShouldSetTheConditionPropertyOfTheItem()
            {
                string actualCondition;
                //Arrange
                actualCondition = "TestConditionValue";
                //Act
                unit.WithCondition(actualCondition);
                //Assert
                import.Condition.Should().Be(actualCondition);
            }
        }
        [TestClass]
        public class TheWithLabelMethod : ElementActionsTest
        {
            [TestMethod]
            public void ShouldSetTheLabelPropertyOfTheItem()
            {
                string actualLabel;
                //Arrange
                actualLabel = "TestLabelValue";
                //Act
                unit.WithLabel(actualLabel);
                //Assert
                import.Label.Should().Be(actualLabel);
            }
        }
    }
}
