using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents a PropertyGroup element in an MSBuild project.
    /// </summary>
    public class PropertyGroup : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "PropertyGroup"; }
        }

        /// <summary>
        /// Gets all child properties.
        /// </summary>
        /// <returns>Returns all child properties.</returns>
        public ICollection<Property> Properties
        {
            get
            {
                return Children.OfType<Property>().ToList();
            }
        }
        /// <summary>
        /// Gets all child properties, starting with the last child.
        /// </summary>
        /// <returns>Returns all child properties, starting with the last child.</returns>
        public ICollection<Property> PropertiesReversed
        {
            get
            {
                return ChildrenReversed.OfType<Property>().ToList();
            }
        }

        internal PropertyGroup(Root containingProject)
        {
            ContainingProject = containingProject;
        }
        /// <summary>
        /// Appends a new property to the property group.
        /// </summary>
        /// <typeparam name="TPropertyValue">The type of the property to add</typeparam>
        /// <param name="name">The property name.</param>
        /// <param name="unevaluatedValue">The property value.</param>
        /// <returns>Returns the new property.</returns>
        public IPropertyActions<TPropertyValue> AddProperty<TPropertyValue>(string name, TPropertyValue unevaluatedValue)
        {
            Property property;
            PropertyTypes<TPropertyValue> type;

            type = PropertyTypes<TPropertyValue>.Get();

            property = ContainingProject.CreatePropertyElement(name);
            property.Value = type.ToValue(unevaluatedValue);
            AppendChild(property);

            return new PropertyActions<TPropertyValue>(property);
        }
        /// <summary>
        /// Updates the value of the given property in the property group.
        /// </summary>
        /// <typeparam name="TPropertyValue">The type of the property to add</typeparam>
        /// <param name="name">The name of the property to be updated.</param>
        /// <param name="unevaluatedValue">The new property value.</param>
        /// <returns>Returns the updated property.</returns>
        public IPropertyActions<TPropertyValue> SetProperty<TPropertyValue>(string name, TPropertyValue unevaluatedValue)
        {
            IPropertyActions<TPropertyValue> propertyActions;
            Property existing;

            existing = Properties.FirstOrDefault(p =>
                p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                string.IsNullOrEmpty(p.Condition));
            if (existing != null)
            {
                PropertyTypes<TPropertyValue> type;

                type = PropertyTypes<TPropertyValue>.Get();
                existing.Value = type.ToValue(unevaluatedValue);

                propertyActions = new PropertyActions<TPropertyValue>(existing);
            }
            else
            {
                propertyActions = AddProperty(name, unevaluatedValue);
            }

            return propertyActions;
        }

        /// <summary>
        /// Load a child element from an XmlReader
        /// </summary>
        /// <param name="reader">The XmlReader to load a child element from</param>
        /// <returns>The loaded child element</returns>
        protected override Element LoadChildElement(XmlReader reader)
        {
            switch (reader.LocalName)
            {
                case "ItemGroup":
                case "PropertyGroup":
                    throw CreateError(reader, string.Format("{0} is a reserved name that cannot be used for a property.", reader.LocalName));
                // others need to be checked too, but things like "Project" are somehow allowed...
            }
            return AddProperty(reader.LocalName, (string)null).Element;
        }
    }
}