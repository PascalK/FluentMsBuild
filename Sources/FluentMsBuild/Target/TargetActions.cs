
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a Target element adding chaining functinality
    /// </summary>
    public class TargetActions : ElementContainerActions<ITargetActions, Target>, ITargetActions
    {
        /// <summary>
        /// Creates a TargetActions instance for a Target element
        /// </summary>
        /// <param name="element">The Target element to create a TargetActions instance for</param>
        public TargetActions(Target element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets the AfterTargets property
        /// </summary>
        /// <param name="afterTargets">The AfterTargets to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        public ITargetActions WithAfterTargets(string afterTargets)
        {
            Element.AfterTargets = afterTargets;

            return this;
        }
        /// <summary>
        /// Sets the BeforeTargets property
        /// </summary>
        /// <param name="beforeTargets">The BeforeTargets to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        public ITargetActions WithBeforeTargets(string beforeTargets)
        {
            Element.BeforeTargets = beforeTargets;

            return this;
        }
        /// <summary>
        /// Sets the DependsOnTargets property
        /// </summary>
        /// <param name="dependsOnTargets">The DependsOnTargets to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        public ITargetActions WithDependsOnTargets(string dependsOnTargets)
        {
            Element.DependsOnTargets = dependsOnTargets;

            return this;
        }
        /// <summary>
        /// Sets the Inputs property
        /// </summary>
        /// <param name="inputs">The Inputs to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        public ITargetActions WithInputs(string inputs)
        {
            Element.Inputs = inputs;

            return this;
        }
        /// <summary>
        /// Sets the KeepDuplicateOutputs property
        /// </summary>
        /// <param name="keepDuplicateOutputs">The KeepDuplicateOutputs to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        public ITargetActions WithKeepDuplicateOutputs(string keepDuplicateOutputs)
        {
            Element.KeepDuplicateOutputs = keepDuplicateOutputs;

            return this;
        }
        /// <summary>
        /// Sets the Outputs property
        /// </summary>
        /// <param name="outputs">The Outputs to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        public ITargetActions WithOutputs(string outputs)
        {
            Element.Outputs = outputs;

            return this;
        }
        /// <summary>
        /// Sets the Returns property
        /// </summary>
        /// <param name="returns">The Returns to set</param>
        /// <returns>The TargetActions to allow chaining</returns>
        public ITargetActions WithReturns(string returns)
        {
            Element.Returns = returns;

            return this;
        }
    }
}
