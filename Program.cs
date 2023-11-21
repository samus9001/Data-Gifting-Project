using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using System.Web;

namespace DataGifting
{
    internal class Program
    {
        //const string MB_UNITS = "MB";
        //const string GB_UNITS = "GB";
        //SIM sim1 = new SIM();
        //sim1.PhoneNumber = "07725917672";
        //sim1.SerialNumber = "361308296409";

        static async Task Main(string[] args)
        {
            string baseUri = "https://id.ee.co.uk/";
            //string uri = null;
            string confirmedUri = "https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/B2C_1A_RPBT_SignUpSignIn/api/CombinedSigninAndSignup/confirmed";
            string loginUri = "https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/B2C_1A_RPBT_SignUpSignIn/SelfAsserted/";
            string dashboardUri = "https://id.ee.co.uk/id/dashboard";
            string dashboardRedirectUri = "https://ee.co.uk/exp/home/";
            string openidUri = "https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/b2c_1a_rpbt_signupsignin/v2.0/.well-known/openid-configuration";
            string useridUri = "https://auth.ee.co.uk/common/v1/customer-identity-profiles?identity-id=2dcfe49c-81a0-4581-8662-7735017ea90b";
            string identityUri = "https://api.ee.co.uk/digital/v1/person-identities/self";
            string usageDataUri = "https://ee.co.uk/plans-subscriptions/mobile/usage-since-last-bill";
            string userDataUri = "https://ee.co.uk/app/api/basic";
            string dataGiftUri = "https://ee.co.uk/plans-subscriptions/mobile/data-gifting";

            string p = "B2C_1A_RPBT_SignUpSignIn";
            string request_type = "RESPONSE";
            string signInName = "sameer99@outlook.com";
            string password = "D@tagifting2113";
            string rememberMe = "false";
            string pageViewId = null;
            string pageId = "CombinedSigninAndSignup";
            string trace = "[]";
            string diags = "";
            //var sim = new SIM("361308296409");]
            //var phone = new SIM("07725917672");

            // create an instance of HttpClientHandler with cookie support
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            handler.UseCookies = true;
            handler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(handler); // pass the HttpClientHandler to HttpClient

            LogicMethods.SetDefaultRequestHeaders(client);

            try
            {
                // store the call for the first GET request
                Uri firstLocationRedirect = await LogicMethods.SendFirstGetRequest(client, baseUri);
                Uri secondLocationRedirect = null;
                Uri thirdLocationRedirect = null;

                if (firstLocationRedirect != null)
                {
                    // store the call for the first redirect GET request
                    secondLocationRedirect = await LogicMethods.SendFirstRedirectGetRequest(client, baseUri, firstLocationRedirect);
                }

                if (secondLocationRedirect != null)
                {
                    thirdLocationRedirect = await LogicMethods.SendSecondRedirectGetRequest(client, secondLocationRedirect);
                }

                string responseBody = await LogicMethods.SendLoginGetRequest(client, thirdLocationRedirect);

                //uri = await LogicMethods.SendInitialGetRequest(client, baseUri, uri);

                //responseBody = await LogicMethods.SendLoginGetRequest(client, uri);

                string csrf_token = LogicMethods.ExtractCsrfToken(client, responseBody);

                string tx = LogicMethods.ExtractTxValue(responseBody);

                pageViewId = LogicMethods.ExtractPageViewIdValue(responseBody);

                diags = $"{{\"pageViewId\":\"{pageViewId}\",\"pageId\":\"{pageId}\",\"trace\":{trace}}}";

                //Console.WriteLine(diags);  

                // URL-encode the diags string
                string urlEncodedDiags = HttpUtility.UrlEncode(diags);

                //Console.WriteLine(urlEncodedDiags);

                string confirmedUriQueries = $"{confirmedUri}?rememberMe={rememberMe}&csrf_token={csrf_token}&tx={tx}&p={p}&diags={urlEncodedDiags}";

                Uri thirdLocationHeader = await LogicMethods.SendSecondRedirectGetRequest(client, secondLocationRedirect);

                HttpResponseMessage firstPostResponse = await LogicMethods.SendFirstPostRequest(client, loginUri, tx, p, request_type, signInName, thirdLocationHeader);

                await LogicMethods.HandleFirstPostResponse(firstPostResponse);

                responseBody = await LogicMethods.SendSecondGetRequest(client, confirmedUriQueries, thirdLocationHeader);

                csrf_token = LogicMethods.ExtractCsrfToken(client, responseBody);

                pageViewId = LogicMethods.ExtractPageViewIdValue(responseBody);

                diags = $"{{\"pageViewId\":\"{pageViewId}\",\"pageId\":\"{pageId}\",\"trace\":{trace}}}";

                //Console.WriteLine(diags);  

                // URL-encode the diags string
                urlEncodedDiags = HttpUtility.UrlEncode(diags);

                //Console.WriteLine(urlEncodedDiags);

                confirmedUriQueries = $"{confirmedUri}?rememberMe={rememberMe}&csrf_token={csrf_token}&tx={tx}&p={p}&diags={urlEncodedDiags}";

                HttpResponseMessage secondPostResponse = await LogicMethods.SendSecondPostRequest(client, loginUri, tx, p, request_type, signInName, password, confirmedUriQueries);

                await LogicMethods.HandleSecondPostResponse(secondPostResponse);

                await LogicMethods.SendThirdGetRequest(client, confirmedUriQueries, confirmedUriQueries);

                //TODO: additional LocationRedirect method for dashboard pages required by first doing the request POST /v1/identity/authorize/login

                await LogicMethods.SendDashboardGetRequest(client, dashboardUri);

                await LogicMethods.SendDashboardRedirectGetRequest(client, dashboardRedirectUri);

                await LogicMethods.SendOpenidGetRequest(client, openidUri);

                await LogicMethods.SendUseridGetRequest(client, useridUri);

                await LogicMethods.SendIdentityGetRequest(client, identityUri);

                await LogicMethods.SendUsageDataGetRequest(client, usageDataUri);

                await LogicMethods.SendUserDataGetRequest(client, userDataUri);

                await LogicMethods.SendDataGiftGetRequest(client, dataGiftUri);
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine("\nHttpRequestException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nAn unexpected exception occurred!");
                Console.WriteLine("Message :{0} ", ex.Message);
            }
        }
    }
}