using System.Collections.Generic;
using Microsoft.Bot.Builder.Dialogs;

namespace AllInOne
{
    public class AddUserInfoDialog: DialogContainer
    {
        private const string DefaultName = nameof(AddUserInfoDialog);
        public AddUserInfoDialog(string dialogId = DefaultName, DialogSet dialogs = null) : base(dialogId, dialogs)
        {
            Dialogs.Add(dialogId, new WaterfallStep[]
            {
                async (dc, args, next) =>
                {
                    dc.ActiveDialog.State = new Dictionary<string, object>();
                    await dc.Prompt("textPrompt", "Введи имя.");

                },
                async (dc, args, next) =>
                {
                    dc.ActiveDialog.State["name"] = args["Value"];
                    await dc.Prompt("textPrompt", "Введи фамилию?");

                },
                async (dc, args, next) =>
                {
                    dc.ActiveDialog.State["lastName"] = args["Value"];
                    await dc.Context.SendActivity($"Спасибо, получены ваши данные\n\rИмя {dc.ActiveDialog.State["name"]}, фамилия {dc.ActiveDialog.State["lastName"]}");
                    await dc.End();
                }
            });

            Dialogs.Add("textPrompt", new TextPrompt());
        }
    }
}
