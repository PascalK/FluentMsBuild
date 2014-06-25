using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class ExtensionsActionsTest
    {
        ExtensionsActions unit;
        Extensions extensions;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            extensions = root.CreateProjectExtensionsElement();
            unit = new ExtensionsActions(extensions);
        }

        [TestClass]
        public class TheWithContentMethod : ExtensionsActionsTest
        {
            [TestMethod]
            public void ShouldSetTheExtensionssContent()
            {
                string expectedContent;
                //Arrange
                expectedContent = "TestExtensionsValue";
                //Act
                unit.WithContent(expectedContent);
                //Assert
                extensions.Content.Should().Be(expectedContent);
            }
        }
    }
}
