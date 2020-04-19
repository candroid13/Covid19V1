using System.Collections.Generic;
using Covid19.Entities.ComplexTypes;
using Covid19.Entities.Concrete;

namespace Covid19.Business.Abstract
{
    public interface IParameterService
    {
        List<Parameter> GetAll();
        Parameter GetById(int id);
        ParameterMessage GetMessage(int id);
    }
}
