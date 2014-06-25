using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Gets all the child import groups in this project.
        /// </summary>
        /// <returns>Returns all the child import groups in this project.</returns>
        public ICollection<ImportGroup> ImportGroups
        {
            get
            {
                return Children.OfType<ImportGroup>().ToList();
            }
        }
        /// <summary>
        /// Gets all the child import groups in this project, starting with the last group.
        /// </summary>
        /// <returns>Returns all the child import groups in this project, starting with the last group.</returns>
        public ICollection<ImportGroup> ImportGroupsReversed
        {
            get
            {
                return ChildrenReversed.OfType<ImportGroup>().ToList();
            }
        }
        /// <summary>
        /// Creates an import group at the end of this project.
        /// </summary>
        /// <returns>Returns the import group created at the end of this project.</returns>
        public IImportGroupActions AddImportGroup()
        {
            ImportGroup importGroup;

            importGroup = CreateImportGroupElement();
            AppendChild(importGroup);

            return new ImportGroupActions(importGroup);
        }
        /// <summary>
        /// Creates an import group.
        /// </summary>
        /// <returns>Returns the import group.</returns>
        public ImportGroup CreateImportGroupElement()
        {
            return new ImportGroup(this);
        }
    }
}
