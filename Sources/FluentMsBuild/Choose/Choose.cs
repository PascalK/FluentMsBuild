using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents the Choose Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class Choose : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "Choose"; }
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
        /// Gets any Otherwise Element (MSBuild) child.
        /// </summary>
        /// <returns>Returns any Otherwise child. Returns null if no Otherwise child exists.</returns>
        public Otherwise OtherwiseElement
        {
            get { return LastChild as Otherwise; }
        }
        /// <summary>
        /// Gets all the When Element (MSBuild) children.
        /// </summary>
        /// <returns>Returns all the When children. There is always at least one When child.</returns>
        public ICollection<When> WhenElements
        {
            get
            {
                return Children.OfType<When>().ToList();
            }
        }

        internal Choose(Root containingProject)
        {
            ContainingProject = containingProject;
        }

        /// <summary>
        /// Loads the child elements of a Choose element from an XmlReader
        /// </summary>
        /// <param name="reader">The reader to load elements from</param>
        /// <returns>The loaded element</returns>
        protected override Element LoadChildElement(XmlReader reader)
        {
            Element loadedELement;
            string name;

            name = reader.LocalName;
            switch (name)
            {
                case "Otherwise":
                    Otherwise otherwise;

                    otherwise = ContainingProject.CreateOtherwiseElement();
                    AppendChild(otherwise);

                    loadedELement = otherwise;
                    break;
                case "When":
                    When when;

                    when = ContainingProject.CreateWhenElement(null);
                    PrependChild(when);

                    loadedELement = when;
                    break;
                default:
                    throw new InvalidProjectFileException(
                        string.Format("Child \"{0}\" is not a known node type.", name));
            }

            return loadedELement;
        }
    }
}