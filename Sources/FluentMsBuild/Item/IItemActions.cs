using System.Collections.Generic;

namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped Item element
    /// </summary>
    public interface IItemActions : IElementContainerActions<IItemActions, Item>
    {
        /// <summary>
        /// Sets the Include property
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        IItemActions WithInclude(string include);
        /// <summary>
        /// Sets the Remove property
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        IItemActions WithRemove(string remove);
        /// <summary>
        /// Sets the KeepDuplicates property
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        IItemActions WithKeepDuplicates(string keepDuplicates);
        /// <summary>
        /// Sets the KeepMetadata property
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        IItemActions WithKeepMetadata(string keepMetadata);
        /// <summary>
        /// Sets the RemoveMetadata property
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        IItemActions WithRemoveMetadata(string removeMetadata);
        /// <summary>
        /// Adds Metadata
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        IItemActions WithMetadata(IEnumerable<KeyValuePair<string, string>> metadata);
        /// <summary>
        /// Adds a Metadata element
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        IItemActions WithMetadata(string name, string unevaluatedValue);
        /// <summary>
        /// Adds a Metadata element
        /// </summary>
        /// <returns>The MetadataActions to allow chaining</returns>
        IMetadataActions AddMetadata(string name, string unevaluatedValue);
    }
}
