
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a UsingTaskParameter element adding chaining functinality
    /// </summary>
    public class UsingTaskParameterActions : ElementActions<IUsingTaskParameterActions, UsingTaskParameter>, IUsingTaskParameterActions
    {
        /// <summary>
        /// Creates a UsingTaskParameterActions instance for a UsingTaskParameter element
        /// </summary>
        /// <param name="element">The UsingTaskParameter element to create a UsingTaskParameterActions instance for</param>
        public UsingTaskParameterActions(UsingTaskParameter element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets the Output property
        /// </summary>
        /// <param name="output">The Output to set</param>
        /// <returns>The UsingTaskParameterActions to allow chaining</returns>
        public IUsingTaskParameterActions WithOutput(string output)
        {
            Element.Output = output;

            return this;
        }
        /// <summary>
        /// Sets the Required property
        /// </summary>
        /// <param name="required">The Required to set</param>
        /// <returns>The UsingTaskParameterActions to allow chaining</returns>
        public IUsingTaskParameterActions WithRequired(string required)
        {
            Element.Required = required;

            return this;
        }
        /// <summary>
        /// Sets the ParameterType property
        /// </summary>
        /// <param name="parameterType">The ParameterType to set</param>
        /// <returns>The UsingTaskParameterActions to allow chaining</returns>
        public IUsingTaskParameterActions WithParameterType(string parameterType)
        {
            Element.ParameterType = parameterType;

            return this;
        }
    }
}
