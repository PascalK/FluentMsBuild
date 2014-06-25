using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Gets all the child properties in this project.
        /// </summary>
        /// <returns>Returns all child properties in this project, even if they are contained by Choose elements.</returns>
        public ICollection<Property> Properties
        {
            get
            {
                return AllChildren.OfType<Property>().ToList();
            }
        }
        /// <summary>
        /// Updates or adds a property to this project.
        /// </summary>
        /// <typeparam name="TPropertyValue"></typeparam>
        /// <param name="name">The name of the property to be updated or added.</param>
        /// <param name="value">The value of the property to be updated or added.</param>
        /// <returns>Returns the updated or added property.</returns>
        public IPropertyActions<TPropertyValue> AddProperty<TPropertyValue>(string name, TPropertyValue value)
        {
            PropertyGroup parentGroup = null;
            foreach (var @group in PropertyGroups)
            {
                if (string.IsNullOrEmpty(@group.Condition))
                {
                    if (parentGroup == null)
                    {
                        parentGroup = @group;
                    }
                    var property = @group.Properties.
                            Where(p =>
                                string.IsNullOrEmpty(p.Condition) &&
                                p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                            .FirstOrDefault();
                    if (property != null)
                    {
                        PropertyTypes<TPropertyValue> type;

                        type = PropertyTypes<TPropertyValue>.Get();
                        property.Value = type.ToValue(value);
                        return new PropertyActions<TPropertyValue>(property);
                    }
                }
            }
            if (parentGroup == null)
            {
                parentGroup = AddPropertyGroup().Element;
            }
            return parentGroup.AddProperty(name, value);
        }
        /// <summary>
        /// Creates a property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>Returns the property.</returns>
        public Property CreatePropertyElement(string name)
        {
            return new Property(name, this);
        }
    }
}
