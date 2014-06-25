
namespace FluentMsBuild
{
    /// <summary>
    /// Class to wrap a Choose element adding chaining functinality
    /// </summary>
    public class ChooseActions : ElementActions<IChooseActions, Choose>, IChooseActions
    {
        /// <summary>
        /// Creates a ChooseActions instance for a Choose element
        /// </summary>
        /// <param name="element">The Choose element to create a ChooseActions instance for</param>
        public ChooseActions(Choose element)
            : base(element)
        {
        }

        /// <summary>
        /// Sets a new otherwise element
        /// </summary>
        /// <returns>The addded Otherwise element</returns>
        public IOtherwiseActions AddOtherwise()
        {
            var otherWise = Element.ContainingProject.CreateOtherwiseElement();
            Element.AppendChild(otherWise);

            return new OtherwiseActions(otherWise);
        }
        /// <summary>
        /// Creates and adds a new otherwise element
        /// </summary>
        /// <returns>The Choose element with the added Otherwise element</returns>
        public IChooseActions WithOtherwise()
        {
            var otherWise = Element.ContainingProject.CreateOtherwiseElement();
            Element.AppendChild(otherWise);

            return this;
        }
        /// <summary>
        /// Adds a when element to the Choose element
        /// </summary>
        /// <param name="condition">The condition for the when element</param>
        /// <returns>The added When element</returns>
        public IWhenActions AddWhen(string condition)
        {
            When when;

            when = Element.ContainingProject.CreateWhenElement(condition);
            Element.PrependChild(when);

            return new WhenActions(when);
        }
        /// <summary>
        /// Sets a when element to the Choose element
        /// </summary>
        /// <param name="condition">The condition for the when element</param>
        /// <returns>The Choose element with the added When element</returns>
        public IChooseActions WithWhen(string condition)
        {
            When when;

            when = Element.ContainingProject.CreateWhenElement(condition);
            Element.PrependChild(when);

            return this;
        }
    }
}
