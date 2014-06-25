namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped Property element
    /// </summary>
    public interface IPropertyActions<TPropertyValue> : IElementActions<IPropertyActions<TPropertyValue>, Property>
    {
        /// <summary>
        /// Set the Value
        /// </summary>
        /// <param name="value">The value to set</param>
        /// <returns>The PropertyActions to allow chaining</returns>
        IPropertyActions<TPropertyValue> WithValue(TPropertyValue value);
        /// <summary>
        /// Sets the Name
        /// </summary>
        /// <param name="name">The name to set</param>
        /// <returns>The PropertyActions to allow chaining</returns>
        IPropertyActions<TPropertyValue> WithName(string name);
    }
}