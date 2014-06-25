using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents the Comment Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class Comment : Element
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return string.Empty; }
        }
        string comment;
        /// <summary>
        /// The value of the Comment
        /// </summary>
        public string Value
        {
            get
            {
                return comment ?? string.Empty;
            }
            set
            {
                comment = value;
            }
        }

        internal Comment(Root containingProject)
        {
            ContainingProject = containingProject;
        }

        internal override void Load(XmlReader reader)
        {
            FillLocation(reader);
            LoadValue(reader);
        }
        internal override void LoadValue(XmlReader reader)
        {
            if (reader.NodeType == XmlNodeType.Comment)
            {
                Value = reader.Value;
            }
        }
        internal override void Save(XmlWriter writer)
        {
            this.SaveValue(writer);
        }
        internal override void SaveValue(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                writer.WriteComment(Value);
            }
        }
    }
}
