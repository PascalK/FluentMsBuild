
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a Property element adding chaining functinality
    /// </summary>
    public class PropertyActions<TPropertyValue> : ElementActions<IPropertyActions<TPropertyValue>, Property>, IPropertyActions<TPropertyValue>
    {
        /// <summary>
        /// Creates a PropertyActions instance for a Property element
        /// </summary>
        /// <param name="element">The Property element to create a PropertyActions instance for</param>
        public PropertyActions(Property element)
            : base(element)
        {
        }

        /// <summary>
        /// Set the Value
        /// </summary>
        /// <param name="value">The value to set</param>
        /// <returns>The PropertyActions to allow chaining</returns>
        public IPropertyActions<TPropertyValue> WithValue(TPropertyValue value)
        {
            PropertyTypes<TPropertyValue> type;

            type = PropertyTypes<TPropertyValue>.Get();
            Element.Value = type.ToValue(value);

            return this;
        }
        /// <summary>
        /// Sets the Name
        /// </summary>
        /// <param name="name">The name to set</param>
        /// <returns>The PropertyActions to allow chaining</returns>
        public IPropertyActions<TPropertyValue> WithName(string name)
        {
            Element.Name = name;

            return this;
        }
    }
}
