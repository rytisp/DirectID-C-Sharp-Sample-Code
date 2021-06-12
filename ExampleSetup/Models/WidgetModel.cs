using System.Web;

namespace ExampleSetup.Models
{
    public class WidgetModel
    {
        public string Token { get; private set; }

        public string FullCDNPath { get; private set; }

        public string IndividualSummaryEndpoint { get; private set; }

        public string IndividualDetailsEndpoint { get; private set; }

        public WidgetModel()
        {
            
        }

        public WidgetModel(string token, string fullCDNPath, string individualSummaryEndpoint)
        {
            FullCDNPath = fullCDNPath;
            Token = token;
            IndividualSummaryEndpoint =
                HttpUtility.HtmlEncode(Decoder.EncodeForwardSlashes(individualSummaryEndpoint));
        }

    }
}