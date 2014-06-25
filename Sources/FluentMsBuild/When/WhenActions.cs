
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a When element adding chaining functinality
    /// </summary>
    public class WhenActions : ElementContainerActions<IWhenActions, When>, IWhenActions
    {
        /// <summary>
        /// Creates a WhenActions instance for a When element
        /// </summary>
        /// <param name="element">The When element to create a WhenActions instance for</param>
        public WhenActions(When element)
            : base(element)
        {
        }
    }
}
