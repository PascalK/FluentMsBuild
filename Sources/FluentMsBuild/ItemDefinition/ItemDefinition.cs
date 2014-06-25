using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents an ItemDefinition element in an MSBuild project.
    /// </summary>
    public class ItemDefinition : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "ItemDefinition"; }
        }
        /// <summary>
        /// Gets the item definition element name.
        /// </summary>
        /// <returns>Returns the item definition element name.</returns>
        public string ItemType { get; private set; }
        /// <summary>
        /// Gets all child metadata definitions.
        /// </summary>
        /// <returns>Returns all child metadata definitions.</returns>
        public ICollection<Metadata> Metadata
        {
            get
            {
                return Children.OfType<Metadata>().ToList();
            }
        }

        internal ItemDefinition(string itemType, Root containingProject)
        {
            ItemType = itemType;
            ContainingProject = containingProject;
        }

        /// <summary>
        /// Adds metadata to this item definition. Appends the metadata to any existing metadata.
        /// </summary>
        /// <param name="name">Name of the metadata.</param>
        /// <param name="unevaluatedValue">Metadata to be added.</param>
        /// <returns>Returns the modified metadata.</returns>
        public IMetadataActions AddMetadata(string name, string unevaluatedValue)
        {
            Metadata metadata;

            metadata = ContainingProject.CreateMetadataElement(name);
            metadata.Value = unevaluatedValue;
            AppendChild(metadata);

            return new MetadataActions(metadata);
        }
        /// <summary>
        /// Loads any child element of the ItemDefinition
        /// </summary>
        /// <param name="reader">The XmlReader to load chil element from</param>
        /// <returns>The loaded child Element</returns>
        protected override Element LoadChildElement(XmlReader reader)
        {
            return AddMetadata(reader.LocalName, null).Element;
        }
    }
}