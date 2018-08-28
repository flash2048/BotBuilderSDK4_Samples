using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace AllInOne.Commands
{
    public class ToBase64Command: IDialogContinue
    {
        public async Task DialogBegin(DialogContext dc, IDictionary<string, object> dialogArgs = null)
        {
            var message = dc.Context.Activity.Text;

            if (!string.IsNullOrEmpty(message))
            {
                var textBytes = System.Text.Encoding.UTF8.GetBytes(message);
                var base64text = System.Convert.ToBase64String(textBytes);

                await dc.Context.SendActivity(base64text);
                await dc.End();
            }
            else
            {
                await dc.Context.SendActivity("Введите текст для преобразования в base64:");
            }
        }

        public async Task DialogContinue(DialogContext dc)
        {
            await DialogBegin(dc);
        }
    }
}
