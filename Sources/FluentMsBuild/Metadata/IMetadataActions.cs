
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped Metadata element
    /// </summary>
    public interface IMetadataActions : IElementActions<IMetadataActions, Metadata>
    {
        /// <summary>
        /// Sets the Name property
        /// </summary>
        /// <param name="name">The name to set</param>
        /// <returns>The MetadataActions to allow chaining</returns>
        IMetadataActions WithName(string name);
        /// <summary>
        /// Sets the Value property
        /// </summary>
        /// <param name="value">The value to set</param>
        /// <returns>The MetadataActions to allow chaining</returns>
        IMetadataActions WithValue(string value);
    }
}
