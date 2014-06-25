using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FluentMsBuild
{
    [TestClass]
    public class ImportGroupActionsTest
    {
        ImportGroupActions unit;
        ImportGroup importGroup;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            importGroup = root.CreateImportGroupElement();
            unit = new ImportGroupActions(importGroup);
        }

        [TestClass]
        public class TheAddImportMethod : ImportGroupActionsTest
        {
            [TestMethod]
            public void Should()
            {
                string expectedProject;
                //Arrange
                expectedProject = "TestProjectValue";
                //Act
                var actualResult = unit.AddImport(expectedProject).Element;
                //Assert
                importGroup.Imports.Single().Should().Be(actualResult);
            }
        }
    }
}
