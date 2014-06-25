
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped Output element
    /// </summary>
    public interface IOutputActions : IElementActions<IOutputActions, Output>
    {
        /// <summary>
        /// Sets the ItemName property
        /// </summary>
        /// <param name="itemName">The ItemName to set</param>
        /// <returns>The OutputActions to allow chaining</returns>
        IOutputActions WithItemName(string itemName);
        /// <summary>
        /// Sets the PropertyName property
        /// </summary>
        /// <param name="propertyName">The PropertyName to set</param>
        /// <returns>The OutputActions to allow chaining</returns>
        IOutputActions WithPropertyName(string propertyName);
        /// <summary>
        /// Sets the TaskParameter property
        /// </summary>
        /// <param name="taskParameter">The TaskParameter to set</param>
        /// <returns>The OutputActions to allow chaining</returns>
        IOutputActions WithTaskParameter(string taskParameter);
    }
}