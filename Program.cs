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
            string uri = "";
            string responseBody = null;
            string loginUri = "https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/B2C_1A_RPBT_SignUpSignIn/SelfAsserted/";
            string dashboardUri = "https://id.ee.co.uk/id/dashboard";
            string redirectUri = "https://ee.co.uk/exp/home/";
            string openidUri = "https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/b2c_1a_rpbt_signupsignin/v2.0/.well-known/openid-configuration";
            string useridUri = "https://auth.ee.co.uk/common/v1/customer-identity-profiles?identity-id=2dcfe49c-81a0-4581-8662-7735017ea90b";
            string identityUri = "https://api.ee.co.uk/digital/v1/person-identities/self";
            string usageDataUri = "https://ee.co.uk/plans-subscriptions/mobile/usage-since-last-bill";
            string userDataUri = "https://ee.co.uk/app/api/basic";
            string dataGiftUri = "https://ee.co.uk/plans-subscriptions/mobile/data-gifting";

            string tx = "";
            string p = "B2C_1A_RPBT_SignUpSignIn";
            string request_type = "RESPONSE";
            string signInName = "sameer99@outlook.com";
            string password = "D@tagifting2113";
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
                Uri firstLocationRedirect = await LogicMethods.SendFirstGetRequest(client, baseUri, uri);
                Uri secondLocationRedirect = null;

                if (firstLocationRedirect != null)
                {
                    // store the call for the first redirect GET request
                    secondLocationRedirect = await LogicMethods.SendFirstRedirectGetRequest(client, baseUri, firstLocationRedirect);
                }

                if (secondLocationRedirect != null)
                {
                    Uri login = await LogicMethods.SendSecondRedirectGetRequest(client, secondLocationRedirect);
                }

                //uri = await LogicMethods.SendInitialGetRequest(client, baseUri, uri);

                //responseBody = await LogicMethods.SendLoginGetRequest(client, uri);

                LogicMethods.ExtractCsrfToken(client, responseBody);

                LogicMethods.ExtractTxValue(responseBody, ref tx);

                HttpResponseMessage postResponse = await LogicMethods.SendInitialPostRequest(client, loginUri, tx, p, request_type, signInName, password);

                await LogicMethods.HandleInitialPostResponse(postResponse);

                await LogicMethods.SendDashboardGetRequest(client, dashboardUri, redirectUri);

                await LogicMethods.SendOpenidGetRequest(client, openidUri);

                await LogicMethods.SendUseridGetRequest(client, useridUri);

                await LogicMethods.SendIdentityGetRequest(client, identityUri);

                await LogicMethods.SendUsageDataGetRequest(client, usageDataUri);

                await LogicMethods.SendUserDataGetRequest(client, userDataUri);

                //await LogicMethods.SendDataGiftGetRequest(client, dataGiftUri);
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