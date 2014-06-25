using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents a Task Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class Task : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return Name; }
        }
        string continueOnError;
        /// <summary>
        /// Gets or sets the ContinueOnError attribute value.
        /// </summary>
        /// <returns>Returns the ContinueOnError attribute value. Returns an empty string if the attribute is not present.</returns>
        public string ContinueOnError
        {
            get
            {
                return continueOnError ?? string.Empty;
            }
            set
            {
                continueOnError = value;
            }
        }
        string name;
        /// <summary>
        /// Gets the name of this task.
        /// </summary>
        /// <returns>Returns the name of this task.</returns>
        public string Name
        {
            get
            {
                return name ?? string.Empty;
            }
            private set
            {
                name = value;
            }
        }
        /// <summary>
        /// Gets all Output Element (MSBuild) children.
        /// </summary>
        /// <returns>Returns all Output element children.</returns>
        public ICollection<Output> Outputs
        {
            get
            {
                return AllChildren.OfType<Output>().ToList();
            }
        }
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        /// <summary>
        /// Gets all unevaluated parameters of this task.
        /// </summary>
        /// <returns>Returns all unevaluated parameters of this task.</returns>
        public IDictionary<string, string> Parameters
        {
            get { return parameters; }
        }
        //public string ExecuteTargets { get; set; }
        //public ElementLocation ExecuteTargetsLocation { get; set; }
        /// <summary>
        /// Location of the "ContinueOnError" attribute on this element, if any. If there is no such attribute, returns null;
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation ContinueOnErrorLocation { get; set; }
        /// <summary>
        /// Gets or sets the architecture value for the task. Returns empty string if it is not present. Removes the attribute if the value to set is empty.
        /// </summary>
        /// <returns>Returns System.String.</returns>
        public string MSBuildArchitecture { get; set; }
        /// <summary>
        /// Location of the "MSBuildArchitecture" attribute on this element, if any. If there is no such attribute, returns null;
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation MSBuildArchitectureLocation { get; set; }
        /// <summary>
        /// Gets or sets the runtime value for the task. Returns empty string if it is not present. Removes the attribute if the value to set is empty.
        /// </summary>
        /// <returns>Returns System.String.</returns>
        public string MSBuildRuntime { get; set; }
        /// <summary>
        /// Location of the "MSBuildRuntime" attribute on this element, if any. If there is no such attribute, returns null;
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation MSBuildRuntimeLocation { get; set; }

        internal Task(string taskName, Root containingProject)
            : this(containingProject)
        {
            Name = taskName;
        }
        internal Task(Root containingProject)
        {
            ContainingProject = containingProject;
        }

        /// <summary>
        /// Adds an Output item after the last child.
        /// </summary>
        /// <param name="taskParameter">The name of the task which outputs to the item.</param>
        /// <param name="itemName">The item name of the new item whose value is set to the output of the task.</param>
        /// <returns>Returns the added Output item.</returns>
        public IOutputActions AddOutputItem(string taskParameter, string itemName)
        {
            return AddOutputItem(taskParameter, itemName, null);
        }
        /// <summary>
        /// Adds a conditioned Output item to this task after the last child.
        /// </summary>
        /// <param name="taskParameter">The name of the parameter.</param>
        /// <param name="itemName">The item name of the item.</param>
        /// <param name="condition">The condition of the parameter.</param>
        /// <returns>Returns the added conditioned Output item.</returns>
        public IOutputActions AddOutputItem(string taskParameter, string itemName, string condition)
        {
            Output output;

            output = new Output(taskParameter, itemName, null, ContainingProject);
            if (condition != null)
            {
                output.Condition = condition;
            }
            AppendChild(output);

            return new OutputActions(output);
        }
        /// <summary>
        /// Adds an Output property to this task after the last child.
        /// </summary>
        /// <param name="taskParameter">The name of the parameter.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>Returns the added Output property.</returns>
        public IOutputActions AddOutputProperty(string taskParameter, string propertyName)
        {
            return AddOutputProperty(taskParameter, propertyName, null);
        }
        /// <summary>
        /// Adds a conditioned Output property to this task after the last child.
        /// </summary>
        /// <param name="taskParameter">The name of the parameter.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="condition">The condition of the property.</param>
        /// <returns>Returns the added conditioned Output property.</returns>
        public IOutputActions AddOutputProperty(string taskParameter, string propertyName, string condition)
        {
            Output output;

            output = new Output(taskParameter, null, propertyName, ContainingProject);
            if (condition != null)
            {
                output.Condition = condition;
            }
            AppendChild(output);

            return new OutputActions(output);
        }
        /// <summary>
        /// Gets the value of the parameter with the specified name.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>Returns the value of the parameter with the specified name. If no parameter by that name exists in the task, returns an empty string.</returns>
        public string GetParameter(string name)
        {
            string value;
            if (parameters.TryGetValue(name, out value))
            {
                return value;
            }
            return string.Empty;
        }
        /// <summary>
        /// Removes all parameters from the task. Does not remove any ContinueOnError and/or Condition attributes.
        /// </summary>
        public void RemoveAllParameters()
        {
            parameters.Clear();
        }
        /// <summary>
        /// Removes any parameter on this task with the specified name. If no parameter by that name exists in the task, does nothing.
        /// </summary>
        /// <param name="name">The name of the parameter to remove.</param>
        public void RemoveParameter(string name)
        {
            parameters.Remove(name);
        }
        /// <summary>
        /// Updates or adds a parameter on this task
        /// </summary>
        /// <param name="name">The name of the parameter to update or add.</param>
        /// <param name="unevaluatedValue">The value of the parameter.</param>
        public void SetParameter(string name, string unevaluatedValue)
        {
            parameters[name] = unevaluatedValue;
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
                case "Output":
                    var output = ContainingProject.CreateOutputElement(null, null, null);
                    AppendChild(output);
                    return output;
                default:
                    throw new InvalidProjectFileException(string.Format(
                            "Child \"{0}\" is not a known node type.", reader.LocalName));
            }
        }
        internal override void LoadAttribute(string name, string value)
        {
            switch (name)
            {
                case "ContinueOnError":
                    ContinueOnError = value;
                    break;
                //case "ExecuteTargets":
                //    ExecuteTargets = value;
                //    break;
                case "MSBuildArchitecture":
                    MSBuildArchitecture = value;
                    break;
                case "MSBuildRuntime":
                    MSBuildRuntime = value;
                    break;
                case "xmlns":
                    break;
                case "Label":
                    Label = value;
                    break;
                case "Condition":
                    Condition = value;
                    break;
                default:
                    SetParameter(name, value);
                    break;
            }
        }
        internal override void SaveValue(XmlWriter writer)
        {
            SaveAttribute(writer, "ContinueOnError", ContinueOnError);
            //SaveAttribute(writer, "ExecuteTargets", ExecuteTargets);
            SaveAttribute(writer, "MSBuildArchitecture", MSBuildArchitecture);
            SaveAttribute(writer, "MSBuildRuntime", MSBuildRuntime);
            foreach (var parameter in parameters)
            {
                SaveAttribute(writer, parameter.Key, parameter.Value);
            }
            base.SaveValue(writer);
        }
    }
}