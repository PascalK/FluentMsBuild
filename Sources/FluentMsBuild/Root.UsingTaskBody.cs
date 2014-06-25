
namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Creates a task to be referenced by a UsingTask Element (MSBuild).
        /// </summary>
        /// <param name="evaluate">The string to evaluate.</param>
        /// <param name="body">The body to add.</param>
        /// <returns>Returns the task.</returns>
        public UsingTaskBody CreateUsingTaskBodyElement(string evaluate, string body)
        {
            return new UsingTaskBody(evaluate, body, this);
        }
    }
}