
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped Extensions element
    /// </summary>
    public interface IExtensionsActions : IElementActions<IExtensionsActions, Extensions>
    {
        /// <summary>
        /// Sets content for the Extensions element
        /// </summary>
        /// <param name="content">The content to set</param>
        /// <returns>The ExtensionsActions to allow chaining</returns>
        IExtensionsActions WithContent(string content);
    }
}
