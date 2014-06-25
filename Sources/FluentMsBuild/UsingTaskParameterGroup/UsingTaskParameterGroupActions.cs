
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a UsingTaskParameterGroup element adding chaining functinality
    /// </summary>
    public class UsingTaskParameterGroupActions : ElementContainerActions<IUsingTaskParameterGroupActions, UsingTaskParameterGroup>, IUsingTaskParameterGroupActions
    {
        /// <summary>
        /// Creates a UsingTaskParameterGroupActions instance for a UsingTaskParameterGroup element
        /// </summary>
        /// <param name="element">The UsingTaskParameterGroup element to create a UsingTaskParameterGroupActions instance for</param>
        public UsingTaskParameterGroupActions(UsingTaskParameterGroup element)
            : base(element)
        {
        }
    }
}
