namespace SIPI_PRESENTEEISM.Core.Integrations.Interfaces
{
    using Newtonsoft.Json.Linq;

    public interface IMail
    {
        Task SendEmail(JArray recipients, string subject, string textPart, string htmlPart);
    }
}
