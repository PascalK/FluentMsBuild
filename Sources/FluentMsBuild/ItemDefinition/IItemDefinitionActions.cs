using System.Collections.Generic;

namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped ItemDefinition element
    /// </summary>
    public interface IItemDefinitionActions : IElementContainerActions<IItemDefinitionActions, ItemDefinition>
    {
        /// <summary>
        /// Adds metadata
        /// </summary>
        /// <param name="metadata">The metadata to add</param>
        /// <returns>The ItemDefinitionActions to allow Chaining</returns>
        IItemDefinitionActions WithMetadata(IEnumerable<KeyValuePair<string, string>> metadata);
        /// <summary>
        /// Adds a metadata item
        /// </summary>
        /// <param name="name">The name of the metadata item to add</param>
        /// <param name="unevaluatedValue">The value of the metadata item to add</param>
        /// <returns>The ItemDefinitionActions to allow chaining</returns>
        IItemDefinitionActions WithMetadata(string name, string unevaluatedValue);
        /// <summary>
        /// Adds a metadata item
        /// </summary>
        /// <param name="name">The name of the metadata item to add</param>
        /// <param name="unevaluatedValue">The value of the metadata item to add</param>
        /// <returns>The MetadataActions to allow chaining</returns>
        IMetadataActions AddMetadata(string name, string unevaluatedValue);
    }
}