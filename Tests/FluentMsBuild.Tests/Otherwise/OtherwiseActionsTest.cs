using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMsBuild
{
    [TestClass]
    public class OtherwiseActionsTest
    {
        OtherwiseActions unit;
        Otherwise otherwise;

        [TestInitialize]
        public void TestInitialize()
        {
            Root root = Root.Create();
            otherwise = root.CreateOtherwiseElement();
            unit = new OtherwiseActions(otherwise);
        }
    }
}
