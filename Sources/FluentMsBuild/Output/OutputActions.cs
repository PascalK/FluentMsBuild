
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap an Output element adding chaining functinality
    /// </summary>
    public class OutputActions : ElementActions<IOutputActions, Output>, IOutputActions
    {
        /// <summary>
        /// Creates a OutputActions instance for a Output element
        /// </summary>
        /// <param name="element">The Output element to create a OutputActions instance for</param>
        public OutputActions(Output element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets the ItemName property
        /// </summary>
        /// <param name="itemName">The ItemName to set</param>
        /// <returns>The OutputActions to allow chaining</returns>
        public IOutputActions WithItemName(string itemName)
        {
            Element.ItemName = itemName;

            return this;
        }
        /// <summary>
        /// Sets the PropertyName property
        /// </summary>
        /// <param name="propertyName">The PropertyName to set</param>
        /// <returns>The OutputActions to allow chaining</returns>
        public IOutputActions WithPropertyName(string propertyName)
        {
            Element.PropertyName = propertyName;

            return this;
        }
        /// <summary>
        /// Sets the TaskParameter property
        /// </summary>
        /// <param name="taskParameter">The TaskParameter to set</param>
        /// <returns>The OutputActions to allow chaining</returns>
        public IOutputActions WithTaskParameter(string taskParameter)
        {
            Element.TaskParameter = taskParameter;

            return this;
        }
    }
}