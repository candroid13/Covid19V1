using Covid19.Entities.Concrete;
using System.Threading.Tasks;

namespace Covid19.DataAccess.Abstract
{
    public interface ILuisDal
    {
        Task<LuisResult> GetLuisResult(string host, string endpointKey);
    }
}
