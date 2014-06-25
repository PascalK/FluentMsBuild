
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped ImportGroup element
    /// </summary>
    public class ImportGroupActions : ElementActions<IImportGroupActions, ImportGroup>, IImportGroupActions
    {
        /// <summary>
        /// Creates a ImportGroupActions instance for a ImportGroup element
        /// </summary>
        /// <param name="element">The ImportGroup element to create a ImportGroupActions instance for</param>
        public ImportGroupActions(ImportGroup element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets the project of the ImportGroup
        /// </summary>
        /// <returns>The ImportActions to allow chaining</returns>
        public IImportActions AddImport(string project)
        {
            return Element.AddImport(project);
        }
    }
}
