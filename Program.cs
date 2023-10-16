using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;

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
            string tx = "";
            string p = "B2C_1A_RPBT_SignUpSignIn";
            string request_type = "RESPONSE";
            string signInName = "sameer99@outlook.com";
            string password = "D@tagifting2113";
            string redirectUri = "https://ee.co.uk/exp/home/";
            string authEndpointUri = "https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/b2c_1a_rpbt_signupsignin/v2.0/.well-known/openid-configuration";
            //string authenticationUri = "https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/b2c_1a_rpbt_signupsignin/oauth2/v2.0/token/";
            string userDataUri = "https://ee.co.uk/app/api/basic";
            //var sim = new SIM("361308296409");
            //var phone = new SIM("07725917672");

            // create an instance of HttpClientHandler with cookie support
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            handler.UseCookies = true;
            HttpClient client = new HttpClient(handler); // pass the HttpClientHandler to HttpClient

            LogicMethods.SetDefaultRequestHeaders(client);

            try
            {
                uri = await LogicMethods.SendInitialGetRequest(client, baseUri, uri);

                responseBody = await LogicMethods.SendLoginGetRequest(client, uri);

                LogicMethods.ExtractCsrfToken(client, responseBody);

                LogicMethods.ExtractTxValue(responseBody, ref tx);

                HttpResponseMessage postResponse = await LogicMethods.SendInitialPostRequest(client, loginUri, tx, p, request_type, signInName, password);

                await LogicMethods.HandleInitialPostResponse(postResponse);

                await LogicMethods.SendDashboardGetRequest(client, redirectUri);

                //await LogicMethods.SendAuthEndpointGetRequest(client, authEndpointUri);

                await LogicMethods.SendUserDataGetRequest(client, userDataUri);

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