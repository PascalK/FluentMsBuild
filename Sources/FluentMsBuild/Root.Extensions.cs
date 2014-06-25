
namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Creates a ProjectExtensions Element (MSBuild).
        /// </summary>
        /// <returns>Returns the ProjectExtensions element.</returns>
        public Extensions CreateProjectExtensionsElement()
        {
            return new Extensions(this);
        }
    }
}