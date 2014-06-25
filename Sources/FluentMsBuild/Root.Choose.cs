using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    public partial class Root
    {
        /// <summary>
        /// Gets all child Choose Element (MSBuild) in this project.
        /// </summary>
        /// <returns>Returns all child Choose elements.</returns>
        public ICollection<Choose> ChooseElements
        {
            get
            {
                return Children.OfType<Choose>().ToList();
            }
        }
        /// <summary>
        /// Creates a Choose Element (MSBuild).
        /// </summary>
        /// <returns>A ChoseElement class.</returns>
        public Choose CreateChooseElement()
        {
            return new Choose(this);
        }
        /// <summary>
        /// Creates and adds an choose element to this project.
        /// </summary>
        /// <returns>Returns the added choose element.</returns>
        public IChooseActions AddChooseElement()
        {
            Choose choose;

            choose = CreateChooseElement();
            AppendChild(choose);

            return new ChooseActions(choose);
        }
    }
}
