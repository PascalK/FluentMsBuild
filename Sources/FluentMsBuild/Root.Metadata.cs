
namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Creates a metadata node with the specified name.
        /// </summary>
        /// <param name="name">The name of the metadata.</param>
        /// <returns>Returns the metadata node.</returns>
        public Metadata CreateMetadataElement(string name)
        {
            return new Metadata(name, this);
        }
        /// <summary>
        /// Creates a metadata node with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the metadata.</param>
        /// <param name="unevaluatedValue">The value of the metadata.</param>
        /// <returns>Returns the metadata element.</returns>
        public Metadata CreateMetadataElement(string name, string unevaluatedValue)
        {
            var metadata = CreateMetadataElement(name);
            metadata.Value = unevaluatedValue;
            return metadata;
        }
    }
}
