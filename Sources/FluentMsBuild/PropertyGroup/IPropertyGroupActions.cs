
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped Choose element
    /// </summary>
    public interface IPropertyGroupActions : IElementContainerActions<IPropertyGroupActions, PropertyGroup>
    {
        /// <summary>
        /// Adds a property
        /// </summary>
        /// <typeparam name="TPropertyValue">The type of the property to add</typeparam>
        /// <param name="name">The name of the property to add</param>
        /// <param name="unevaluatedValue">The value of the property to add</param>
        /// <returns>The PropertyActions to allow chaining</returns>
        IPropertyActions<TPropertyValue> AddProperty<TPropertyValue>(string name, TPropertyValue unevaluatedValue);
        /// <summary>
        /// Sets a property
        /// </summary>
        /// <typeparam name="TPropertyValue">The type od the property to set</typeparam>
        /// <param name="name">The name of the property to set</param>
        /// <param name="unevaluatedValue">The value of the property to set</param>
        /// <returns>The PropertyActions to allow chaining</returns>
        IPropertyActions<TPropertyValue> SetProperty<TPropertyValue>(string name, TPropertyValue unevaluatedValue);
    }
}