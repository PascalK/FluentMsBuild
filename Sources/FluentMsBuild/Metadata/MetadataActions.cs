
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a Metadata element adding chaining functinality
    /// </summary>
    public class MetadataActions : ElementActions<IMetadataActions, Metadata>, IMetadataActions
    {
        /// <summary>
        /// Creates a MetadataActions instance for a Metadata element
        /// </summary>
        /// <param name="element">The Metadata element to create a MetadataActions instance for</param>
        public MetadataActions(Metadata element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets the Name property
        /// </summary>
        /// <param name="name">The name to set</param>
        /// <returns>The MetadataActions to allow chaining</returns>
        public IMetadataActions WithName(string name)
        {
            Element.Name = name;

            return this;
        }
        /// <summary>
        /// Sets the Value property
        /// </summary>
        /// <param name="value">The value to set</param>
        /// <returns>The MetadataActions to allow chaining</returns>
        public IMetadataActions WithValue(string value)
        {
            Element.Value = value;

            return this;
        }
    }
}