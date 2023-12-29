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
        static async Task Main(string[] args)
        {
            string baseUri = "https://id.ee.co.uk/";
            //string uri = null;
            string confirmedUri = "https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/B2C_1A_RPBT_SignUpSignIn/api/CombinedSigninAndSignup/confirmed";
            string firstSignUpUri = "https://auth.ee.co.uk/static/content/stage-2/SignUpSignIn/SignUpSignInPage1/unified.html";
            string secondSignUpUri = "https://auth.ee.co.uk/static/content/stage-2/SignUpSignIn/SignUpSignInPage2/unified.html";
            string loginUri = "https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/B2C_1A_RPBT_SignUpSignIn/SelfAsserted";
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
            string signInName = "";
            string password = "";
            string slice = "001-000";
            string dc = "DB3";
            string rememberMe = "false";
            string pageViewId = null;
            string pageId = "CombinedSigninAndSignup";
            string trace = "[]";
            string diags = "";

            // create an instance of HttpClientHandler with cookie support
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            handler.UseCookies = true;
            handler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(handler); // pass the HttpClientHandler to HttpClient

            //LogicMethods.SetDefaultPOSTRequestHeaders(client);

            try
            {
                LogicMethods.SetFirstGETRequestHeaders(client);

                // store the call for the first GET request
                Uri firstLocationRedirect = await LogicMethods.SendFirstGetRequest(client, baseUri);
                Uri secondLocationRedirect = null;
                Uri thirdLocationRedirect = null;

                LogicMethods.RemoveGETRequestHeaders(client);

                if (firstLocationRedirect != null)
                {
                    LogicMethods.SetFirstRedirectGETRequestHeaders(client);

                    // store the call for the first redirect GET request
                    secondLocationRedirect = await LogicMethods.SendFirstRedirectGetRequest(client, baseUri, firstLocationRedirect);

                    LogicMethods.RemoveGETRequestHeaders(client);
                }

                if (secondLocationRedirect != null)
                {
                    LogicMethods.SetSecondRedirectGETRequestHeaders(client);

                    // store the call for the second redirect GET request
                    thirdLocationRedirect = await LogicMethods.SendSecondRedirectGetRequest(client, secondLocationRedirect);

                    LogicMethods.RemoveGETRequestHeaders(client);
                }

                LogicMethods.SetLoginGETRequestHeaders(client);

                string responseBody = await LogicMethods.SendLoginGetRequest(client, thirdLocationRedirect);

                LogicMethods.RemoveGETRequestHeaders(client);

                //uri = await LogicMethods.SendInitialGetRequest(client, baseUri, uri);

                //responseBody = await LogicMethods.SendLoginGetRequest(client, uri);

                string clientId = LogicMethods.ExtractClientIdValue(responseBody);

                string signupUriQueries = $"{firstSignUpUri}?clientid={clientId}&slice={slice}&dc={dc}";

                string csrf_token = LogicMethods.ExtractCsrfToken(client, responseBody);

                //Console.WriteLine($"\n{csrf_token}\n");

                string tx = LogicMethods.ExtractTxValue(responseBody);

                pageViewId = LogicMethods.ExtractPageViewIdValue(responseBody);

                diags = $"{{\"pageViewId\":\"{pageViewId}\",\"pageId\":\"{pageId}\",\"trace\":{trace}}}";

                //Console.WriteLine(diags);  

                // URL-encode the diags string
                string urlEncodedDiags = HttpUtility.UrlEncode(diags);

                //Console.WriteLine(urlEncodedDiags);

                string confirmedUriQueries = $"{confirmedUri}?rememberMe={rememberMe}&csrf_token={csrf_token}&tx={tx}&p={p}&diags={urlEncodedDiags}";

                LogicMethods.SetFirstStaticPageGETRequestHeaders(client);

                await LogicMethods.SendFirstStaticPageGetRequest(client, signupUriQueries, thirdLocationRedirect);

                Uri thirdLocationHeader = await LogicMethods.SendSecondRedirectGetRequest(client, secondLocationRedirect);

                LogicMethods.RemoveGETRequestHeaders(client);

                LogicMethods.SetDefaultPOSTRequestHeaders(client);

                HttpResponseMessage firstPostResponse = await LogicMethods.SendFirstPostRequest(client, loginUri, tx, p, request_type, signInName, thirdLocationHeader);

                await LogicMethods.HandleFirstPostResponse(firstPostResponse);

                LogicMethods.RemovePOSTRequestHeaders(client);

                LogicMethods.SetSecondGETRequestHeaders(client);

                responseBody = await LogicMethods.SendSecondGetRequest(client, confirmedUriQueries, thirdLocationHeader);

                LogicMethods.RemoveGETRequestHeaders(client);

                csrf_token = LogicMethods.ExtractCsrfToken(client, responseBody);

                //Console.WriteLine($"\n{csrf_token}\n");

                pageViewId = LogicMethods.ExtractPageViewIdValue(responseBody);

                diags = $"{{\"pageViewId\":\"{pageViewId}\",\"pageId\":\"{pageId}\",\"trace\":{trace}}}";

                //Console.WriteLine(diags);  

                // URL-encode the diags string
                urlEncodedDiags = HttpUtility.UrlEncode(diags);

                //Console.WriteLine(urlEncodedDiags);

                signupUriQueries = $"{secondSignUpUri}?clientid={clientId}&slice={slice}&dc={dc}";

                confirmedUriQueries = $"{confirmedUri}?rememberMe={rememberMe}&csrf_token={csrf_token}&tx={tx}&p={p}&diags={urlEncodedDiags}";

                LogicMethods.SetSecondStaticPageGETRequestHeaders(client);

                await LogicMethods.SendSecondStaticPageGetRequest(client, signupUriQueries, confirmedUriQueries);

                LogicMethods.RemoveGETRequestHeaders(client);

                LogicMethods.SetDefaultPOSTRequestHeaders(client);

                HttpResponseMessage secondPostResponse = await LogicMethods.SendSecondPostRequest(client, loginUri, tx, p, request_type, signInName, password, confirmedUriQueries);

                await LogicMethods.HandleSecondPostResponse(secondPostResponse);

                LogicMethods.RemovePOSTRequestHeaders(client);

                LogicMethods.SetDefaultGETRequestHeaders(client);

                await LogicMethods.SendFourthGetRequest(client, confirmedUriQueries, confirmedUriQueries);

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