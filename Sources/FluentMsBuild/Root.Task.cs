
namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Creates a task.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <returns>Returns the task.</returns>
        public Task CreateTaskElement(string name)
        {
            return new Task(name, this);
        }
    }
}
