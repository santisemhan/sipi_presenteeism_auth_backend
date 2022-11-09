namespace SIPI_PRESENTEEISM.Core.Integrations.Mailing
{
    using global::Mailjet.Client;
    using global::Mailjet.Client.Resources;
    using Newtonsoft.Json.Linq;
    using SIPI_PRESENTEEISM.Core.Integrations.Interfaces;
    using System.Threading.Tasks;

    public class Mailjet : IMail
    {
        private readonly IConfiguration configuration;
        private readonly MailjetClient _client;
        public Mailjet(IConfiguration configuration)
        {
            this.configuration = configuration;
            _client = new MailjetClient(this.configuration.GetValue<string>("Mailing:Mailjet:PUBLIC_APIKEY"),
                this.configuration.GetValue<string>("Mailing:Mailjet:PRIVATE_APIKEY"));
        }

        public async Task SendEmail(JArray recipients, string subject, string textPart, string htmlPart)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            }
            .Property(Send.FromEmail, "santisemhan3@gmail.com") // TODO: Crear el mail y asociarlo a Mailjet
            .Property(Send.FromName, "SIPI UADE G3")
            .Property(Send.Recipients, recipients)
            .Property(Send.Subject, subject)
            .Property(Send.TextPart, textPart)
            .Property(Send.HtmlPart, htmlPart); // TODO: Hacer un html mas lindo

            var response = await _client.PostAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("No se ha podido enviar el codigo de validación");
            }
        }     
    }
}
