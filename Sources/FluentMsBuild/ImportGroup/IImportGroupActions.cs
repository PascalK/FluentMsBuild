
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped ImportGroup element
    /// </summary>
    public interface IImportGroupActions : IElementContainerActions<IImportGroupActions, ImportGroup>
    {
        /// <summary>
        /// Sets the project of the ImportGroup
        /// </summary>
        /// <returns>The ImportActions to allow chaining</returns>
        IImportActions AddImport(string project);
    }
}