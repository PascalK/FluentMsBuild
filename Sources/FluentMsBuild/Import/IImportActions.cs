
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped Import element
    /// </summary>
    public interface IImportActions : IElementActions<IImportActions, Import>
    {
        /// <summary>
        /// Sets the project value of the import element
        /// </summary>
        /// <returns>The ImportActions to allow chaning</returns>
        IImportActions WithProject(string project);
    }
}