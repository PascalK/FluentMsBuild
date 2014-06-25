using System;
using System.Collections.Generic;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Abstract base class for MSBuild construction object model elements.
    /// </summary>
    public abstract class Element
    {
        internal const string MSBuildNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected abstract string XmlName { get; }
        /// <summary>
        /// ProjectRootElement (possibly imported) that contains this Xml. Cannot be null.
        /// </summary>
        /// <returns>Returns the project root container that contains this project.</returns>
        public Root ContainingProject { get; internal set; }
        /// <summary>
        /// Previous sibling element. May be null.
        /// </summary>
        /// <returns>Returns the previous sibling of this project element. Returns null if no previous sibling exists.</returns>
        public Element PreviousSibling
        {
            get { return LinkedListNode.Previous == null ? null : LinkedListNode.Previous.Value; }
            //internal set { }
        }
        /// <summary>
        /// Null if this is a ProjectRootElement. Null if this has not been attached to a parent yet.
        /// </summary>
        /// <returns>Returns the project element container that contains this project.</returns>
        public ElementContainer Parent { get; internal set; }
        /// <summary>
        /// Next sibling element. May be null.
        /// </summary>
        /// <returns>Returns the next sibling of this project element. Returns null if no next sibling exists.</returns>
        public Element NextSibling
        {
            get
            {
                return LinkedListNode.Next == null ? null : LinkedListNode.Next.Value;
            }
            //internal set { }
        }
        string label;
        /// <summary>
        /// Gets or sets the Label value. Returns empty string if it is not present. Removes the attribute if the value to set is empty.
        /// </summary>
        /// <returns>Returns the label. Returns an empty string if no label is present.</returns>
        public string Label
        {
            get
            {
                return label ?? string.Empty;
            }
            set
            {
                label = value;
            }
        }
        string condition;
        /// <summary>
        /// Gets or sets the Condition value. It will return empty string IFF a condition attribute is legal but it’s not present or has no value. It will return null IFF a Condition attribute is illegal on that element. Removes the attribute if the value to set is empty. It is possible for derived classes to throw an System.InvalidOperationException if setting the condition is not applicable for those elements.
        /// </summary>
        /// <returns>Returns the Condition attribute value. Returns an empty string if the attribute is not present.</returns>
        public virtual string Condition
        {
            get
            {
                return condition ?? string.Empty;
            }
            set
            {
                condition = value;
            }
        }
        /// <summary>
        /// All parent elements of this element, going up to the ProjectRootElement. None if this is a ProjectRootElement. None if this has not been attached to a parent yet.
        /// </summary>
        /// <returns>Returns an enumerator over all parent elements. There are no parents elements if the project element is a ProjectRootElement or if this is not yet attached to a parent element.</returns>
        public IEnumerable<ElementContainer> AllParents
        {
            get
            {
                var parent = Parent;
                while (parent != null)
                {
                    yield return parent;
                    parent = parent.Parent;
                }
            }
        }
        internal LinkedListNode<Element> LinkedListNode { get; private set; }
        /// <summary>
        /// Location of the corresponding Xml element. May not be correct if file is not saved, or file has been edited since it was last saved. In the case of an unsaved edit, the location only contains the path to the file that the element originates from.
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation Location { get; private set; }
        /// <summary>
        /// Location of the "Label" attribute on this element, if any. If there is no such attribute, returns null;
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation LabelLocation { get; private set; }
        /// <summary>
        /// Location of the "Condition" attribute on this element, if any. If there is no such attribute, returns null.
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation ConditionLocation { get; private set; }

        internal Element()
        {
            LinkedListNode = new LinkedListNode<Element>(this);
        }

        internal virtual void Load(XmlReader reader)
        {
            reader.ReadToFollowing(XmlName);
            FillLocation(reader);
            while (reader.MoveToNextAttribute())
            {
                LoadAttribute(reader.Name, reader.Value);
            }
            reader.MoveToElement();
            LoadValue(reader);
        }
        internal virtual void LoadAttribute(string name, string value)
        {
            switch (name)
            {
                case "xmlns":
                    break;
                case "Label":
                    Label = value;
                    break;
                case "Condition":
                    Condition = value;
                    break;
                default:
                    throw new InvalidOperationException(string.Format(
                            "Attribute \"{0}\" is not known on node \"{1}\" [type {2}].", name, XmlName,
                            GetType()));
                //throw new InvalidProjectFileException(Location, null, string.Format(
                //        "Attribute \"{0}\" is not known on node \"{1}\" [type {2}].", name, XmlName,
                //        GetType()));
            }
        }
        internal virtual void LoadValue(XmlReader reader)
        {
        }
        internal virtual void Save(XmlWriter writer)
        {
            writer.WriteStartElement(XmlName);
            SaveValue(writer);
            writer.WriteEndElement();
        }
        internal virtual void SaveValue(XmlWriter writer)
        {
            SaveAttribute(writer, "Label", Label);
            SaveAttribute(writer, "Condition", Condition);
        }
        internal void SaveAttribute(XmlWriter writer, string attributeName, string attributeValue)
        {
            if (!string.IsNullOrWhiteSpace(attributeValue))
            {
                writer.WriteAttributeString(attributeName, attributeValue);
            }
        }

        string GetFilePath(string baseURI)
        {
            return string.IsNullOrEmpty(baseURI) ? string.Empty : new Uri(baseURI).LocalPath;
        }

        internal void FillLocation(XmlReader reader)
        {
            IXmlLineInfo lineInfo;
            string path;

            lineInfo = reader as IXmlLineInfo;
            path = GetFilePath(reader.BaseURI);
            if (lineInfo != null && lineInfo.HasLineInfo())
            {
                Location = new ElementLocation(path, lineInfo);
            }
            if (reader.MoveToAttribute("Condition") && lineInfo.HasLineInfo())
            {
                ConditionLocation = new ElementLocation(path, lineInfo);
            }
            if (reader.MoveToAttribute("Label") && lineInfo.HasLineInfo())
            {
                LabelLocation = new ElementLocation(path, lineInfo);
            }
            reader.MoveToElement();
        }

        internal InvalidProjectFileException CreateError(XmlReader reader, string message, int columnOffset = 0)
        {
            var li = reader as IXmlLineInfo;
            bool valid = li != null && li.HasLineInfo();
            throw new InvalidProjectFileException(reader.BaseURI, valid ? li.LineNumber : 0, valid ? li.LinePosition + columnOffset : 0, 0, 0, message, null, null, null);
        }
    }
}