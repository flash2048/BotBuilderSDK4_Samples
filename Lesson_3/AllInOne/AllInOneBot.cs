using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllInOne.Commands;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using TextPrompt = Microsoft.Bot.Builder.Dialogs.TextPrompt;

namespace AllInOne
{
    public class AllInOneBot : IBot
    {

        private DialogSet _dialogs;

        public AllInOneBot()
        {
            _dialogs = new DialogSet();
            _dialogs.Add("textPrompt", new TextPrompt());
            _dialogs.Add("addUserInfo", new AddUserInfoDialog());

            _dialogs.Add("tobase64", new ToBase64Command());
            _dialogs.Add("frombase64", new FromBase64Command());
            _dialogs.Add("cards", new CardsCommand());


            // Register the card prompt
        }

        public async Task OnTurn(ITurnContext context)
        {
            var state = context.GetConversationState<Dictionary<string, object>>();

            var dialogCtx = _dialogs.CreateContext(context, state);
            if (context.Activity.Type == ActivityTypes.ConversationUpdate)
            {
                var newUserName = context.Activity.MembersAdded.FirstOrDefault()?.Name;
                if (!string.Equals("Bot", newUserName))
                {
                    //await dialogCtx.Begin("addUserInfo");
                }
            }

            if (context.Activity.Type == ActivityTypes.Message)
            {
                await dialogCtx.Continue();

                var message = context.Activity.Text.Trim();
                var indexOfSpace = message.IndexOf(" ", StringComparison.Ordinal);
                var command = indexOfSpace != -1 ? message.Substring(0, indexOfSpace).ToLower() : message.ToLower();

                switch (command)
                {
                    case "tobase64":

                        context.Activity.Text = indexOfSpace >= 0
                            ? context.Activity.Text.Substring(indexOfSpace + 1, message.Length - indexOfSpace - 1)
                            : String.Empty;
                        await dialogCtx.Begin("tobase64");
                        break;
                    case "frombase64":

                        context.Activity.Text = indexOfSpace >= 0
                            ? context.Activity.Text.Substring(indexOfSpace + 1, message.Length - indexOfSpace - 1)
                            : String.Empty;
                        await dialogCtx.Begin("frombase64");
                        break;
                    case "cards":
                        context.Activity.Text = indexOfSpace >= 0
                            ? context.Activity.Text.Substring(indexOfSpace + 1, message.Length - indexOfSpace - 1)
                            : String.Empty;
                        await dialogCtx.Begin("cards");
                        break;
                }
            }
        }
    }
}
