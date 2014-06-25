
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped TElement element
    /// </summary>
    /// <typeparam name="TReturnActions">Type of the return value to make chaining possible</typeparam>
    /// <typeparam name="TElement">Type of the element to wrap</typeparam>
    public interface IElementActions<TReturnActions, TElement>
        where TReturnActions : IElementActions<TReturnActions, TElement>
        where TElement : Element
    {
        /// <summary>
        /// The wrapped element
        /// </summary>
        TElement Element { get; }
        /// <summary>
        /// Sets the condition of the wrapped element
        /// </summary>
        /// <param name="condition">The condition to set</param>
        /// <returns>Actions for the wrapped element</returns>
        TReturnActions WithCondition(string condition);
        /// <summary>
        /// Sets the label of the wrapped element
        /// </summary>
        /// <param name="label">The label to set</param>
        /// <returns>Actions for the wrapped element</returns>
        TReturnActions WithLabel(string label);
    }
}