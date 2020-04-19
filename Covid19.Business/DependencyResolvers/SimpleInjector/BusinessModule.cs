using Covid19.Business.Abstract;
using Covid19.Business.Concrete;
using Covid19.DataAccess.Abstract;
using Covid19.DataAccess.Concrete.EntityFramework;
using Covid19.DataAccess.Concrete.WebService;
using SimpleInjector;
using System.Data.Entity;

namespace Covid19.Business.DependencyResolvers.SimpleInjector
{
    public class BusinessModule 
    {
        public static void Load(Container builder)
        {
            builder.Register<IIntentService, IntentManager>(Lifestyle.Singleton);
            builder.Register<IIntentDal, EfIntentDal>(Lifestyle.Singleton);

            builder.Register<IMessageSendService, MessageSendManager>(Lifestyle.Singleton);
            builder.Register<IMessageSendDal, EfMessageSendDal>(Lifestyle.Singleton);

            builder.Register<IParameterService, ParameterManager>(Lifestyle.Singleton);
            builder.Register<IParameterDal, EfParameterDal>(Lifestyle.Singleton);

            builder.Register<ILuisService, LuisManager>(Lifestyle.Singleton);
            builder.Register<ILuisDal, RsLuisDal>(Lifestyle.Singleton);

            builder.Register<ICovidService, CovidManager>(Lifestyle.Singleton);
            builder.Register<ICovidDal, RsCovidDal>(Lifestyle.Singleton);

            builder.Register<DbContext, Covid19Context>();
            //builder.Register<IAzureRedisCacheService, AzureRedisCacheManager>(Lifestyle.Singleton);
            //builder.Register<IAzureRedisCacheDal, RsAzureRedisCacheDal>(Lifestyle.Singleton);

        }

    }
}
