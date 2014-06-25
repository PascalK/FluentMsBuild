using System;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents a parameter of an inline task.
    /// </summary>
    public class UsingTaskParameter : Element
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return Name; }
        }

        /// <summary>
        /// Gets a nonexistent condition, which is implicitly true.
        /// </summary>
        /// <returns>Returns a null.</returns>
        public override string Condition
        {
            get
            {
                return null;
            }
            set
            {
                throw new InvalidOperationException(
                    "Can not set Condition.");
            }
        }
        string name;
        /// <summary>
        /// Gets and sets the name of the parameter of this inline task.
        /// </summary>
        /// <returns>Returns the name of the parameter.</returns>
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
        string output;
        /// <summary>
        /// Gets or sets the optional Output attribute of this inline task.
        /// </summary>
        /// <returns>Returns the value of the Output attribute. Returns an empty string if the attribute is not present.</returns>
        public string Output
        {
            get
            {
                return output ?? string.Empty;
            }
            set
            {
                output = value;
            }
        }
        string parameterType;
        /// <summary>
        /// Gets or sets the Type attribute of this inline task.
        /// </summary>
        /// <returns>Returns the value of the Type attribute. Returns an empty string if the attribute is not present.</returns>
        public string ParameterType
        {
            get
            {
                return parameterType ?? string.Empty;
            }
            set
            {
                parameterType = value;
            }
        }
        string required;
        /// <summary>
        /// Gets or sets the Required attribute
        /// </summary>
        /// <returns>Returns the value of the Required attribute. Returns an empty string if the attribute is not present.</returns>
        public string Required
        {
            get
            {
                return required ?? string.Empty;
            }
            set
            {
                required = value;
            }
        }

        internal UsingTaskParameter(string name, string output, string required,
                            string parameterType, Root containingProject)
        {
            Name = name;
            Output = output;
            Required = required;
            ParameterType = parameterType;
            ContainingProject = containingProject;
        }

        internal override void LoadAttribute(string name, string value)
        {
            switch (name)
            {
                case "ParameterType":
                    ParameterType = value;
                    break;
                case "Output":
                    Output = value;
                    break;
                case "Required":
                    Required = value;
                    break;
                default:
                    base.LoadAttribute(name, value);
                    break;
            }
        }
        internal override void SaveValue(XmlWriter writer)
        {
            base.SaveValue(writer);
            SaveAttribute(writer, "ParameterType", ParameterType);
            SaveAttribute(writer, "Required", Required);
            SaveAttribute(writer, "Output", Output);
        }
    }
}