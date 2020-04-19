using Covid19.DataAccess.Abstract;
using Covid19.Entities.Concrete;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Covid19.DataAccess.Concrete.WebService
{
    [Serializable]
    public class RsLuisDal : ILuisDal
    {
        public async Task<LuisResult> GetLuisResult(string host, string endpointKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", endpointKey);
                var response = await client.GetAsync(host);
                var strResponseContent = await response.Content.ReadAsStringAsync();
                var entity = JsonConvert.DeserializeObject<LuisResult>(strResponseContent);
                return entity;
            }
        }
    }
}
