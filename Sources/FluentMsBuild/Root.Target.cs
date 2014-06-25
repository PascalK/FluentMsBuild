using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Gets all the child targets in this project.
        /// </summary>
        /// <returns>Returns all the child targets in this project.</returns>
        public ICollection<Target> Targets
        {
            get
            {
                return Children.OfType<Target>().ToList();
            }
        }
        /// <summary>
        /// Adds a target to the project.
        /// </summary>
        /// <param name="name">The name of the target to be added.</param>
        /// <returns>Returns the added target.</returns>
        public ITargetActions AddTarget(string name)
        {
            Target target;

            target = CreateTargetElement(name);
            AppendChild(target);

            return new TargetActions(target);
        }
        /// <summary>
        /// Creates a target.
        /// </summary>
        /// <param name="name">The name of the target.</param>
        /// <returns>Returns the target.</returns>
        public Target CreateTargetElement(string name)
        {
            return new Target(name, this);
        }
    }
}
