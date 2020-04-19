using Covid19.Business.Abstract;
using Covid19.Entities.Concrete;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace Covid19.Bot.Dialogs
{
    [Serializable]
    public class IntentDialog : IDialog<BotEntity>
    {
        #region Const
        private IIntentService _intentService;
        private BotEntity _botEntity;
        #endregion
        public IntentDialog(IIntentService intentService)
        {
            _intentService = intentService;
        }
        public Task StartAsync(IDialogContext context)
        {
            context.Wait<BotEntity>(MessageReceivedAsync);
            return Task.CompletedTask;
        }
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<BotEntity> result)
        {
            var activity = await result;
            _botEntity = activity;
            var message = _intentService.GetMessage(_botEntity.BotEntityDetail.IntentId).Text;
            await context.PostAsync(message);
            context.Done(_botEntity);
        }
    }
}