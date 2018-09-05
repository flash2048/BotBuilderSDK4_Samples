using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Azure.KeyVault;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Rest;

namespace AllInOne.Commands
{
    class ApiKeyServiceClientCredentials : ServiceClientCredentials
    {
        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", "");
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }

    public class LangDetect : IDialogContinue
    {
        public async Task DialogBegin(DialogContext dc, IDictionary<string, object> dialogArgs = null)
        {
            var message = dc.Context.Activity.Text;

            if (!string.IsNullOrEmpty(message))
            {
               ITextAnalyticsClient client = new TextAnalyticsClient(new ApiKeyServiceClientCredentials())
            {
                Endpoint = "https://westeurope.api.cognitive.microsoft.com"
               };


                var result = client.DetectLanguageAsync(new BatchInput(
                    new List<Input>()
                    {
                        new Input("1", message)
                    })).Result;

                foreach (var document in result.Documents)
                {
                    await dc.Context.SendActivity(document.DetectedLanguages[0].Name);
                }
                     
                await dc.End();
            }
            else
            {
                await dc.Context.SendActivity("Введите текст для распознавания языка");
            }
        }

        public async Task DialogContinue(DialogContext dc)
        {
            await DialogBegin(dc);
        }
    }
}
