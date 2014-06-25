using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents the ParameterGroup of an inline task.
    /// </summary>
    public class UsingTaskParameterGroup : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "ParameterGroup"; }
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
                throw new InvalidOperationException(
                    "Can not set Condition.");
            }
        }
        /// <summary>
        /// Gets all parameters of this parameter group.
        /// </summary>
        /// <returns>Returns all parameters.</returns>
        public ICollection<UsingTaskParameter> Parameters
        {
            //get
            //{
            //    return new CollectionFromEnumerable<ProjectUsingTaskParameterElement>(
            //      new FilteredEnumerable<ProjectUsingTaskParameterElement>(Children));
            //}
            get
            {
                return Children.OfType<UsingTaskParameter>().ToList();
            }
        }

        internal UsingTaskParameterGroup(Root containingProject)
        {
            ContainingProject = containingProject;
        }

        /// <summary>
        /// Adds a parameter to this parameter group.
        /// </summary>
        /// <param name="name">The name of the parameter to be added.</param>
        /// <returns>The new parameter.</returns>
        public IUsingTaskParameterActions AddParameter(string name)
        {
            return AddParameter(name, null, null, null);
        }
        /// <summary>
        /// Adds a parameter to this parameter group, using the given name, type, and attributes.
        /// </summary>
        /// <param name="name">The name of the parameter to be added.</param>
        /// <param name="output">The value of the Output attribute.</param>
        /// <param name="required">The value of the Required attribute.</param>
        /// <param name="parameterType">The type of the parameter.</param>
        /// <returns>Returns the new parameter.</returns>
        public IUsingTaskParameterActions AddParameter(string name, string output, string required, string parameterType)
        {
            UsingTaskParameter parameter;

            parameter = ContainingProject.CreateUsingTaskParameterElement(name, output, required, parameterType);
            AppendChild(parameter);

            return new UsingTaskParameterActions(parameter);
        }
        /// <summary>
        /// Loads a child element from an XmlReader
        /// </summary>
        /// <param name="reader">The XmlReader to load elements from</param>
        /// <returns>The loaded element</returns>
        protected override Element LoadChildElement(XmlReader reader)
        {
            return AddParameter(reader.LocalName).Element;
        }
    }
}