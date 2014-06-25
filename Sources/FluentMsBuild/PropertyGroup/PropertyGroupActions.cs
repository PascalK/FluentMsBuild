
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a Choose element adding chaining functinality
    /// </summary>
    public class PropertyGroupActions : ElementContainerActions<IPropertyGroupActions, PropertyGroup>, IPropertyGroupActions
    {
        /// <summary>
        /// Creates a PropertyGroupActions instance for a PropertyGroup element
        /// </summary>
        /// <param name="element">The PropertyGroup element to create a PropertyGroupActions instance for</param>
        public PropertyGroupActions(PropertyGroup element)
            : base(element)
        {
        }

        /// <summary>
        /// Adds a property
        /// </summary>
        /// <typeparam name="TPropertyValue">The type of the property to add</typeparam>
        /// <param name="name">The name of the property to add</param>
        /// <param name="unevaluatedValue">The value of the property to add</param>
        /// <returns>The PropertyActions to allow chaining</returns>
        public IPropertyActions<TPropertyValue> AddProperty<TPropertyValue>(string name, TPropertyValue unevaluatedValue)
        {
            return Element.AddProperty<TPropertyValue>(name, unevaluatedValue);
        }
        /// <summary>
        /// Sets a property
        /// </summary>
        /// <typeparam name="TPropertyValue">The type od the property to set</typeparam>
        /// <param name="name">The name of the property to set</param>
        /// <param name="unevaluatedValue">The value of the property to set</param>
        /// <returns>The PropertyActions to allow chaining</returns>
        public IPropertyActions<TPropertyValue> SetProperty<TPropertyValue>(string name, TPropertyValue unevaluatedValue)
        {
            return Element.SetProperty<TPropertyValue>(name, unevaluatedValue);
        }
    }
}
