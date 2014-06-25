
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped Extensions element
    /// </summary>
    public class ExtensionsActions : ElementActions<IExtensionsActions, Extensions>, IExtensionsActions
    {
        /// <summary>
        /// Creates a ExtensionsActions instance for a Extensions element
        /// </summary>
        /// <param name="element">The Extensions element to create a ExtensionsActions instance for</param>
        public ExtensionsActions(Extensions element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets content for the Extensions element
        /// </summary>
        /// <param name="content">The content to set</param>
        /// <returns>The ExtensionsActions to allow chaining</returns>
        public IExtensionsActions WithContent(string content)
        {
            Element.Content = content;

            return this;
        }
    }
}
