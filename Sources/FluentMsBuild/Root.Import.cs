using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Gets all the child import elements in this project.
        /// </summary>
        /// <returns>Gets all the child import elements in this project.</returns>
        public ICollection<Import> Imports
        {
            get
            {
                return AllChildren.OfType<Import>().ToList();
            }
        }
        /// <summary>
        /// Adds a project import to this project.
        /// </summary>
        /// <param name="project">The project to be imported.</param>
        /// <returns>Returns the added project import.</returns>
        public IImportActions AddImport(string project)
        {
            Import import;

            import = CreateImportElement(project);
            AppendChild(import);

            return new ImportActions(import);
        }
        /// <summary>
        /// Creates an Import Element (MSBuild).
        /// </summary>
        /// <param name="project">The project to be imported.</param>
        /// <returns>Returns the created Import element.</returns>
        public Import CreateImportElement(string project)
        {
            return new Import(project, this);
        }
    }
}