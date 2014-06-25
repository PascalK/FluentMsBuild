using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Gets all the child property groups in this project.
        /// </summary>
        /// <returns>Returns all the child property groups in this project.</returns>
        public ICollection<PropertyGroup> PropertyGroups
        {
            get
            {
                return Children.OfType<PropertyGroup>().ToList();
            }
        }
        /// <summary>
        /// Gets all the child property groups present in this project, starting with the last group.
        /// </summary>
        /// <returns>Returns all the child property groups present in this project, starting with the last group.</returns>
        public ICollection<PropertyGroup> PropertyGroupsReversed
        {
            get
            {
                return ChildrenReversed.OfType<PropertyGroup>().ToList();
            }
        }
        /// <summary>
        /// Adds a new property group to this project.
        /// </summary>
        /// <returns>Returns the added property group.</returns>
        public IPropertyGroupActions AddPropertyGroup()
        {
            PropertyGroup @group;
            PropertyGroup last;

            @group = CreatePropertyGroupElement();
            last = PropertyGroupsReversed.FirstOrDefault();
            InsertAfterChild(@group, last);

            return new PropertyGroupActions(@group);
        }
        /// <summary>
        /// Creates a property group.
        /// </summary>
        /// <returns>Returns the property group.</returns>
        public PropertyGroup CreatePropertyGroupElement()
        {
            return new PropertyGroup(this);
        }
    }
}