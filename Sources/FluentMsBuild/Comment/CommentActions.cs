
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap an Comment element adding chaining functinality
    /// </summary>
    public class CommentActions : ElementActions<ICommentActions, Comment>, ICommentActions
    {
        /// <summary>
        /// Creates a CommentActions instance for a Comment element
        /// </summary>
        /// <param name="element">The Comment element to create a CommentActions instance for</param>
        public CommentActions(Comment element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets the value of the Comment element
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The Comment element with the set value</returns>
        public ICommentActions WithValue(string value)
        {
            Element.Value = value;

            return this;
        }
    }
}