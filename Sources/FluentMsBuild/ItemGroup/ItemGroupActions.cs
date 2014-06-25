
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a ItemGroup element adding chaining functinality
    /// </summary>
    public class ItemGroupActions : ElementContainerActions<IItemGroupActions, ItemGroup>, IItemGroupActions
    {
        /// <summary>
        /// Creates a ItemGroupActions instance for a ItemGroup element
        /// </summary>
        /// <param name="element">The ItemGroup element to create a ItemGroupActions instance for</param>
        public ItemGroupActions(ItemGroup element)
            : base(element)
        {
        }
    }
}