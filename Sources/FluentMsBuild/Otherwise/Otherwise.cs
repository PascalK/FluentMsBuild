using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents an Otherwise Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class Otherwise : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "Otherwise"; }
        }
        /// <summary>
        /// Gets all child Choose elements.
        /// </summary>
        /// <returns>Returns all child Choose elements.</returns>
        public ICollection<Choose> ChooseElements
        {
            get
            {
                return Children.OfType<Choose>().ToList();
            }
        }
        /// <summary>
        /// Gets a nonexistent condition, which is implicitly true.
        /// </summary>
        /// <remarks>Trying to set this property wil result in an exception</remarks>
        /// <returns>Returns a null.</returns>
        public override string Condition
        {
            get { return null; }
            set
            {
                throw new InvalidOperationException("Can not set Condition.");
            }
        }
        /// <summary>
        /// Gets all child item groups.
        /// </summary>
        /// <returns>Returns all child item groups.</returns>
        public ICollection<ItemGroup> ItemGroups
        {
            get
            {
                return Children.OfType<ItemGroup>().ToList();
            }
        }
        /// <summary>
        /// Gets all child property groups.
        /// </summary>
        /// <returns>Returns all child property groups.</returns>
        public ICollection<PropertyGroup> PropertyGroups
        {
            get
            {
                return Children.OfType<PropertyGroup>().ToList();
            }
        }

        internal Otherwise(Root containingProject)
        {
            ContainingProject = containingProject;
        }

        /// <summary>
        /// Loads a child element from an XmlReader
        /// </summary>
        /// <param name="reader">The XmlReader to load elements from</param>
        /// <returns>The loaded element</returns>
        protected override Element LoadChildElement(XmlReader reader)
        {
            switch (reader.LocalName)
            {
                case "PropertyGroup":
                    var property = ContainingProject.CreatePropertyGroupElement();
                    AppendChild(property);
                    return property;
                case "ItemGroup":
                    var item = ContainingProject.CreateItemGroupElement();
                    AppendChild(item);
                    return item;
                case "Choose":
                    var choose = ContainingProject.CreateChooseElement();
                    AppendChild(choose);
                    return choose;
                default:
                    throw new InvalidProjectFileException(string.Format(
                            "Child \"{0}\" is not a known node type.", reader.LocalName));
            }
        }
    }
}