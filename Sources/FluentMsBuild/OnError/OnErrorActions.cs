
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a OnError element adding chaining functinality
    /// </summary>
    public class OnErrorActions : ElementActions<IOnErrorActions, OnError>, IOnErrorActions
    {
        /// <summary>
        /// Creates a OnErrorActions instance for a OnError element
        /// </summary>
        /// <param name="element">The OnError element to create a OnErrorActions instance for</param>
        public OnErrorActions(OnError element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets the ExecuteTargetsAttribute property
        /// </summary>
        /// <param name="executeTargetsAttribute">The executeTargetsAttribute to set</param>
        /// <returns>The OnErrorActions to allow chaining</returns>
        public IOnErrorActions WithExecuteTargetsAttribute(string executeTargetsAttribute)
        {
            Element.ExecuteTargetsAttribute = executeTargetsAttribute;

            return this;
        }
    }
}