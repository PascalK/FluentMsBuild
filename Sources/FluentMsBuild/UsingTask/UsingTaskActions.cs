
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a UsingTask element adding chaining functinality
    /// </summary>
    public class UsingTaskActions : ElementContainerActions<IUsingTaskActions, UsingTask>, IUsingTaskActions
    {
        /// <summary>
        /// Creates a UsingTaskActions instance for a UsingTask element
        /// </summary>
        /// <param name="element">The UsingTask element to create a UsingTaskActions instance for</param>
        public UsingTaskActions(UsingTask element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets the TaskFactory property
        /// </summary>
        /// <param name="taskFactory">The TaskFactory to set</param>
        /// <returns>The UsingTaskActions to allow chaining</returns>
        public IUsingTaskActions WithTaskFactory(string taskFactory)
        {
            Element.TaskFactory = taskFactory;

            return this;
        }
    }
}
