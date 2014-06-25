
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped Choose element
    /// </summary>
    public interface IChooseActions : IElementContainerActions<IChooseActions, Choose>
    {
        /// <summary>
        /// Creates and adds a new otherwise element
        /// </summary>
        /// <returns>The addded Otherwise element</returns>
        IOtherwiseActions AddOtherwise();
        /// <summary>
        /// Sets a new otherwise element
        /// </summary>
        /// <returns>The Choose element with the added Otherwise element</returns>
        IChooseActions WithOtherwise();
        /// <summary>
        /// Adds a when element to the Choose element
        /// </summary>
        /// <param name="condition">The condition for the when element</param>
        /// <returns>The added When element</returns>
        IWhenActions AddWhen(string condition);
        /// <summary>
        /// Sets a when element to the Choose element
        /// </summary>
        /// <param name="condition">The condition for the when element</param>
        /// <returns>The Choose element with the added When element</returns>
        IChooseActions WithWhen(string condition);
    }
}
