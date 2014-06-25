using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Gets all child UsingTask Element (MSBuild) in this project.
        /// </summary>
        /// <returns>Returns all child UsingTask elements in this project.</returns>
        public ICollection<UsingTask> UsingTasks
        {
            get
            {
                return Children.OfType<UsingTask>().ToList();
            }
        }
        /// <summary>
        /// Adds a UsingTask Element (MSBuild) to the project.
        /// </summary>
        /// <param name="name">The task name.</param>
        /// <param name="assemblyFile">The file path to the assembly.</param>
        /// <param name="assemblyName">The name of the assembly to load.</param>
        /// <returns>Returns the added UsingTask element.</returns>
        public IUsingTaskActions AddUsingTask(string name, string assemblyFile, string assemblyName)
        {
            UsingTask usingTask;

            usingTask = CreateUsingTaskElement(name, assemblyFile, assemblyName);
            AppendChild(usingTask);

            return new UsingTaskActions(usingTask);
        }
        /// <summary>
        /// Adds a UsingTask Element (MSBuild) to the project.
        /// </summary>
        /// <param name="taskName">The task name.</param>
        /// <param name="assemblyFile">The file path to the assembly.</param>
        /// <param name="assemblyName">The name of the assembly to load.</param>
        /// <returns>Returns the UsingTask element.</returns>
        public UsingTask CreateUsingTaskElement(string taskName, string assemblyFile,
                                                       string assemblyName)
        {
            return new UsingTask(taskName, assemblyFile, assemblyName, this);
        }
    }
}
