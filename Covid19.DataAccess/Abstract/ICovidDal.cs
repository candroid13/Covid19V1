using Covid19.Entities.Concrete;
using System.Threading.Tasks;

namespace Covid19.DataAccess.Abstract
{
    public interface ICovidDal
    {
        Task<CovidParams> GetCovidResult();
    }
}
