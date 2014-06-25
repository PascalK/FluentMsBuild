using System;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents the body of an inline task.
    /// </summary>
    public class UsingTaskBody : Element
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "Task"; }
        }

        /// <summary>
        /// Gets null because the Condition attribute is nonexistent for this element and a nonexistent condition is implicitly true.
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
        string evaluate;
        /// <summary>
        /// Gets the value of the Evaluate attribute.
        /// </summary>
        /// <returns>Returns the value of the Evaluate attribute, which is either "true" or "false". Returns "true" if the attribute is not present.</returns>
        public string Evaluate
        {
            get
            {
                return evaluate ?? string.Empty;
            }
            set
            {
                evaluate = value;
            }
        }

        string taskBody;
        /// <summary>
        /// Gets or sets the unevaluated value of the contents of the inline task.
        /// </summary>
        /// <returns>Returns the unevaluated inner XML content of the inline task. Returns an empty string if no inline task body is present.</returns>
        public string TaskBody
        {
            get
            {
                return taskBody ?? string.Empty;
            }
            set
            {
                taskBody = value;
            }
        }

        internal UsingTaskBody(string evaluate, string body, Root containingProject)
        {
            Evaluate = evaluate;
            TaskBody = body;
            ContainingProject = containingProject;
        }

        internal override void SaveValue(XmlWriter writer)
        {
            base.SaveValue(writer);
            SaveAttribute(writer, "Evaluate", Evaluate);
            if (!string.IsNullOrWhiteSpace(TaskBody))
                writer.WriteRaw(TaskBody);
        }
        internal override void LoadAttribute(string name, string value)
        {
            switch (name)
            {
                case "Evaluate":
                    Evaluate = value;
                    break;
                default:
                    base.LoadAttribute(name, value);
                    break;
            }
        }
        internal override void LoadValue(XmlReader reader)
        {
            TaskBody = reader.ReadInnerXml();
        }
    }
}