
namespace FluentMsBuild
{
    /// <summary>
    /// Base class for generic actions wrapping a container element.
    /// </summary>
    /// <typeparam name="TReturnActions">Type of the return value to make chaining possible</typeparam>
    /// <typeparam name="TElement">Type of the element to wrap</typeparam>
    public class ElementContainerActions<TReturnActions, TElement> : ElementActions<TReturnActions, TElement>, IElementContainerActions<TReturnActions, TElement>
        where TReturnActions : IElementContainerActions<TReturnActions, TElement>
        where TElement : ElementContainer
    {
        /// <summary>
        /// Creates a ElementContainerActions instance for an ElementContainer
        /// </summary>
        /// <param name="element">The ElementContainer to create a ElementContainerActions instance for</param>
        public ElementContainerActions(TElement element)
            : base(element)
        {
        }
    }
}
