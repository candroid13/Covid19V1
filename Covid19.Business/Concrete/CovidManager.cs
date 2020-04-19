using Covid19.Business.Abstract;
using Covid19.DataAccess.Abstract;
using Covid19.Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Covid19.Business.Concrete
{
    [Serializable]
    public class CovidManager : ICovidService
    {
        private readonly ICovidDal _covidDal;
        public CovidManager(ICovidDal covidDal)
        {
            _covidDal = covidDal;
        }

        public async Task<CovidParams> GetCovidResult()
        {
            return await _covidDal.GetCovidResult();
        }
    }
}
