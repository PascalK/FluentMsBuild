using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class ImportActionsTest
    {
        ImportActions unit;
        Import import;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            import = root.CreateImportElement(string.Empty);
            unit = new ImportActions(import);
        }
        [TestClass]
        public class TheWithProjectMethod : ImportActionsTest
        {
            [TestMethod]
            public void ShouldSetTheProjectPropertyOfTheImport()
            {
                string actualProject;
                //Arrange
                actualProject = "TestProjectValue";
                //Act
                unit.WithProject(actualProject);
                //Assert
                import.Project.Should().Be(actualProject);
            }
        }
    }
}
