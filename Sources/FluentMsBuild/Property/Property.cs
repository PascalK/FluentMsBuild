using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents a Property element in an MSBuild project.
    /// </summary>
    public class Property : Element
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return Name; }
        }
        string name;
        /// <summary>
        /// Gets the property name.
        /// </summary>
        /// <returns>Returns the property name.</returns>
        public string Name
        {
            get
            {
                return name ?? string.Empty;
            }
            set
            {
                name = value;
            }
        }
        string internalValue;
        /// <summary>
        /// Gets or sets the unevaluated property value.
        /// </summary>
        /// <returns>Returns the unevaluated property value. Returns an empty string if the value is uninitialized.</returns>
        public string Value
        {
            get
            {
                return internalValue ?? string.Empty;
            }
            set
            {
                internalValue = value;
            }
        }

        internal Property(string name, Root containingProject)
        {
            Name = name;
            ContainingProject = containingProject;
        }

        internal override void SaveValue(XmlWriter writer)
        {
            base.SaveValue(writer);
            if (!string.IsNullOrWhiteSpace(Value))
            {
                writer.WriteValue(Value);
            }
        }
        internal override void LoadValue(XmlReader reader)
        {
            while (reader.Read() & reader.NodeType != XmlNodeType.Text)
                ;
            Value = reader.Value;
        }
    }
}