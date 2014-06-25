
namespace FluentMsBuild
{
    /// <summary>
    /// Interface for adding chaining functinality to a wrapped UsingTaskParameter element
    /// </summary>
    public interface IUsingTaskParameterActions : IElementActions<IUsingTaskParameterActions, UsingTaskParameter>
    {
        /// <summary>
        /// Sets the Output property
        /// </summary>
        /// <param name="output">The Output to set</param>
        /// <returns>The UsingTaskParameterActions to allow chaining</returns>
        IUsingTaskParameterActions WithOutput(string output);
        /// <summary>
        /// Sets the Required property
        /// </summary>
        /// <param name="required">The Required to set</param>
        /// <returns>The UsingTaskParameterActions to allow chaining</returns>
        IUsingTaskParameterActions WithRequired(string required);
        /// <summary>
        /// Sets the ParameterType property
        /// </summary>
        /// <param name="parameterType">The ParameterType to set</param>
        /// <returns>The UsingTaskParameterActions to allow chaining</returns>
        IUsingTaskParameterActions WithParameterType(string parameterType);
    }
}