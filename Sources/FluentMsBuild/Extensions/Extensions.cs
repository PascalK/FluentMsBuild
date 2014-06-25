using System;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents the ProjectExtensions Element (MSBuild) in an MSBuild project. Project extensions can contain arbitrary XML content.
    /// </summary>
    public class Extensions : Element
    {
        XmlDocument document;
        XmlElement element;

        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "ProjectExtensions"; }
        }
        /// <summary>
        /// Gets a nonexistent condition, which is implicitly true.
        /// </summary>
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
        /// Gets or sets the arbitrary XML content of this project extension.
        /// </summary>
        /// <returns>Returns the arbitrary XML content of this project extension.</returns>
        public string Content
        {
            get { return element.InnerXml; }
            set { element.InnerXml = value; }
        }
        /// <summary>
        /// Gets or sets the content of the first sub-element with the given name parameter.
        /// </summary>
        /// <param name="name">The name of the sub-element.</param>
        /// <returns>Returns the contents of the element.</returns>
        public string this[string name]
        {
            get
            {
                var child = element[name];
                return child == null ? string.Empty : child.InnerXml;
            }
            set
            {
                var child = element[name];
                if (child == null)
                {
                    if (string.IsNullOrEmpty(name))
                        return;
                    child = document.CreateElement(name);
                    element.AppendChild(child);
                }
                if (string.IsNullOrEmpty(value))
                    element.RemoveChild(child);
                else
                    child.InnerXml = value;
            }
        }

        internal Extensions(Root containingProject)
        {
            ContainingProject = containingProject;
            document = new XmlDocument();
            element = document.CreateElement(XmlName);
        }

        internal override void Load(XmlReader reader)
        {
            while (reader.Read() && reader.NodeType != XmlNodeType.Element)
                ;
            FillLocation(reader);
            using (XmlReader subReader = reader.ReadSubtree())
            {
                document = new XmlDocument();
                document.Load(subReader);
                element = document.DocumentElement;
            }
        }
        internal override void SaveValue(XmlWriter writer)
        {
            element.WriteContentTo(writer);
        }
    }
}