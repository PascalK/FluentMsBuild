
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap an Import element adding chaining functinality
    /// </summary>
    public class ImportActions : ElementActions<IImportActions, Import>, IImportActions
    {
        /// <summary>
        /// Creates a ImportActions instance for an Import element
        /// </summary>
        /// <param name="element">The Import element to create a ImportActions instance for</param>
        public ImportActions(Import element)
            : base(element)
        {
        }
        /// <summary>
        /// Sets the project value of the import element
        /// </summary>
        /// <returns>The ImportActions to allow chaning</returns>
        public IImportActions WithProject(string project)
        {
            Element.Project = project;

            return this;
        }
    }
}