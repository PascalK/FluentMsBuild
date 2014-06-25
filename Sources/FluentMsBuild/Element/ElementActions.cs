
namespace FluentMsBuild
{
    /// <summary>
    /// Base class for generic actions wrapping an element.
    /// </summary>
    /// <typeparam name="TReturnActions">Type of the return value to make chaining possible</typeparam>
    /// <typeparam name="TElement">Type of the element to wrap</typeparam>
    public class ElementActions<TReturnActions, TElement> : IElementActions<TReturnActions, TElement>
        where TReturnActions : IElementActions<TReturnActions, TElement>
        where TElement : Element
    {
        /// <summary>
        /// The wrapped element
        /// </summary>
        public TElement Element { get; private set; }

        /// <summary>
        /// Creates a ElementActions instance for an Element
        /// </summary>
        /// <param name="element">The Element to create a ElementActions instance for</param>
        public ElementActions(TElement element)
        {
            this.Element = element;
        }

        /// <summary>
        /// Sets the condition of the wrapped element
        /// </summary>
        /// <param name="condition">The condition to set</param>
        /// <returns>Actions for the wrapped element</returns>
        public TReturnActions WithCondition(string condition)
        {
            Element.Condition = condition;

            return (TReturnActions)(IElementActions<TReturnActions, TElement>)this;
        }
        /// <summary>
        /// Sets the label of the wrapped element
        /// </summary>
        /// <param name="label">The label to set</param>
        /// <returns>Actions for the wrapped element</returns>
        public TReturnActions WithLabel(string label)
        {
            Element.Label = label;

            return (TReturnActions)(IElementActions<TReturnActions, TElement>)this;
        }
    }
}
