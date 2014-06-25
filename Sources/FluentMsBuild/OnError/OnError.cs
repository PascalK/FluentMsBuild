using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents an OnError Element (MSBuild) in an MSBuild project.
    /// </summary>
    public class OnError : Element
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "OnError"; }
        }
        /// <summary>
        /// Gets or sets the value of the ExecuteTargets attribute.
        /// </summary>
        /// <returns>Returns the value of the ExecuteTargets attribute.</returns>
        public string ExecuteTargetsAttribute { get; set; }
        /// <summary>
        /// Location of the "ExecuteTargets" attribute on this element, if any. If there is no such attribute, returns null;
        /// </summary>
        /// <remarks>This property is not yet implemented</remarks>
        /// <returns>Returns Microsoft.Build.Construction.ElementLocation.</returns>
        public ElementLocation ExecuteTargetsLocation { get; private set; } //TODO

        internal OnError(Root containingProject)
        {
            ContainingProject = containingProject;
        }
        internal OnError(string executeTargets, Root containingProject)
            : this(containingProject)
        {
            ExecuteTargetsAttribute = executeTargets;
        }

        internal override void SaveValue(XmlWriter writer)
        {
            base.SaveValue(writer);
            SaveAttribute(writer, "ExecuteTargets", ExecuteTargetsAttribute);
        }
        internal override void LoadAttribute(string name, string value)
        {
            switch (name)
            {
                case "ExecuteTargets":
                    ExecuteTargetsAttribute = value;
                    break;
                default:
                    base.LoadAttribute(name, value);
                    break;
            }
        }
    }
}