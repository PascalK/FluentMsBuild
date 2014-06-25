using System.Collections.Generic;

namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a ItemDefinition element adding chaining functinality
    /// </summary>
    public class ItemDefinitionActions : ElementContainerActions<IItemDefinitionActions, ItemDefinition>, IItemDefinitionActions
    {
        /// <summary>
        /// Creates a ItemDefinitionActions instance for a ItemDefinition element
        /// </summary>
        /// <param name="element">The ItemDefinition element to create a ItemDefinitionActions instance for</param>
        public ItemDefinitionActions(ItemDefinition element)
            : base(element)
        {
        }
        /// <summary>
        /// Adds metadata
        /// </summary>
        /// <param name="metadata">The metadata to add</param>
        /// <returns>The ItemDefinitionActions to allow Chaining</returns>
        public IItemDefinitionActions WithMetadata(IEnumerable<KeyValuePair<string, string>> metadata)
        {
            foreach (var metadataItem in metadata)
            {
                Element.AddMetadata(metadataItem.Key, metadataItem.Value);
            }

            return this;
        }
        /// <summary>
        /// Adds a metadata item
        /// </summary>
        /// <param name="name">The name of the metadata item to add</param>
        /// <param name="unevaluatedValue">The value of the metadata item to add</param>
        /// <returns>The ItemDefinitionActions to allow chaining</returns>
        public IItemDefinitionActions WithMetadata(string name, string unevaluatedValue)
        {
            Element.AddMetadata(name, unevaluatedValue);

            return this;
        }
        /// <summary>
        /// Adds a metadata item
        /// </summary>
        /// <param name="name">The name of the metadata item to add</param>
        /// <param name="unevaluatedValue">The value of the metadata item to add</param>
        /// <returns>The MetadataActions to allow chaining</returns>
        public IMetadataActions AddMetadata(string name, string unevaluatedValue)
        {
            return Element.AddMetadata(name, unevaluatedValue);
        }
    }
}
