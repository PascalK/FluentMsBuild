
namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Creates a parameter for use in a UsingTask Element (MSBuild) parameter group.
        /// </summary>
        /// <param name="name">The name of the UsingTask element.</param>
        /// <param name="output">Stores outputs from the task in the project file.</param>
        /// <param name="required">A user-defined task parameter that contains the parameter value as its value.</param>
        /// <param name="parameterType">The type of the parameter.</param>
        /// <returns>Returns the parameter.</returns>
        public UsingTaskParameter CreateUsingTaskParameterElement(string name, string output, string required, string parameterType)
        {
            return new UsingTaskParameter(name, output, required, parameterType, this);
        }
    }
}