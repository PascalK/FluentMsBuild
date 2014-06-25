namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a Otherwise element adding chaining functinality
    /// </summary>
    public class OtherwiseActions : ElementContainerActions<IOtherwiseActions, Otherwise>, IOtherwiseActions
    {
        /// <summary>
        /// Creates a OtherwiseActions instance for a Otherwise element
        /// </summary>
        /// <param name="element">The Otherwise element to create a OtherwiseActions instance for</param>
        public OtherwiseActions(Otherwise element)
            : base(element)
        {
        }
    }
}