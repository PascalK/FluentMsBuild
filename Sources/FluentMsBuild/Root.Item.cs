using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Gets all child items in this project.
        /// </summary>
        /// <returns>Returns all child items in this project, even if they are contained by Choose elements.</returns>
        public ICollection<Item> Items
        {
            get
            {
                return AllChildren.OfType<Item>().ToList();
            }
        }
        /// <summary>
        /// Adds an item to this project.
        /// </summary>
        /// <param name="itemType">The item type of the item to be added.</param>
        /// <param name="include">The Include value of the item to be added.</param>
        /// <returns>Returns the added item.</returns>
        public IItemActions AddItem(string itemType, string include)
        {
            return AddItem(itemType, include, null);
        }
        /// <summary>
        /// Adds an item with metadata to this project.
        /// </summary>
        /// <param name="itemType">The item type of the item to be added.</param>
        /// <param name="include">The Include value of the item to be added.</param>
        /// <param name="metadata">The metadata to be added.</param>
        /// <returns>Returns the added item.</returns>
        public IItemActions AddItem(string itemType, string include, IEnumerable<KeyValuePair<string, string>> metadata)
        {
            var @group = ItemGroups.
                    Where(itemGroup =>
                        string.IsNullOrEmpty(itemGroup.Condition) &&
                        itemGroup.Items.Any(s => s.ItemType.Equals(itemType, StringComparison.OrdinalIgnoreCase)))
                    .FirstOrDefault();
            if (@group == null)
            {
                @group = AddItemGroup().Element;
            }
            return @group.AddItem(itemType, include, metadata);
        }
        /// <summary>
        /// Creates an item.
        /// </summary>
        /// <param name="itemType">The item type of the item.</param>
        /// <returns>Returns the item.</returns>
        public Item CreateItemElement(string itemType)
        {
            return new Item(itemType, this);
        }
        /// <summary>
        /// Creates an item with the specifed Include value.
        /// </summary>
        /// <param name="itemType">The item type of the item.</param>
        /// <param name="include">The Include value of the item.</param>
        /// <returns>Returns the item.</returns>
        public Item CreateItemElement(string itemType, string include)
        {
            var item = CreateItemElement(itemType);
            item.Include = include;
            return item;
        }
    }
}
