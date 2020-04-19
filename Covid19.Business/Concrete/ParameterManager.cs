using System;
using System.Collections.Generic;
using Covid19.Business.Abstract;
using Covid19.DataAccess.Abstract;
using Covid19.Entities.ComplexTypes;
using Covid19.Entities.Concrete;

namespace Covid19.Business.Concrete
{
    public class ParameterManager : IParameterService
    {
        private IParameterDal _parameterDal;

        public ParameterManager(IParameterDal parameterDal)
        {
            _parameterDal = parameterDal;
        }

        public List<Parameter> GetAll()
        {
            var parameters = _parameterDal.GetList();
            return parameters;
        }

        public Parameter GetById(int id)
        {
            return _parameterDal.Get(p => p.ParameterId == id);
        }

        public ParameterMessage GetMessage(int id)
        {
            return _parameterDal.GetParameterMessage(id);
        }
    }
}
