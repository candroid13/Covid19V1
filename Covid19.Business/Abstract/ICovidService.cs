using Covid19.Entities.Concrete;
using System.Threading.Tasks;

namespace Covid19.Business.Abstract
{
    public interface ICovidService
    {
        Task<CovidParams> GetCovidResult();
    }
}
