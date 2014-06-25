using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Xml;

namespace FluentMsBuild
{
    [TestClass]
    public class CommentTest
    {
        Comment unit;
        Root root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = Root.Create();
            unit = root.CreateCommentElement();
        }

        [TestClass]
        public class TheValueProperty : CommentTest
        {
            [TestMethod]
            public void ShouldNeverReturnNull()
            {
                //Arrange
                unit.Value = null;
                //Act
                var actualValue = unit.Value;
                //Assert
                actualValue.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheXmlNameProperty : CommentTest
        {
            [TestMethod]
            public void ShoulBeEmpty()
            {
                //Arrange
                //Act
                var actualXmlName = typeof(Comment).GetProperty("XmlName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty).GetValue(unit).ToString();
                //Assert
                actualXmlName.Should().BeEmpty();
            }
        }
        [TestClass]
        public class TheLoadMethod : CommentTest
        {
            XmlReader reader;
            string actualXml;

            [TestMethod]
            public void ShouldLoadTextACommentValue()
            {
                string expectedComentValue;
                //Arrange
                expectedComentValue = "TestCommentValue";
                actualXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                "<!--" + expectedComentValue + "-->" +
                            "</Project>";
                reader = XmlReader.Create(new StringReader(actualXml));
                //Act
                root = Root.Create(reader);
                //Assert
                root.FirstChild.Should().BeOfType<Comment>().Which.Value.Should().Be(expectedComentValue);
            }
        }
        [TestClass]
        public class TheSaveMethod : CommentTest
        {
            [TestMethod]
            public void ShouldWriteHeValueAsAComment()
            {
                StringWriter stringWriter;
                string expectedCommentValue;
                //Arrange
                expectedCommentValue = "TestCommentValue";
                unit.Value = expectedCommentValue;
                root.AppendChild(unit);
                stringWriter = new StringWriter();
                //Act
                root.Save(stringWriter);
                //Assert
                var actualXml = stringWriter.ToString();
                actualXml.Should().Contain(XmlComment(expectedCommentValue));
            }
            string XmlComment(string commentValue)
            {
                return string.Format("<!--{0}-->", commentValue);
            }
        }
    }
}