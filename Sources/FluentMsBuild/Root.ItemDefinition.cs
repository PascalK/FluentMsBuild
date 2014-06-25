using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Gets all child item definitions in all item definition groups anywhere in this project.
        /// </summary>
        /// <returns>Gets all child item definitions in all item definition groups anywhere in this project.</returns>
        public ICollection<ItemDefinition> ItemDefinitions
        {
            get
            {
                return AllChildren.OfType<ItemDefinition>().ToList();
            }
        }
        /// <summary>
        /// Adds an item definition to this project.
        /// </summary>
        /// <param name="itemType">The item type of the item definition to be added.</param>
        /// <returns>Returns the added item definition.</returns>
        public IItemDefinitionActions AddItemDefinition(string itemType)
        {
            var @group = ItemDefinitionGroups.
                    Where(itemDefinitionGroup =>
                        string.IsNullOrEmpty(itemDefinitionGroup.Condition) &&
                        itemDefinitionGroup.ItemDefinitions.Any(s => s.ItemType.Equals(itemType, StringComparison.OrdinalIgnoreCase)))
                    .FirstOrDefault();
            if (@group == null)
            {
                @group = AddItemDefinitionGroup().Element;
            }
            return @group.AddItemDefinition(itemType);
        }
        /// <summary>
        /// Creates an item definition.
        /// </summary>
        /// <param name="itemType">The item type of the item definition.</param>
        /// <returns>Returns the item definition.</returns>
        public ItemDefinition CreateItemDefinitionElement(string itemType)
        {
            return new ItemDefinition(itemType, this);
        }
    }
}