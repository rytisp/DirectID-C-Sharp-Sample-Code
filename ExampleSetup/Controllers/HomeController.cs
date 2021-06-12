using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExampleSetup.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;

namespace ExampleSetup.Controllers
{
    public class HomeController : Controller
    {
        private static string _authenticationToken;

        /// <summary>
        ///     Presents a form for populating the credentials required to
        ///     establish a Direct ID UserSessionEndpoint connection.
        /// </summary>
        public ActionResult Index()
        {
            return View(new CredentialsModel());
        }

        /// <summary>
        ///     Handles the form post submitted by the <see cref="Index" /> view,
        ///     using the supplied credentials
        /// </summary>
        [HttpPost]
        public async Task<ViewResult> Connect(CredentialsModel credentials)
        {
            _authenticationToken = AcquireOAuthAccessToken(credentials);
            var userSessionToken =
                await AcquireUserSessionToken(_authenticationToken, new Uri(credentials.UserSessionEndpoint));

            return View("Widget",
                new WidgetModel(userSessionToken, credentials.FullCDNPath, credentials.IndividualSummaryEndpoint));
        }

        /// <summary>
        ///     Load a Individuals Summary page
        /// </summary>
        [HttpPost]
        public ViewResult IndividualsSummary(WidgetModel model, string Summary)
        {
            return View(PopulateIndividualsSummaryModel(GetJson(Summary), Summary));
        }

        /// <summary>
        ///     Load a Individuals Details page
        /// </summary>
        public ActionResult IndividualDetails(string Link)
        {
            return View(PopulateIndividualDetailsModel(GetJson(Link)));
        }

        /// <summary>
        ///     Obtains an OAuth access token which can then be used to make authorized calls
        ///     to the Direct ID UserSessionEndpoint.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The returned value is expected to be included in the authentication header
        ///         of subsequent UserSessionEndpoint requests.
        ///     </para>
        ///     <para>
        ///         As the returned value authenticates the application, UserSessionEndpoint calls made using
        ///         this value should only be made using server-side code.
        ///     </para>
        /// </remarks>
        private static string AcquireOAuthAccessToken(CredentialsModel credentials)
        {
            TrimCredentialsModel(credentials);
            var context = new AuthenticationContext(
                credentials.Authority);

            var accessToken = context.AcquireToken(
                credentials.ResourceID,
                new ClientCredential(
                    credentials.ClientID,
                    credentials.SecretKey));

            if (accessToken == null)
            {
                throw new InvalidOperationException(
                    "Unable to acquire access token from resource: " + credentials.ResourceID +
                    ".  Please check your settings from Direct ID.");
            }

            return accessToken.AccessToken;
        }

        private static void TrimCredentialsModel(CredentialsModel credentials)
        {
            credentials.UserSessionEndpoint = credentials.UserSessionEndpoint.Trim();
            credentials.Authority = credentials.Authority.Trim();
            credentials.ClientID = credentials.ClientID.Trim();
            credentials.ResourceID = credentials.ResourceID.Trim();
            credentials.SecretKey = credentials.SecretKey.Trim();
            credentials.FullCDNPath = credentials.FullCDNPath.Trim(' ', '/');
            credentials.IndividualSummaryEndpoint = credentials.IndividualSummaryEndpoint.Trim(' ', '/');
        }

        /// <summary>
        ///     Queries <paramref name="apiEndpoint" /> with an http request
        ///     authorized with <paramref name="authenticationToken" />.
        /// </summary>
        private static async Task<string> AcquireUserSessionToken(
            string authenticationToken,
            Uri apiEndpoint)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authenticationToken);

            var response = await httpClient.GetAsync(apiEndpoint);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    "Unable to acquire access token from endpoint: " + apiEndpoint +
                    ".  Please check your settings from Direct ID.");
            }

            var userSessionResponse = await response.Content.ReadAsAsync<UserSessionResponse>();
            return userSessionResponse.token;
        }

        /// <summary>
        ///     Getting Json using authorization token
        /// </summary>
        private string GetJson(string summary)
        {
            var endpoint = HttpUtility.HtmlDecode(Decoder.DecodeForwardSlashes(summary));
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    _authenticationToken);
                return httpClient.GetAsync(endpoint).Result.Content.ReadAsStringAsync().Result;
            }
        }

        private static IEnumerable<IndividualsSummary> PopulateIndividualsSummaryModel(string json, string summary)
        {
            var parsedJson = JObject.Parse(json);
            var individuals = new List<IndividualsSummary>();
            foreach (var item in parsedJson["Individuals"])
            {
                individuals.Add(new IndividualsSummary((string) item["Reference"], (string) item["Timestamp"],
                    (string) item["Name"], (string) item["EmailAddress"], (string) item["UserID"],
                    RemoveSFromSummary(summary) + "/" + (string) item["Reference"]));
            }

            return individuals;
        }

        private static string RemoveSFromSummary(string summary)
        {
            return summary.Substring(0, summary.Length - 1);
        }

        private static IndividualDetails PopulateIndividualDetailsModel(string json)
        {
            dynamic parsedJson = JObject.Parse(json);
            var providers = parsedJson.Individual.Global.Bank.Providers[0];
            string provider = providers["Provider"];
            var accountsJson = providers.Accounts;
            var accounts = new List<AccountDetails>();
            GetAccounts(accountsJson, accounts);
            return new IndividualDetails((string) parsedJson.Individual["Reference"], provider, accounts);
        }

        private static void GetAccounts(dynamic accountsJson, List<AccountDetails> accounts)
        {
            foreach (var item in accountsJson)
            {
                var transactions = new List<Transaction>();
                foreach (var details in item["Transactions"])
                {
                    transactions.Add(new Transaction((string) details["Date"],
                        (string) details["Description"],
                        (string) details["Amount"], 
                        (string) details["Type"],
                        (string) details["Balance"],
                        (string) details["Category"],
                        (string) details["CategoryId"],
                        (string) details["CategoryType"],
                        (string)details["CategorizationKeyword"]
                        ));
                }

                var accountDetails = new AccountDetails((string) item["AccountName"], 
                    (string) item["AccountHolder"],
                    (string) item["AccountType"],
                    (string) item["ActivityAvailableFrom"],
                    (string) item["AccountNumber"],
                    (string) item["SortCode"],
                    (string) item["Balance"],
                    (string) item["BalanceFormatted"], 
                    (string) item["OpeningBalance"],
                    (string) item["OpeningBalanceFormatted"],
                    (string) item["CurrencyCode"],
                    (string) item["VerifiedOn"],
                    transactions);
                accounts.Add(accountDetails);
            }
        }
    }
}