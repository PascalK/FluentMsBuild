
namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Creates an Output Element (MSBuild).
        /// </summary>
        /// <param name="taskParameter">The name of the task's output parameter.</param>
        /// <param name="itemType">The item that receives the task's output parameter value.</param>
        /// <param name="propertyName">The property that receives the task's output parameter value.</param>
        /// <returns>Returns the Output element.</returns>
        public Output CreateOutputElement(string taskParameter, string itemType,
                                                                 string propertyName)
        {
            return new Output(taskParameter, itemType, propertyName, this);
        }
    }
}