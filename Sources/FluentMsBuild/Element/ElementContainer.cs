using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Provides an abstract container class for project elements.
    /// </summary>
    public abstract class ElementContainer : Element
    {
        LinkedList<Element> children;
        /// <summary>
        /// Gets a recursive depth-first enumerator over all child elements.
        /// </summary>
        /// <returns>Returns a recursive depth-first enumerator over all child elements.</returns>
        public IEnumerable<Element> AllChildren
        {
            get
            {
                foreach (var child in Children)
                {
                    var container = child as ElementContainer;
                    if (container != null)
                    {
                        foreach (var containersChild in container.AllChildren)
                        {
                            yield return containersChild;
                        }
                    }
                    yield return child;
                }
            }
        }
        /// <summary>
        /// Gets all child elements.
        /// </summary>
        /// <returns>Returns all child elements.</returns>
        public ICollection<Element> Children
        {
            get
            {
                return children.Where(p => !(p is Comment)).ToList();
            }
        }
        /// <summary>
        /// Gets all child elements, starting from the last child.
        /// </summary>
        /// <returns>Returns all child elements, starting from the last child.</returns>
        public ICollection<Element> ChildrenReversed
        {
            get
            {
                return children.Reverse<Element>().ToList();
            }
        }
        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        /// <returns>Returns the number of child elements.</returns>
        public int Count
        {
            get { return children.Count; }
        }
        /// <summary>
        /// Gets the first child element.
        /// </summary>
        /// <returns>Returns the first child element. Returns null if no child element exists.</returns>
        public Element FirstChild
        {
            get
            {
                return children.First == null ? null : children.First.Value;
            }
            //private set { }
        }
        /// <summary>
        /// Gets the last child element.
        /// </summary>
        /// <returns>Returns the last child element. Returns null if no child element exists.</returns>
        public Element LastChild
        {
            get
            {
                return children.Last == null ? null : children.Last.Value;
            }
            //private set { }
        }

        /// <summary>
        /// Creates a new instance of ElementContainer
        /// </summary>
        public ElementContainer()
        {
            children = new LinkedList<Element>();
        }

        /// <summary>
        /// Appends the child element as the last child of this project container.
        /// </summary>
        /// <param name="child">The project element to be appended.</param>
        public void AppendChild(Element child)
        {
            children.AddLast(child.LinkedListNode);
            child.Parent = this;
        }
        /// <summary>
        /// Inserts the child element after the reference element.
        /// </summary>
        /// <param name="child">The project element to be inserted.</param>
        /// <param name="reference">The project element to be inserted after.</param>
        public void InsertAfterChild(Element child, Element reference)
        {
            if (reference == null)
            {
                PrependChild(child);
            }
            else
            {
                child.Parent = this;
                children.AddAfter(reference.LinkedListNode, child.LinkedListNode);
            }
        }
        /// <summary>
        /// Inserts the child element before the reference element.
        /// </summary>
        /// <param name="child">The project element to be inserted.</param>
        /// <param name="reference">The project element to be inserted before.</param>
        public void InsertBeforeChild(Element child, Element reference)
        {
            if (reference == null)
            {
                AppendChild(child);
            }
            else
            {
                child.Parent = this;
                children.AddBefore(reference.LinkedListNode, child.LinkedListNode);
            }
        }
        /// <summary>
        /// Prepends the child element as the first child of this project container.
        /// </summary>
        /// <param name="child">The project element to be prepended.</param>
        public void PrependChild(Element child)
        {
            children.AddFirst(child.LinkedListNode);
            child.Parent = this;
        }
        /// <summary>
        /// Removes all the children, if any, from this project container.
        /// </summary>
        public void RemoveAllChildren()
        {
            while (children.Any())
            {
                RemoveChild(children.First());
            }
        }
        /// <summary>
        /// Removes a child element from this project container.
        /// </summary>
        /// <param name="child">The project element to be removed.</param>
        public void RemoveChild(Element child)
        {
            child.Parent = null;
            children.Remove(child.LinkedListNode);
        }
        internal override void SaveValue(XmlWriter writer)
        {
            base.SaveValue(writer);
            foreach (var child in children)
            {
                child.Save(writer);
            }
        }
        internal override void Load(XmlReader reader)
        {
            reader.Read();
            reader.MoveToContent();
            FillLocation(reader);
            if (reader.LocalName != XmlName ||
                reader.NamespaceURI != MSBuildNamespace)
            {
                throw CreateError(reader, string.Format("Unexpected XML {0} \"{1}\" in namespace \"{2}\" appeared, while \"{3}\" in namespace \"{4}\" is expected.",
                                reader.NodeType, reader.LocalName, reader.NamespaceURI, XmlName, MSBuildNamespace), -1);
            }
            while (reader.MoveToNextAttribute())
            {
                LoadAttribute(reader.Name, reader.Value);
            }
            LoadValue(reader);
        }
        internal override void LoadValue(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    var child = LoadChildElement(reader);
                    child.Load(reader.ReadSubtree());
                }
                else if (reader.NodeType == XmlNodeType.Comment)
                {
                    var commentElement = new Comment(ContainingProject);
                    commentElement.Load(reader);
                    AppendChild(commentElement);
                }
            }
        }
        /// <summary>
        /// Loads any child element of the ItemDefinition
        /// </summary>
        /// <param name="reader">The XmlReader to load chil element from</param>
        /// <returns>The loaded child Element</returns>
        protected abstract Element LoadChildElement(XmlReader reader);
    }
}