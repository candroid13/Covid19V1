using System;
using System.Collections.Generic;
using Covid19.Business.Abstract;
using Covid19.DataAccess.Abstract;
using Covid19.Entities.ComplexTypes;
using Covid19.Entities.Concrete;

namespace Covid19.Business.Concrete
{
    public class IntentManager : IIntentService
    {
        private IIntentDal _intentDal;
        public IntentManager(IIntentDal intentDal)
        {
            _intentDal = intentDal;
        }

        public List<Intent> GetAll()
        {
            var intents = _intentDal.GetList();
            return intents;
        }

        public Intent GetById(int id)
        {
            return _intentDal.Get(p => p.IntentId == id);
        }

        public IntentMessage GetMessage(int id)
        {
            return _intentDal.GetIntentMessage(id);
        }

        public List<IntentParameter> GetParameters(int id)
        {
            return _intentDal.GetIntentParameters(id);
        }
    }
}
