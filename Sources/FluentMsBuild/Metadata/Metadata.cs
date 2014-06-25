using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents a Metadata element in an MSBuild project.
    /// </summary>
    public class Metadata : Element
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
        /// Gets the metadata name.
        /// </summary>
        /// <returns>Returns the metadata name.</returns>
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
        /// Gets or sets the unevaluated metadata value.
        /// </summary>
        /// <returns>Returns the unevaluated metadata value. Returns an empty string if the value is uninitialized.</returns>
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

        internal Metadata(string name, Root containingProject)
        {
            Name = name;
            ContainingProject = containingProject;
        }

        internal override void SaveValue(XmlWriter writer)
        {
            base.SaveValue(writer);
            if (!string.IsNullOrWhiteSpace(Value))
                writer.WriteValue(Value);
        }
        internal override void LoadValue(XmlReader reader)
        {
            while (reader.Read() & reader.NodeType != XmlNodeType.Text)
                ;
            Value = reader.Value;
        }
    }
}