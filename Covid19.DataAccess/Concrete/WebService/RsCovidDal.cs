using Covid19.DataAccess.Abstract;
using Covid19.Entities.Concrete;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Covid19.DataAccess.Concrete.WebService
{
    [Serializable]
    public class RsCovidDal : ICovidDal
    {
        public async Task<CovidParams> GetCovidResult()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("application/json"));
                var url = "https://api.collectapi.com/corona/countriesData";
                client.DefaultRequestHeaders.Add("authorization", "apikey 7tpHjf5LRUrDqaFVE1lTT1:7Ghkik89iyBbOUppeBBZxC");
                var response = client.GetAsync(url).Result;
                var strResponseContent = await response.Content.ReadAsStringAsync();
                var entity = JsonConvert.DeserializeObject<CovidParams>(strResponseContent);
                return entity;
            }
        }
    }
}
