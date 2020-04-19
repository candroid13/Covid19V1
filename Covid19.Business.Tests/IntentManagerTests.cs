using Covid19.Business.Concrete;
using Covid19.DataAccess.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Covid19.Business.Tests
{
    [TestClass]
    public class IntentManagerTests
    {
        public void Intent_validation_check()
        {
            Mock<IIntentDal> mock = new Mock<IIntentDal>();
            IntentManager intentManager = new IntentManager(mock.Object);
            intentManager.GetAll();
        }
    }
}
