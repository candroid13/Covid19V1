using System.Collections.Generic;
using Covid19.Entities.ComplexTypes;
using Covid19.Entities.Concrete;

namespace Covid19.Business.Abstract
{
    public interface IIntentService
    {
        List<Intent> GetAll();
        Intent GetById(int id);
        List<IntentParameter> GetParameters(int id);
        IntentMessage GetMessage(int id);
    }
}
