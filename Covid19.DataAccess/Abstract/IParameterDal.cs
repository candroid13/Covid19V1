using Covid19.Core.DataAccess;
using Covid19.Entities.ComplexTypes;
using Covid19.Entities.Concrete;

namespace Covid19.DataAccess.Abstract
{
    public interface IParameterDal : IEntityRepository<Parameter>
    {
        ParameterMessage GetParameterMessage(int id);
    }
}
