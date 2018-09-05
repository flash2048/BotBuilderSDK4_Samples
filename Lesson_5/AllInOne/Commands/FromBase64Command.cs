using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace AllInOne.Commands
{
    public class FromBase64Command: IDialogContinue
    {
        public async Task DialogBegin(DialogContext dc, IDictionary<string, object> dialogArgs = null)
        {
            var message = dc.Context.Activity.Text;
            if (!string.IsNullOrEmpty(message))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(message);
                var normalText = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                await dc.Context.SendActivity(normalText);
                await dc.End();
            }
            else
            {
                await dc.Context.SendActivity("Введите base64 текст");
            }
        }

        public async Task DialogContinue(DialogContext dc)
        {
            await DialogBegin(dc);
        }
    }
}
