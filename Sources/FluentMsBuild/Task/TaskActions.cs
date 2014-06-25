
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a Task element adding chaining functinality
    /// </summary>
    public class TaskActions : ElementContainerActions<ITaskActions, Task>, ITaskActions
    {
        /// <summary>
        /// Creates a TaskActions instance for a Task element
        /// </summary>
        /// <param name="element">The Task element to create a TaskActions instance for</param>
        public TaskActions(Task element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets the ContinueOnError property
        /// </summary>
        /// <param name="continueOnError">The ContinueOnError to set</param>
        /// <returns>The TaskActions to allow chaining</returns>
        public ITaskActions WithContinueOnError(string continueOnError)
        {
            Element.ContinueOnError = continueOnError;

            return this;
        }
        /// <summary>
        /// Sets the MSBuildArchitecture property
        /// </summary>
        /// <param name="msBuildArchitecture">The MSBuildArchitecture to set</param>
        /// <returns>The TaskActions to allow chaining</returns>
        public ITaskActions WithMSBuildArchitecture(string msBuildArchitecture)
        {
            Element.MSBuildArchitecture = msBuildArchitecture;

            return this;
        }
        /// <summary>
        /// Sets the MSBuildRuntime property
        /// </summary>
        /// <param name="msBuildRuntime">the MSBuildRuntime to set</param>
        /// <returns>The TaskActions to allow chaining</returns>
        public ITaskActions WithMSBuildRuntime(string msBuildRuntime)
        {
            Element.MSBuildRuntime = msBuildRuntime;

            return this;
        }
    }
}
