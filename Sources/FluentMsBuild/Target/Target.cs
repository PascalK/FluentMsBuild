using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents a Target Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class Target : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "Target"; }
        }
        string afterTargets;
        /// <summary>
        /// Gets or sets the AfterTargets attribute value.
        /// </summary>
        /// <returns>Returns the AfterTargets attribute value. Returns an empty string if the attribute is not present.</returns>
        public string AfterTargets
        {
            get
            {
                return afterTargets ?? string.Empty;
            }
            set
            {
                afterTargets = value;
            }
        }
        string beforeTargets;
        /// <summary>
        /// Gets or sets the BeforeTargets attribute value.
        /// </summary>
        /// <returns>Returns the BeforeTargets attribute value. Returns an empty string if the attribute is not present.</returns>
        public string BeforeTargets
        {
            get
            {
                return beforeTargets ?? string.Empty;
            }
            set
            {
                beforeTargets = value;
            }
        }
        string dependsOnTargets;
        /// <summary>
        /// Gets or sets the DependsOnTargets attribute value. Returns empty string if it is not present. Removes the attribute if the value to set is empty.
        /// </summary>
        /// <returns>Returns the DependsOnTargets attribute value. Returns an empty string if the attribute is not present.</returns>
        public string DependsOnTargets
        {
            get
            {
                return dependsOnTargets ?? string.Empty;
            }
            set
            {
                dependsOnTargets = value;
            }
        }
        string inputs;
        /// <summary>
        /// Gets or sets the Inputs attribute value. Returns empty string if it is not present. Removes the attribute if the value to set is empty.
        /// </summary>
        /// <returns>Returns the Inputs attribute value. Returns an empty string if the attribute is not present.</returns>
        public string Inputs
        {
            get
            {
                return inputs ?? string.Empty;
            }
            set
            {
                inputs = value;
            }
        }
        /// <summary>
        /// Gets all child item groups
        /// </summary>
        /// <returns>Returns all child item groups.</returns>
        public ICollection<ItemGroup> ItemGroups
        {
            get
            {
                return Children.OfType<ItemGroup>().ToList();
            }
        }
        string keepDuplicateOutputs;
        /// <summary>
        /// Gets or sets the TrimDuplicateOutputs attribute value.
        /// </summary>
        /// <returns>Returns the TrimDuplicateOutputs attribute value. Returns an empty string if the attribute is not present.</returns>
        public string KeepDuplicateOutputs
        {
            get
            {
                return keepDuplicateOutputs ?? string.Empty;
            }
            set
            {
                keepDuplicateOutputs = value;
            }
        }
        string name;
        /// <summary>
        /// Gets and sets the name of the this target.
        /// </summary>
        /// <returns>Returns the name of the this target.</returns>
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
        /// <summary>
        /// Get all child OnError Element (MSBuild).
        /// </summary>
        /// <returns>Returns all child OnError elements.</returns>
        public ICollection<OnError> OnErrors
        {
            get
            {
                return Children.OfType<OnError>().ToList();
            }
        }
        string outputs;
        /// <summary>
        /// Gets or sets the Outputs attribute value. Returns empty string if it is not present. Removes the attribute if the value to set is empty.
        /// </summary>
        /// <returns>Returns the Outputs attribute value. Returns an empty string if the attribute is not present.</returns>
        public string Outputs
        {
            get
            {
                return outputs ?? string.Empty;
            }
            set
            {
                outputs = value;
            }
        }
        /// <summary>
        /// Gets all child property groups.
        /// </summary>
        /// <returns>Returns all child property groups.</returns>
        public ICollection<PropertyGroup> PropertyGroups
        {
            get
            {
                return Children.OfType<PropertyGroup>().ToList();
            }
        }
        string returns;
        /// <summary>
        /// Gets or sets the Returns attribute value.
        /// </summary>
        /// <returns>Returns the returns attribute value; null if the attribute is not present</returns>
        public string Returns
        {
            get
            {
                return returns ?? string.Empty;
            }
            set
            {
                returns = value;
            }
        }
        /// <summary>
        /// Gets all child tasks.
        /// </summary>
        /// <returns>Returns all child tasks.</returns>
        public ICollection<Task> Tasks
        {
            get
            {
                return Children.OfType<Task>().ToList();
            }
        }
        /// <summary>
        /// Location of the AfterTargets attribute
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation AfterTargetsLocation { get; private set; }
        /// <summary>
        /// Location of the BeforeTargets attribute
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation BeforeTargetsLocation { get; private set; }
        /// <summary>
        /// Location of the DependsOnTargets attribute
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation DependsOnTargetsLocation { get; private set; }
        /// <summary>
        /// Location of the Inputs attribute
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation InputsLocation { get; private set; }
        /// <summary>
        /// Location of the TrimDuplicateOutputs attribute
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation KeepDuplicateOutputsLocation { get; private set; }
        /// <summary>
        /// Location of the Name attribute
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation NameLocation { get; private set; }
        /// <summary>
        /// Location of the Outputs attribute
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation OutputsLocation { get; private set; }
        /// <summary>
        /// Location of the Returns attribute
        /// </summary>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation ReturnsLocation { get; private set; }

        internal Target(string name, Root containingProject)
            : this(containingProject)
        {
            Name = name;
        }
        internal Target(Root containingProject)
        {
            ContainingProject = containingProject;
        }

        /// <summary>
        /// Adds an item group after the last child.
        /// </summary>
        /// <returns>Returns the added item group.</returns>
        public IItemGroupActions AddItemGroup()
        {
            ItemGroup itemGroup;

            itemGroup = ContainingProject.CreateItemGroupElement();
            AppendChild(itemGroup);

            return new ItemGroupActions(itemGroup);
        }
        /// <summary>
        /// Adds a property group after the last child.
        /// </summary>
        /// <returns>Returns the added property group.</returns>
        public IPropertyGroupActions AddPropertyGroup()
        {
            PropertyGroup propertyGroup;

            propertyGroup = ContainingProject.CreatePropertyGroupElement();
            AppendChild(propertyGroup);

            return new PropertyGroupActions(propertyGroup);
        }
        /// <summary>
        /// Adds a task to this target after any existing task.
        /// </summary>
        /// <param name="taskName">The name of the task to add.</param>
        /// <returns>Returns the added task.</returns>
        public ITaskActions AddTask(string taskName)
        {
            Task task;

            task = ContainingProject.CreateTaskElement(taskName);
            AppendChild(task);

            return new TaskActions(task);
        }
        /// <summary>
        /// Load a child element from an XmlReader
        /// </summary>
        /// <param name="reader">The XmlReader to load a child element from</param>
        /// <returns>The loaded child element</returns>
        protected override Element LoadChildElement(XmlReader reader)
        {
            switch (reader.LocalName)
            {
                case "OnError":
                    var error = new OnError(ContainingProject);
                    AppendChild(error);
                    return error;
                case "PropertyGroup":
                    return AddPropertyGroup().Element;
                case "ItemGroup":
                    return AddItemGroup().Element;
                default:
                    return AddTask(reader.LocalName).Element;
            }
        }
        // This seriously needs to change to become able to fill ElementLocation...
        internal override void LoadAttribute(string name, string value)
        {
            switch (name)
            {
                case "Name":
                    Name = value;
                    break;
                case "DependsOnTargets":
                    DependsOnTargets = value;
                    break;
                case "Returns":
                    Returns = value;
                    break;
                case "Inputs":
                    Inputs = value;
                    break;
                case "Outputs":
                    Outputs = value;
                    break;
                case "BeforeTargets":
                    BeforeTargets = value;
                    break;
                case "AfterTargets":
                    AfterTargets = value;
                    break;
                case "KeepDuplicateOutputs":
                    KeepDuplicateOutputs = value;
                    break;
                default:
                    base.LoadAttribute(name, value);
                    break;
            }
        }
        internal override void SaveValue(System.Xml.XmlWriter writer)
        {
            SaveAttribute(writer, "Name", Name);
            SaveAttribute(writer, "DependsOnTargets", DependsOnTargets);
            SaveAttribute(writer, "Returns", Returns);
            SaveAttribute(writer, "Inputs", Inputs);
            SaveAttribute(writer, "Outputs", Outputs);
            SaveAttribute(writer, "BeforeTargets", BeforeTargets);
            SaveAttribute(writer, "AfterTargets", AfterTargets);
            SaveAttribute(writer, "KeepDuplicateOutputs", KeepDuplicateOutputs);
            base.SaveValue(writer);
        }
    }
}