using Covid19.DataAccess.Concrete.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19.DataAccess.Tests
{
    [TestClass]
    public class EntityFrameworkTest
    {
        [TestMethod]
        public void Get_all_returns_all_IntentMessage()
        {
            EfIntentDal intentDal = new EfIntentDal();

            var result = intentDal.GetIntentMessage(1);

            Assert.AreEqual("Merhaba", result.Text);

        } 
    }
}
