using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KudoCode.Test.Acceptance.Lookups
{
    [TestClass]
    public class LookupsTests : InMemoryTest
    {
        [TestMethod]
        public void LookupsGet()
        {
            this.Start().Authenticate();
            new LookupsGetBddfy().Get(Connector);
        }
    }
}