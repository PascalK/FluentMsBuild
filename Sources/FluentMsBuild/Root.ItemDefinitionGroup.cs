using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Gets all the child item definition groups in this project.
        /// </summary>
        /// <returns>Returns all the child item definition groups in this project.</returns>
        public ICollection<ItemDefinitionGroup> ItemDefinitionGroups
        {
            get
            {
                return Children.OfType<ItemDefinitionGroup>().ToList();
            }
        }
        /// <summary>
        /// Gets all the child item definition groups in this project, starting with the last group.
        /// </summary>
        /// <returns>Returns all the child item definition groups in this project, starting with the last group.</returns>
        public ICollection<ItemDefinitionGroup> ItemDefinitionGroupsReversed
        {
            get
            {
                return ChildrenReversed.OfType<ItemDefinitionGroup>().ToList();
            }
        }
        /// <summary>
        /// Adds an item definition group to this project.
        /// </summary>
        /// <returns>Returns the added item definition group.</returns>
        public IItemDefinitionGroupActions AddItemDefinitionGroup()
        {
            ItemDefinitionGroup @group;
            ElementContainer last;

            @group = CreateItemDefinitionGroupElement();
            last = ItemDefinitionGroupsReversed.FirstOrDefault();
            if (last == null)
            {
                last = PropertyGroupsReversed.FirstOrDefault();
            }
            InsertAfterChild(@group, last);
            return new ItemDefinitionGroupActions(@group);
        }
        /// <summary>
        /// Creates an item definition group.
        /// </summary>
        /// <returns>Returns the item definition group.</returns>
        public ItemDefinitionGroup CreateItemDefinitionGroupElement()
        {
            return new ItemDefinitionGroup(this);
        }
    }
}