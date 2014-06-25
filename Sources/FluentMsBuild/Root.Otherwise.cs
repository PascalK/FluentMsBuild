
namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Creates an Otherwise Element (MSBuild). Caller must add it to the location of choice in the project.
        /// </summary>
        /// <returns>Returns the Otherwise element.</returns>
        public Otherwise CreateOtherwiseElement()
        {
            return new Otherwise(this);
        }
    }
}
