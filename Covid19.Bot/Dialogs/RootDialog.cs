using System;
using System.Threading.Tasks;
using Covid19.Business.Abstract;
using Covid19.Business.DependencyResolvers.SimpleInjector;
using Microsoft.Bot.Builder.Dialogs;
using System.Linq;
using System.Threading;
using Covid19.Entities.Concrete;
using System.Collections.Generic;
using Microsoft.Bot.Connector;

namespace Covid19.Bot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        #region Const
        private IIntentService _intentService;
        private IMessageSendService _messageSendService;
        private IParameterService _parameterService;
        private BotEntity _botEntity;
        private ILuisService _luisService;
        #endregion
        public RootDialog(BotEntity botEntity)
        {
            _botEntity = botEntity;
            _intentService = InstanceFactory.GetInstance<IIntentService>();
            _messageSendService = InstanceFactory.GetInstance<IMessageSendService>();
            _parameterService = InstanceFactory.GetInstance<IParameterService>();
            _luisService = InstanceFactory.GetInstance<ILuisService>();
        }
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            _botEntity.BotEntityDetail.Message = activity.Text;
            var luisResult = await _luisService.GetLuisResult(_botEntity.BotEntityDetail.Message);
            _botEntity.BotEntityDetail.IntentName = luisResult.TopScoringIntent.Intent;
            _botEntity.LuisEntites = luisResult.Entities;
            var intentList = _intentService.GetAll();
            _botEntity.BotEntityDetail.IntentId = intentList.First(x => x.IntentName.Equals(_botEntity.BotEntityDetail.IntentName)).IntentId;
  
            var intentParameters = _intentService.GetParameters(_botEntity.BotEntityDetail.IntentId);
            if (intentParameters.Count > 0)
            {
                var entityFields = new List<EntityField>();
                foreach (var entity in intentParameters)
                {
                    entityFields.Add(new EntityField(entity.ParameterName, string.Empty));
                }
                foreach (var entity in _botEntity.LuisEntites)
                {
                    EntityField first = null;
                    foreach (var x in entityFields)
                    {
                        if (!x.Name.Equals(entity.Type)) continue;
                        first = x;
                        break;
                    }

                    if (first != null) first.Value = entity.Entity;
                }
                _botEntity.BotEntityDetail.IntentEntities = entityFields;

                await context.Forward(
                             new ParameterDialog(_messageSendService, _parameterService), ResumeAfterDialog, _botEntity,
                             CancellationToken.None);
            }
            else
            {
                await context.Forward(
                              new IntentDialog(_intentService), ResumeAfterDialog, _botEntity,
                              CancellationToken.None);
            }
        }

        private async Task ResumeAfterDialog(IDialogContext context, IAwaitable<BotEntity> result)
        {
            var activity = await result;
            _botEntity = activity;
            _botEntity.BotEntityDetail = new BotEntityDetail
            {
                StateParameters = new StateParams()
            };
            context.Done(activity);
        }
    }
}