
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap an Comment element adding chaining functinality
    /// </summary>
    public interface ICommentActions : IElementActions<ICommentActions, Comment>
    {
        /// <summary>
        /// Sets the value of the Comment element
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The Comment element with the set value</returns>
        ICommentActions WithValue(string value);
    }
}
