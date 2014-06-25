using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents an Item Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class Item : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "Item"; }
        }

        string exclude;
        /// <summary>
        /// Gets or sets the Exclude attribute value.
        /// </summary>
        /// <returns>Returns the Exclude attribute value. Returns an empty string if the attribute is not present.</returns>
        public string Exclude
        {
            get
            {
                return exclude ?? string.Empty;
            }
            set
            {
                exclude = value;
            }
        }
        /// <summary>
        /// Determines if this item has any child metadata elements.
        /// </summary>
        /// <returns>Returns true if this item has child metadata elements; false otherwise.</returns>
        public bool HasMetadata
        {
            get
            {
                return Metadata.Any();
            }
        }
        string include;
        /// <summary>
        /// Gets or sets the Include attribute value.
        /// </summary>
        /// <returns>Returns the Include attribute value. Returns an empty string if the attribute is not present.</returns>
        public string Include
        {
            get
            {
                return include ?? string.Empty;
            }
            set
            {
                include = value;
            }
        }
        string itemType;
        /// <summary>
        /// Gets the item element’s type.
        /// </summary>
        /// <returns>Returns the item element type.</returns>
        public string ItemType
        {
            get
            {
                return itemType ?? string.Empty;
            }
            set
            {
                itemType = value;
            }
        }
        /// <summary>
        /// Gets all child metadata.
        /// </summary>
        /// <returns>Returns all child metadata.</returns>
        public ICollection<Metadata> Metadata
        {
            get
            {
                return Children.OfType<Metadata>().ToList();
            }
        }
        string @remove;
        /// <summary>
        /// Gets or sets the Remove attribute value.
        /// </summary>
        /// <returns>Returns the Remove attribute value. Returns an empty string if the attribute is not present.</returns>
        public string Remove
        {
            get
            {
                return @remove ?? string.Empty;
            }
            set
            {
                @remove = value;
            }
        }
        string keepDuplicates;
        /// <summary>
        /// Gets or sets the KeepDuplicates value.
        /// </summary>
        /// <returns>Returns the value of KeepDuplicates, or returns empty string if it is not present.</returns>
        public string KeepDuplicates
        {
            get
            {
                return keepDuplicates ?? string.Empty;
            }
            set
            {
                keepDuplicates = value;
            }
        }
        string keepMetadata;
        /// <summary>
        /// Gets or sets the KeepMetadata attribute value.
        /// </summary>
        /// <returns>Returns the value of the KeepMetadata attribute or returns an empty string if it is not present.</returns>
        public string KeepMetadata
        {
            get
            {
                return keepMetadata ?? string.Empty;
            }
            set
            {
                keepMetadata = value;
            }
        }
        string removeMetadata;
        /// <summary>
        /// Gets or sets the RemoveMetadata attribute.
        /// </summary>
        /// <returns>Returns the RemoveMetadata value or returns an empty string if it is not present.</returns>
        public string RemoveMetadata
        {
            get
            {
                return removeMetadata ?? string.Empty;
            }
            set
            {
                removeMetadata = value;
            }
        }
        /// <summary>
        /// Location of the exclude attribute
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation ExcludeLocation { get; private set; }
        /// <summary>
        /// Location of the include attribute
        /// </summary>
        /// <returns>Returns the include location</returns>
        public ElementLocation IncludeLocation { get; private set; }
        /// <summary>
        /// Gets or sets the Location of the KeepDuplicates attribute.
        /// </summary>
        /// <returns>Returns the location of the KeepDuplicates attribute.</returns>
        public ElementLocation KeepDuplicatesLocation { get; private set; }
        /// <summary>
        /// Location of the remove attribute
        /// </summary>
        /// <returns>Returns the remove attribute’s location.</returns>
        public ElementLocation RemoveLocation { get; private set; }
        /// <summary>
        /// Gets or sets the location of the RemoveMetadata attribute.
        /// </summary>
        /// <returns>Returns the RemoveMetadata attribute’s location.</returns>
        public ElementLocation RemoveMetadataLocation { get; private set; }

        internal Item(string itemType, Root containingProject)
        {
            ItemType = itemType;
            ContainingProject = containingProject;
        }

        /// <summary>
        /// Adds metadata to this item. Appends the metadata to any existing metadata.
        /// </summary>
        /// <param name="name">Name of metadata.</param>
        /// <param name="unevaluatedValue">Metadata to be added.</param>
        /// <returns>Returns the modified metadata.</returns>
        public IMetadataActions AddMetadata(string name, string unevaluatedValue)
        {
            Metadata metadata;

            metadata = ContainingProject.CreateMetadataElement(name, unevaluatedValue);
            AppendChild(metadata);

            return new MetadataActions(metadata);
        }

        internal override void SaveValue(XmlWriter writer)
        {
            SaveAttribute(writer, "Include", Include);
            SaveAttribute(writer, "Exclude", Exclude);
            SaveAttribute(writer, "KeepDuplicates", KeepDuplicates);
            SaveAttribute(writer, "KeepMetadata", KeepMetadata);
            SaveAttribute(writer, "RemoveMetadata", RemoveMetadata);
            SaveAttribute(writer, "Remove", Remove);
            base.SaveValue(writer);
        }
        internal override void LoadAttribute(string name, string value)
        {
            switch (name)
            {
                case "Include":
                    Include = value;
                    break;
                case "Exclude":
                    Exclude = value;
                    break;
                case "KeepDuplicates":
                    KeepDuplicates = value;
                    break;
                case "KeepMetadata":
                    KeepMetadata = value;
                    break;
                case "RemoveMetadata":
                    RemoveMetadata = value;
                    break;
                case "Remove":
                    Remove = value;
                    break;
                default:
                    base.LoadAttribute(name, value);
                    break;
            }
        }
        internal override void LoadValue(XmlReader reader)
        {
            if (string.IsNullOrWhiteSpace(Include) && string.IsNullOrEmpty(Remove))
            {
                throw new InvalidOperationException(string.Format("Both Include and Remove attribute are null or empty on '{0}' item", ItemType));
                //throw new InvalidProjectFileException(Location, null, string.Format("Both Include and Remove attribute are null or empty on '{0}' item", ItemType));
            }
            base.LoadValue(reader);
        }
        /// <summary>
        /// Loads a child element from an XmlReader
        /// </summary>
        /// <param name="reader">The XmlReader to load elements from</param>
        /// <returns>The loaded element</returns>
        protected override Element LoadChildElement(XmlReader reader)
        {
            Metadata metadata;

            metadata = ContainingProject.CreateMetadataElement(reader.LocalName);
            AppendChild(metadata);

            return metadata;
        }
    }
}