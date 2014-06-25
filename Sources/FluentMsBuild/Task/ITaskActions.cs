
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped Task element
    /// </summary>
    public interface ITaskActions : IElementContainerActions<ITaskActions, Task>
    {
        /// <summary>
        /// Sets the ContinueOnError property
        /// </summary>
        /// <param name="continueOnError">The ContinueOnError to set</param>
        /// <returns>The TaskActions to allow chaining</returns>
        ITaskActions WithContinueOnError(string continueOnError);
        /// <summary>
        /// Sets the MSBuildArchitecture property
        /// </summary>
        /// <param name="msBuildArchitecture">The MSBuildArchitecture to set</param>
        /// <returns>The TaskActions to allow chaining</returns>
        ITaskActions WithMSBuildArchitecture(string msBuildArchitecture);
        /// <summary>
        /// Sets the MSBuildRuntime property
        /// </summary>
        /// <param name="msBuildRuntime">the MSBuildRuntime to set</param>
        /// <returns>The TaskActions to allow chaining</returns>
        ITaskActions WithMSBuildRuntime(string msBuildRuntime);
    }
}