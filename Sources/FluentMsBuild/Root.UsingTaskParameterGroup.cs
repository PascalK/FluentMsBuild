
namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Creates a parameter group for a UsingTask Element (MSBuild).
        /// </summary>
        /// <returns>Returns the parameter group.</returns>
        public UsingTaskParameterGroup CreateUsingTaskParameterGroupElement()
        {
            return new UsingTaskParameterGroup(this);
        }
    }
}