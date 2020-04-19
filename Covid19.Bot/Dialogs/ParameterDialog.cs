using Covid19.Business.Abstract;
using Covid19.Entities.Concrete;
using Microsoft.Bot.Builder.Dialogs;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Bot.Dialogs
{
    public class ParameterDialog : IDialog<BotEntity>
    {
        #region Const
        private IMessageSendService _messageSendService;
        private IParameterService _parameterService;
        private BotEntity _botEntity;
        #endregion

        public ParameterDialog(IMessageSendService messageSendService, IParameterService parameterService)
        {
            _messageSendService = messageSendService;
            _parameterService = parameterService;
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
            var entityState = new EntityState(_botEntity);
            _botEntity.BotEntityDetail.StateParameters = entityState.Run();
            if (!_botEntity.BotEntityDetail.StateParameters.IsSuccess)
            {
                var parameters = _parameterService.GetAll();
                _botEntity.BotEntityDetail.StateParameters.MissingParameterId = parameters.Where(x => x.ParameterName.Equals(_botEntity.BotEntityDetail.StateParameters.MissingParameter)).FirstOrDefault().ParameterId;
                _botEntity.BotEntityDetail.StateParameters.MissingParameterMessage = _parameterService.GetMessage(_botEntity.BotEntityDetail.StateParameters.MissingParameterId).Text;
                PromptDialog.Choice(context,
                          ParameterMessageReceivedAsync,
                          _botEntity.BotEntityDetail.StateParameters.MissingParameterData,
                          _botEntity.BotEntityDetail.StateParameters.MissingParameterMessage
                        );
            }
            else
            {
                await context.PostAsync(_botEntity.BotEntityDetail.StateParameters.ResultMessage);
                context.Done(_botEntity);
            }
        }
        private async Task ParameterMessageReceivedAsync(IDialogContext context, IAwaitable<string> result)
        {
            var customerAnswer = await result;
            if (!string.IsNullOrEmpty(customerAnswer))
            {
                if (_botEntity.BotEntityDetail.StateParameters.MissingParameterData.Any(x => x.Equals(customerAnswer)))
                {
                    EntityField first = null;
                    foreach (var y in _botEntity.BotEntityDetail.IntentEntities)
                    {
                        if (y.Name == _botEntity.BotEntityDetail.StateParameters.MissingParameter)
                        {
                            first = y;
                            break;
                        }
                    }

                    if (first != null)
                        first
                            .Value = customerAnswer;
                }
            }
            var entityState = new EntityState(_botEntity);
            var stateParameters = entityState.Run();
            _botEntity.BotEntityDetail.StateParameters = stateParameters;
            if (!_botEntity.BotEntityDetail.StateParameters.IsSuccess)
            {
                var parameters = _parameterService.GetAll();
                _botEntity.BotEntityDetail.StateParameters.MissingParameterId = parameters.Where(x => x.ParameterName.Equals(_botEntity.BotEntityDetail.StateParameters.MissingParameter)).FirstOrDefault().ParameterId;
                _botEntity.BotEntityDetail.StateParameters.MissingParameterMessage = _parameterService.GetMessage(_botEntity.BotEntityDetail.StateParameters.MissingParameterId).Text;
                PromptDialog.Choice(context,
                          ParameterMessageReceivedAsync,
                          _botEntity.BotEntityDetail.StateParameters.MissingParameterData,
                          _botEntity.BotEntityDetail.StateParameters.MissingParameterMessage
                        );
            }
            else
            {
                await context.PostAsync(_botEntity.BotEntityDetail.StateParameters.ResultMessage);
                context.Done(_botEntity);
            }
        }
    }
}