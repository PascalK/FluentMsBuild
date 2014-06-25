using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents a UsingTask Element (MSBuild) in an MSBuild project. The UsingTask element is used both for inline tasks and precompiled tasks.
    /// </summary>
    public class UsingTask : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "UsingTask"; }
        }
        string assemblyFile;
        /// <summary>
        /// Gets the value of the AssemblyFile attribute, which selects the name of the assembly to load.
        /// </summary>
        /// <returns>Returns the value of the AssemblyFile attribute. Returns an empty string if the attribute is not present.</returns>
        public string AssemblyFile
        {
            get
            {
                return assemblyFile ?? string.Empty;
            }
            set
            {
                assemblyFile = value;
            }
        }
        string assemblyName;
        /// <summary>
        /// Gets and sets the value of the AssemblyName attribute.
        /// </summary>
        /// <returns>Returns the value of the AssemblyName attribute. Returns an empty string if the attribute is not present.</returns>
        public string AssemblyName
        {
            get
            {
                return assemblyName ?? string.Empty;
            }
            set
            {
                assemblyName = value;
            }
        }
        /// <summary>
        /// Gets any ParameterGroup element for this inline task.
        /// </summary>
        /// <returns>Returns the parameter group. Returns null if no parameter group exists.</returns>
        public UsingTaskParameterGroup ParameterGroup
        {
            get { return FirstChild as UsingTaskParameterGroup; }
        }
        /// <summary>
        /// Gets the inner XML content of this inline task.
        /// </summary>
        /// <returns>Returns the inner XML content of the inline task. Returns null if no body exists.</returns>
        public UsingTaskBody TaskBody
        {
            get { return LastChild as UsingTaskBody; }
        }
        string taskFactory;
        /// <summary>
        /// Gets and sets the value of the TaskFactory attribute of this inline task.
        /// </summary>
        /// <returns>Returns the value of the TaskFactory attribute. Returns an empty string ifthe attribute is not present.</returns>
        public string TaskFactory
        {
            get
            {
                return taskFactory ?? string.Empty;
            }
            set
            {
                taskFactory = value;
            }
        }
        string taskName;
        /// <summary>
        /// Gets and sets the value of the TaskName attribute.
        /// </summary>
        /// <returns>Returns the value of the TaskName attribute. Returns an empty string if the attribute is not present.</returns>
        public string TaskName
        {
            get
            {
                return taskName ?? string.Empty;
            }
            set
            {
                taskName = value;
            }
        }

        internal UsingTask(string taskName, string assemblyFile, string assemblyName, Root containingProject)
        {
            TaskName = taskName;
            AssemblyFile = assemblyFile;
            AssemblyName = assemblyName;
            ContainingProject = containingProject;
        }

        /// <summary>
        /// Adds a new ParameterGroup element to this inline task.
        /// </summary>
        /// <returns>Returns the new parameter group.</returns>
        public IUsingTaskParameterGroupActions AddParameterGroup()
        {
            UsingTaskParameterGroup @group;

            @group = ContainingProject.CreateUsingTaskParameterGroupElement();
            PrependChild(@group);

            return new UsingTaskParameterGroupActions(@group);
        }
        /// <summary>
        /// Adds a new TaskBody element to this inline task.
        /// </summary>
        /// <param name="evaluate">A flag which, if true, expands the item and property values in the task body. This flag is true by default.</param>
        /// <param name="taskBody">The body of the task as a string.</param>
        /// <returns>Returns the new task body.</returns>
        public IUsingTaskBodyActions AddUsingTaskBody(string evaluate, string taskBody)
        {
            UsingTaskBody body;

            body = ContainingProject.CreateUsingTaskBodyElement(evaluate, taskBody);
            AppendChild(body);

            return new UsingTaskBodyActions(body);
        }
        internal override void LoadAttribute(string name, string value)
        {
            switch (name)
            {
                case "AssemblyName":
                    AssemblyName = value;
                    break;
                case "AssemblyFile":
                    AssemblyFile = value;
                    break;
                case "TaskFactory":
                    TaskFactory = value;
                    break;
                case "TaskName":
                    TaskName = value;
                    break;
                default:
                    base.LoadAttribute(name, value);
                    break;
            }
        }
        internal override void SaveValue(XmlWriter writer)
        {
            SaveAttribute(writer, "AssemblyName", AssemblyName);
            SaveAttribute(writer, "AssemblyFile", AssemblyFile);
            SaveAttribute(writer, "TaskFactory", TaskFactory);
            SaveAttribute(writer, "TaskName", TaskName);
            base.SaveValue(writer);
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
                case "ParameterGroup":
                    return AddParameterGroup().Element;
                case "Task":
                    return AddUsingTaskBody(null, null).Element;
                default:
                    throw new InvalidProjectFileException(string.Format(
                            "Child \"{0}\" is not a known node type.", reader.LocalName));
            }
        }
    }
}