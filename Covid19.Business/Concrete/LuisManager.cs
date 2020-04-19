using Covid19.Business.Abstract;
using Covid19.Core.Utilities.CustomData;
using Covid19.Core.Utilities.StringOperations;
using Covid19.DataAccess.Abstract;
using Covid19.Entities.Concrete;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Covid19.Business.Concrete
{
    [Serializable]
    public class LuisManager : ILuisService
    {
        private readonly ILuisDal _luisDal;
        public LuisManager(ILuisDal luisDal)
        {
            _luisDal = luisDal;
        }
        public async Task<LuisResult> GetLuisResult(string text)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            var luisAppId = ConfigurationManager.AppSettings["LuisAppId"];
            var endpointKey = ConfigurationManager.AppSettings["LuisAPIKey"];
            queryString["q"] = text;
            queryString["timezoneOffset"] = "60";
            queryString["verbose"] = "true";
            queryString["spellCheck"] = "false";
            queryString["staging"] = "false";
            var endpointUri = " https://westeurope.api.cognitive.microsoft.com/luis/v2.0/apps/" + luisAppId + "?" + queryString;
            var luisResult = await _luisDal.GetLuisResult(endpointUri, endpointKey);
            luisResult = NormalizeLuisEntites(luisResult);
            return luisResult;
        }
        public LuisResult NormalizeLuisEntites(LuisResult result)
        {
            if (result.Entities != null && result.Entities.Count > 0)
            {
                foreach (var entites in result.Entities.Where(x=>x.Type.Equals("Country")))
                {
                    if(entites.Entity.Equals("türkiyedeki") || entites.Entity.Equals("türkiye") || entites.Entity.Equals("türkiyede"))
                    {
                        entites.Entity = "Turkey";
                    }
                    else if(entites.Entity.Equals("çindeki") || entites.Entity.Equals("çin") || entites.Entity.Equals("çinde"))
                    {
                        entites.Entity = "China";
                    }
                    else if (entites.Entity.Equals("ispanya") || entites.Entity.Equals("ispanyadaki") || entites.Entity.Equals("ispanya"))
                    {
                        entites.Entity = "Spain";
                    }
                    else if (entites.Entity.Equals("amerikadaki") || entites.Entity.Equals("amerika") || entites.Entity.Equals("amerikada"))
                    {
                        entites.Entity = "America";
                    }
                    else
                    {
                        result.Entities.Clear();
                    }
                }
            }
            return result;
        }
    }
}
