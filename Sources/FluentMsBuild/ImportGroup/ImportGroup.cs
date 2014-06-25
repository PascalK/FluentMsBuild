using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents the ImportGroup element in an MSBuild project.
    /// </summary>
    public class ImportGroup : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get
            {
                return "ImportGroup";
            }
        }
        /// <summary>
        /// Gets all properties in this project import.
        /// </summary>
        /// <returns>Returns all properties in this project import.</returns>
        public ICollection<Import> Imports
        {
            get
            {
                return Children.OfType<Import>().ToList();
            }
        }

        internal ImportGroup(Root containingProject)
        {
            ContainingProject = containingProject;
        }
        /// <summary>
        /// Adds a new import after the last import in this import group.
        /// </summary>
        /// <param name="project">The project to import.</param>
        /// <returns>Returns the new import.</returns>
        public IImportActions AddImport(string project)
        {
            Import import;

            import = ContainingProject.CreateImportElement(project);
            AppendChild(import);

            return new ImportActions(import);
        }
        /// <summary>
        /// Loads a child element from an XmlReader
        /// </summary>
        /// <param name="reader">The XmlReader to load elements from</param>
        /// <returns>The loaded element</returns>
        protected override Element LoadChildElement(XmlReader reader)
        {
            Element loadedELement;
            string name;

            name = reader.LocalName;
            switch (name)
            {
                case "Import":
                    loadedELement = AddImport(null).Element;

                    break;
                default:
                    throw new InvalidProjectFileException(
                        string.Format("Child \"{0}\" is not a known node type.", name));
            }

            return loadedELement;
        }
    }
}