
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped ItemGroup element
    /// </summary>
    public interface IItemGroupActions : IElementContainerActions<IItemGroupActions, ItemGroup>
    {
    }
}
