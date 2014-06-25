using System;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents an Import Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class Import : Element
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "Import"; }
        }

        string project;
        /// <summary>
        /// Gets or sets the Project attribute.
        /// </summary>
        /// <returns>Returns the value of the Project attribute.</returns>
        public string Project
        {
            get
            {
                return project ?? string.Empty;
            }
            set
            {
                project = value;
            }
        }

        internal override void SaveValue(XmlWriter writer)
        {
            SaveAttribute(writer, "Project", Project);
            base.SaveValue(writer);
        }

        internal Import(string project, Root containingProject)
            : this(containingProject)
        {
            Project = project;
        }
        internal Import(Root containingProject)
        {
            ContainingProject = containingProject;
        }

        internal override void LoadValue(XmlReader reader)
        {
            if (string.IsNullOrWhiteSpace(Project))
            {
                throw new InvalidOperationException("Project attribute is null or empty on an Import element");
                //TODO: throw new InvalidProjectFileException(Location, null, "Project attribute is null or empty on an Import element");
            }
            base.LoadValue(reader);
        }

        internal override void LoadAttribute(string name, string value)
        {
            switch (name)
            {
                case "Project":
                    Project = value;
                    break;
                default:
                    base.LoadAttribute(name, value);
                    break;
            }
        }
    }
}
