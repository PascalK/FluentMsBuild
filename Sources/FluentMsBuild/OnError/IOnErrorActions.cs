
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped OnError element
    /// </summary>
    public interface IOnErrorActions : IElementActions<IOnErrorActions, OnError>
    {
        /// <summary>
        /// Sets the ExecuteTargetsAttribute property
        /// </summary>
        /// <param name="executeTargetsAttribute">The executeTargetsAttribute to set</param>
        /// <returns>The OnErrorActions to allow chaining</returns>
        IOnErrorActions WithExecuteTargetsAttribute(string executeTargetsAttribute);
    }
}
