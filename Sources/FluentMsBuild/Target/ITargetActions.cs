
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped Target element
    /// </summary>
    public interface ITargetActions : IElementContainerActions<ITargetActions, Target>
    {
        /// <summary>
        /// Sets the AfterTargets property
        /// </summary>
        /// <param name="afterTargets">The AfterTargets to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        ITargetActions WithAfterTargets(string afterTargets);
        /// <summary>
        /// Sets the BeforeTargets property
        /// </summary>
        /// <param name="beforeTargets">The BeforeTargets to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        ITargetActions WithBeforeTargets(string beforeTargets);
        /// <summary>
        /// Sets the DependsOnTargets property
        /// </summary>
        /// <param name="dependsOnTargets">The DependsOnTargets to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        ITargetActions WithDependsOnTargets(string dependsOnTargets);
        /// <summary>
        /// Sets the Inputs property
        /// </summary>
        /// <param name="inputs">The Inputs to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        ITargetActions WithInputs(string inputs);
        /// <summary>
        /// Sets the KeepDuplicateOutputs property
        /// </summary>
        /// <param name="keepDuplicateOutputs">The KeepDuplicateOutputs to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        ITargetActions WithKeepDuplicateOutputs(string keepDuplicateOutputs);
        /// <summary>
        /// Sets the Outputs property
        /// </summary>
        /// <param name="outputs">The Outputs to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        ITargetActions WithOutputs(string outputs);
        /// <summary>
        /// Sets the Returns property
        /// </summary>
        /// <param name="returns">The Returns to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        ITargetActions WithReturns(string returns);
    }
}