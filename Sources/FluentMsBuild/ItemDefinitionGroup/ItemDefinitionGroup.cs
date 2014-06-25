using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents an ItemDefinitionGroup Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class ItemDefinitionGroup : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "ItemDefinitionGroup"; }
        }
        /// <summary>
        /// Gets all child item definitions.
        /// </summary>
        /// <returns>Returns all child item definitions.</returns>
        public ICollection<ItemDefinition> ItemDefinitions
        {
            get
            {
                return Children.OfType<ItemDefinition>().ToList();
            }
        }

        internal ItemDefinitionGroup(Root containingProject)
        {
            ContainingProject = containingProject;
        }

        /// <summary>
        /// Adds a new item definition after the last child in this item definition group.
        /// </summary>
        /// <param name="itemType">Element name of item to be added.</param>
        /// <returns>Returns the new item definition.</returns>
        public IItemDefinitionActions AddItemDefinition(string itemType)
        {
            ItemDefinition definition;

            definition = ContainingProject.CreateItemDefinitionElement(itemType);
            AppendChild(definition);

            return new ItemDefinitionActions(definition);
        }
        /// <summary>
        /// Load child elements from a XmlReader
        /// </summary>
        /// <param name="reader">The XmlReader to read child elements from</param>
        /// <returns>The loaded child element</returns>
        protected override Element LoadChildElement(XmlReader reader)
        {
            return AddItemDefinition(reader.LocalName).Element;
        }
    }
}