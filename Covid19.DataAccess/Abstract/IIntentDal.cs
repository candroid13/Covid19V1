using Covid19.Core.DataAccess;
using Covid19.Entities.ComplexTypes;
using Covid19.Entities.Concrete;
using System.Collections.Generic;

namespace Covid19.DataAccess.Abstract
{
    public interface IIntentDal : IEntityRepository<Intent>
    {
        IntentMessage GetIntentMessage(int id);
        List<IntentParameter> GetIntentParameters(int id);
    }
}
