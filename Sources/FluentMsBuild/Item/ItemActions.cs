using System.Collections.Generic;

namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap an Item element adding chaining functinality
    /// </summary>
    public class ItemActions : ElementContainerActions<IItemActions, Item>, IItemActions
    {
        /// <summary>
        /// Creates a ItemActions instance for a Item element
        /// </summary>
        /// <param name="element">The Item element to create a ItemActions instance for</param>
        public ItemActions(Item element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets the Include property
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        public IItemActions WithInclude(string include)
        {
            Element.Include = include;
            return this;
        }
        //TODO: Automatically reset Include? Item cant have both.
        /// <summary>
        /// Sets the Remove property
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        public IItemActions WithRemove(string remove)
        {
            Element.Remove = remove;

            return this;
        }
        /// <summary>
        /// Sets the KeepDuplicates property
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        public IItemActions WithKeepDuplicates(string keepDuplicates)
        {
            Element.KeepDuplicates = keepDuplicates;

            return this;
        }
        /// <summary>
        /// Sets the KeepMetadata property
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        public IItemActions WithKeepMetadata(string keepMetadata)
        {
            Element.KeepMetadata = keepMetadata;

            return this;
        }
        /// <summary>
        /// Sets the RemoveMetadata property
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        public IItemActions WithRemoveMetadata(string removeMetadata)
        {
            Element.RemoveMetadata = removeMetadata;

            return this;
        }
        /// <summary>
        /// Adds Metadata
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        public IItemActions WithMetadata(IEnumerable<KeyValuePair<string, string>> metadata)
        {
            foreach (var metadataItem in metadata)
            {
                Element.AddMetadata(metadataItem.Key, metadataItem.Value);
            }

            return this;
        }
        /// <summary>
        /// Adds a Metadata element
        /// </summary>
        /// <returns>The ItemActions to allow chaining</returns>
        public IItemActions WithMetadata(string name, string unevaluatedValue)
        {
            Element.AddMetadata(name, unevaluatedValue);

            return this;
        }
        /// <summary>
        /// Adds a Metadata element
        /// </summary>
        /// <returns>The MetadataActions to allow chaining</returns>
        public IMetadataActions AddMetadata(string name, string unevaluatedValue)
        {
            return Element.AddMetadata(name, unevaluatedValue);
        }
    }
}