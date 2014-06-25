
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a UsingTaskBody element adding chaining functinality
    /// </summary>
    public class UsingTaskBodyActions : ElementActions<IUsingTaskBodyActions, UsingTaskBody>, IUsingTaskBodyActions
    {
        /// <summary>
        /// Creates a UsingTaskBodyActions instance for a UsingTaskBody element
        /// </summary>
        /// <param name="element">The UsingTaskBody element to create a UsingTaskBodyActions instance for</param>
        public UsingTaskBodyActions(UsingTaskBody element)
            : base(element)
        {
        }
    }
}