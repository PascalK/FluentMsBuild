
namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Creates a Comment Element (MSBuild).
        /// </summary>
        /// <returns>A Comment class.</returns>
        public Comment CreateCommentElement()
        {
            return new Comment(this);
        }
    }
}
