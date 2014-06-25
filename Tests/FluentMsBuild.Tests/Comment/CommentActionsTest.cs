using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class CommentActionsTest
    {
        CommentActions unit;
        Comment comment;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            comment = root.CreateCommentElement();
            unit = new CommentActions(comment);
        }

        [TestClass]
        public class TheWithValueMethod : CommentActionsTest
        {
            [TestMethod]
            public void ShouldSetTheCommentsValue()
            {
                string expectedValue;
                //Arrange
                expectedValue = "TestCommentValue";
                //Act
                unit.WithValue(expectedValue);
                //Assert
                comment.Value.Should().Be(expectedValue);
            }
        }
    }
}
