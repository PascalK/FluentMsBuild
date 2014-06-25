using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents an Output Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class Output : Element
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "Output"; }
        }
        /// <summary>
        /// Determines whether this output element represents an output item, as opposed to an output property.
        /// </summary>
        /// <returns>Returns true if this output element represents an output item; false otherwise.</returns>
        public bool IsOutputItem
        {
            get { return !string.IsNullOrWhiteSpace(itemName); }
        }
        /// <summary>
        /// Determines whether this output element represents an output property, as opposed to an output item.
        /// </summary>
        /// <returns>Returns true if this output element represents an output property; false otherwise.</returns>
        public bool IsOutputProperty
        {
            get { return !string.IsNullOrWhiteSpace(propertyName); }
        }
        string itemName;
        /// <summary>
        /// Gets or sets the ItemName attribute value.
        /// </summary>
        /// <returns>Gets or sets the ItemName attribute value Returns an empty string if the attribute is not present.</returns>
        public string ItemName
        {
            get
            {
                return itemName ?? string.Empty;
            }
            set
            {
                itemName = value;
            }
        }
        string propertyName;
        /// <summary>
        /// Gets or sets the PropertyName attribute value.
        /// </summary>
        /// <returns>Gets or sets the PropertyName attribute value. Returns an empty string if the attribute is not present.</returns>
        public string PropertyName
        {
            get
            {
                return propertyName ?? string.Empty;
            }
            set
            {
                propertyName = value;
            }
        }
        string taskParameter;
        /// <summary>
        /// Gets or sets the TaskParameter attributevalue.
        /// </summary>
        /// <returns>Gets or sets the TaskParameter attribute value. Returns an empty string if the attribute is not present.</returns>
        public string TaskParameter
        {
            get
            {
                return taskParameter ?? string.Empty;
            }
            set
            {
                taskParameter = value;
            }
        }

        internal Output(string taskParameter, string itemName, string propertyName,
                        Root containintProject)
        {
            TaskParameter = taskParameter;
            ItemName = itemName;
            PropertyName = propertyName;
            ContainingProject = containintProject;
        }

        internal override void SaveValue(XmlWriter writer)
        {
            base.SaveValue(writer);
            SaveAttribute(writer, "TaskParameter", TaskParameter);
            if (IsOutputProperty)
                SaveAttribute(writer, "PropertyName", PropertyName);
            else
                SaveAttribute(writer, "ItemName", ItemName);
        }
        internal override void LoadAttribute(string name, string value)
        {
            switch (name)
            {
                case "TaskParameter":
                    TaskParameter = value;
                    break;
                case "PropertyName":
                    PropertyName = value;
                    break;
                case "ItemName":
                    ItemName = value;
                    break;
                default:
                    base.LoadAttribute(name, value);
                    break;
            }
        }
    }
}