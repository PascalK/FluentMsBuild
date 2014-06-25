
namespace FluentMsBuild
{
    /// <summary>
    /// Adds generic actions for a container element class.
    /// </summary>
    /// <typeparam name="TReturnActions">Type of the return value to make chaining possible</typeparam>
    /// <typeparam name="TElement">Type of the element to wrap</typeparam>
    public interface IElementContainerActions<TReturnActions, TElement> : IElementActions<TReturnActions, TElement>
        where TReturnActions : IElementContainerActions<TReturnActions, TElement>
        where TElement : ElementContainer
    {
    }
}