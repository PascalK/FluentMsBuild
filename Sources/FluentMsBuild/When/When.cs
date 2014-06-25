using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents a When Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class When : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "When"; }
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
        /// Gets all child Choose Element (MSBuild).
        /// </summary>
        /// <returns>Returns all child Choose elements.</returns>
        public ICollection<Choose> ChooseElements
        {
            get
            {
                return Children.OfType<Choose>().ToList();
            }
        }

        internal When(string condition, Root containingProject)
        {
            Condition = condition;
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
