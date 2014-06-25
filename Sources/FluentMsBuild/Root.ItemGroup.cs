using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Gets all the child item groups in this project.
        /// </summary>
        /// <returns>Returns all the child item groups in this project.</returns>
        public ICollection<ItemGroup> ItemGroups
        {
            get
            {
                return AllChildren.OfType<ItemGroup>().ToList();
            }
        }
        /// <summary>
        /// Gets all the child item groups present in this project, starting with the last group.
        /// </summary>
        /// <returns>Returns all the child item groups present in this project, starting with the last group.</returns>
        public ICollection<ItemGroup> ItemGroupsReversed
        {
            get
            {
                return AllChildren.OfType<ItemGroup>().ToList();
            }
        }
        /// <summary>
        /// Creates and adds an item group to this project.
        /// </summary>
        /// <returns>Returns the added item group.</returns>
        public IItemGroupActions AddItemGroup()
        {
            ItemGroup @group;
            ElementContainer last;

            @group = CreateItemGroupElement();
            last = ItemGroupsReversed.FirstOrDefault();
            if (last == null)
            {
                last = PropertyGroupsReversed.FirstOrDefault();
            }
            InsertAfterChild(@group, last);

            return new ItemGroupActions(@group);
        }
        /// <summary>
        /// Creates an item group.
        /// </summary>
        /// <returns>Returns the item group.</returns>
        public ItemGroup CreateItemGroupElement()
        {
            return new ItemGroup(this);
        }
        /// <summary>
        /// Creates or returns an ItemGroup with the provided label
        /// </summary>
        /// <param name="label">The label of the ItemGroup that will be ensured</param>
        /// <returns>The ItemGroup with the corresponding label</returns>
        public IItemGroupActions EnsureItemGroup(string label)
        {
            IItemGroupActions itemGroupActions;
            ItemGroup itemGroup;

            itemGroup = ItemGroups.FirstOrDefault(ig => ig.Label == label);
            if (itemGroup != null)
            {
                itemGroupActions = new ItemGroupActions(itemGroup);
            }
            else
            {
                itemGroupActions = AddItemGroup().WithLabel(label);
            }

            return itemGroupActions;
        }
    }
}
