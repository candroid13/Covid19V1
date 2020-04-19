using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Covid19.Entities.Concrete;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Covid19.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        #region Const
        private readonly BotEntity _botEntity;
        #endregion
        public MessagesController()
        {
            _botEntity = new BotEntity
            {
                BotEntityDetail = new BotEntityDetail
                {
                    StateParameters = new StateParams()
                }
            };
        }
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                _botEntity.ChannelId = activity.ChannelId;
                _botEntity.ConversationId = activity.Conversation.Id;
                await Conversation.SendAsync(activity, () => new Dialogs.RootDialog(_botEntity)); 
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            switch (message.Type)
            {
                case ActivityTypes.DeleteUserData:
                    // Implement user deletion here
                    // If we handle user deletion, return a real message
                    break;
                case ActivityTypes.ConversationUpdate:
                    // Handle conversation state changes, like members being added and removed
                    // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                    // Not available in all channels
                    break;
                case ActivityTypes.ContactRelationUpdate:
                    // Handle add/remove from contact lists
                    // Activity.From + Activity.Action represent what happened
                    break;
                case ActivityTypes.Typing:
                    // Handle knowing tha the user is typing
                    break;
                default:
                    break;
            }

            return null;
        }
    }
}