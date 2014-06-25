
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped UsingTask element
    /// </summary>
    public interface IUsingTaskActions : IElementContainerActions<IUsingTaskActions, UsingTask>
    {
        /// <summary>
        /// Sets the TaskFactory property
        /// </summary>
        /// <param name="taskFactory">The TaskFactory to set</param>
        /// <returns>The UsingTaskActions to allow chaining</returns>
        IUsingTaskActions WithTaskFactory(string taskFactory);
    }
}