using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents an ItemGroup Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class ItemGroup : ElementContainer//<ProjectItemGroupElement>
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "ItemGroup"; }
        }
        /// <summary>
        /// Gets all child items.
        /// </summary>
        /// <returns>Returns all child items.</returns>
        public ICollection<Item> Items
        {
            get
            {
                return Children.OfType<Item>().ToList();
            }
        }

        internal ItemGroup(Root containingProject)
        {
            ContainingProject = containingProject;
        }

        /// <summary>
        /// Adds a new item to the item group. Items are ordered first by item element name, and then by the Include attribute.
        /// </summary>
        /// <param name="itemType">Item element name of the item to be added.</param>
        /// <param name="include">Include attribute of the item to be added.</param>
        /// <returns>Returns the new item element.</returns>
        public IItemActions AddItem(string itemType, string include)
        {
            return AddItem(itemType, include, null);
        }

        /// <summary>
        /// Adds a new item with metadata to the item group. Items are ordered first by item element name, and then by the Include attribute.
        /// </summary>
        /// <param name="itemType">Item element name of the item to be added.</param>
        /// <param name="include">Include attribute of the item to be added.</param>
        /// <param name="metadata">Metadata to be added.</param>
        /// <returns>Returns the new item element.</returns>
        public IItemActions AddItem(string itemType, string include, IEnumerable<KeyValuePair<string, string>> metadata)
        {
            Item item;
            Element lastChild;

            item = ContainingProject.CreateItemElement(itemType, include);
            if (metadata != null)
                foreach (var data in metadata)
                    item.AddMetadata(data.Key, data.Value);

            lastChild = LastChild;
            foreach (var existingItem in Items)
            {
                int compare;
                compare = string.Compare(item.ItemType, existingItem.ItemType, StringComparison.OrdinalIgnoreCase);

                if (compare == 0)
                {
                    if (string.Compare(item.Include, existingItem.Include, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        continue;
                    }
                    lastChild = existingItem.PreviousSibling;
                    break;
                }
                if (compare < 0)
                {
                    lastChild = existingItem.PreviousSibling;
                    break;
                }
            }
            InsertAfterChild(item, lastChild);

            return new ItemActions(item);
        }
        /// <summary>
        /// Loads child elements form a XmlReader
        /// </summary>
        /// <param name="reader">The XmlReader to load child elements from</param>
        /// <returns>The loaded child element</returns>
        protected override Element LoadChildElement(XmlReader reader)
        {
            Item createdItem;

            createdItem = ContainingProject.CreateItemElement(reader.LocalName);
            AppendChild(createdItem);

            return createdItem;
        }
    }
}
