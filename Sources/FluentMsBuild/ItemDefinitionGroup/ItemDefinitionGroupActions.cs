
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a ItemDefinitionGroup element adding chaining functinality
    /// </summary>
    public class ItemDefinitionGroupActions : ElementContainerActions<IItemDefinitionGroupActions, ItemDefinitionGroup>, IItemDefinitionGroupActions
    {
        /// <summary>
        /// Creates a ItemDefinitionGroupActions instance for a ItemDefinitionGroup element
        /// </summary>
        /// <param name="element">The ItemDefinitionGroup element to create a ItemDefinitionGroupActions instance for</param>
        public ItemDefinitionGroupActions(ItemDefinitionGroup element)
            : base(element)
        {
        }
    }
}
