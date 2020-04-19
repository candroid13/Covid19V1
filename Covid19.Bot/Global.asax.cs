using Autofac;
using Covid19.Business.DependencyResolvers.SimpleInjector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Threading;
using System.Web.Http;

namespace Covid19.Bot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        CancellationTokenSource _getTokenAsyncCancellation = new CancellationTokenSource();
        protected void Application_Start()
        {
            var store = new InMemoryDataStore();
            Conversation.UpdateContainer(
                builder =>
                {
                    builder.Register(c => new CachingBotDataStore(store,
                            CachingBotDataStoreConsistencyPolicy
                                .ETagBasedConsistency))
                        .As<IBotDataStore<BotData>>()
                        .AsSelf()
                        .InstancePerLifetimeScope();
                    builder
                        .RegisterModule(new ReflectionSurrogateModule());
                });

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            BusinessModule.Load(container);
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configure(config =>
            {
                config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
                WebApiConfig.Register(config);
            });
            InstanceFactory.Container = container;
         
        }
        protected void Application_End()
        {
            _getTokenAsyncCancellation.Cancel();
        }
    }

}
