
namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Creates a When Element (MSBuild) with a specified Condition attribute.
        /// </summary>
        /// <param name="condition">The value of the Condition attribute.</param>
        /// <returns>Returns the When element.</returns>
        public When CreateWhenElement(string condition)
        {
            return new When(condition, this);
        }
    }
}
