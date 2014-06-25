
namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Creates an OnError Element (MSBuild).
        /// </summary>
        /// <param name="executeTargets">The targets to execute if a task fails.</param>
        /// <returns>Returns the OnError element.</returns>
        public OnError CreateOnErrorElement(string executeTargets)
        {
            return new OnError(executeTargets, this);
        }
    }
}
