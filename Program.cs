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
            var baseUri = "https://id.ee.co.uk/";
            var uri = "";
            var loginUri = "https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/B2C_1A_RPBT_SignUpSignIn/SelfAsserted";
            var tx = "";
            var p = "B2C_1A_RPBT_SignUpSignIn";
            var request_type = "RESPONSE";
            var signInName = "sameer99%40outlook.com";
            var password = "D%40tagifting2113";
            var redirectUri = "https://ee.co.uk/exp/home";
            var sim = new SIM("361308296409");
            //var phone = new SIM("07725917672");

            // create an instance of HttpClientHandler with cookie support
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            // pass the HttpClientHandler to HttpClient
            HttpClient client = new HttpClient(handler);

            try
            {
                // send a GET request to retrieve the initial page
                HttpResponseMessage initialResponse = await client.GetAsync(baseUri);

                if (initialResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"initial URL = {baseUri}"); 
                    Console.WriteLine("\nGET request was successful\n");

                    // extract the new URL from the response
                    uri = initialResponse.RequestMessage.RequestUri.ToString();
                }

                // send a GET request to retrieve the redirect page
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"login page URL = {uri}"); 
                    Console.WriteLine("\nGET request was successful\n");

                    // retrieves the response body as a string
                    string responseBody = await client.GetStringAsync(uri);

                    //Console.WriteLine($"RESPONSE BODY: {responseBody}\n");

                    // find the start of StateProperties in the response body string
                    int startIndex = responseBody.IndexOf("StateProperties=");


                    if (startIndex != -1)
                    {
                        // find the quotation mark character at the end of StateProperties in the response body string
                        int endIndex = responseBody.IndexOf('"', startIndex);

                        // extract the substring containing the StateProperties value
                        string stateProperties = responseBody.Substring(startIndex, endIndex - startIndex);

                        // assign StateProperties to the tx variable
                        tx = stateProperties;
                        //Console.WriteLine($"TX VALUE = {tx}\n");
                    }
                    else
                    {
                        // handle the case where "StateProperties=" is not found in the response
                        Console.WriteLine("StateProperties was not found in the response body.");
                    }

                    Console.Write("\nPOST request header = ");
                    Console.Write(loginUri + $"?tx={tx}&p={p}\n");

                    // simulate form data for the POST request
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("request_type", request_type),
                    new KeyValuePair<string, string>("signInName", signInName),
                    new KeyValuePair<string, string>("password", password)
                    });

                    // send a POST request with the query parameters and form data
                    var postResponse = await client.PostAsync(loginUri + $"?tx={tx}&p={p}", formContent);

                    // handle the response content
                    var stringContent = await postResponse.Content.ReadAsStringAsync();

                    // check if the POST request was successful
                    if (postResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"\nPOST request was successful: {postResponse.StatusCode}\n");

                        // check if the response URL matches the successful login URL
                        if (postResponse.RequestMessage.RequestUri.AbsoluteUri == redirectUri)
                        {
                            Console.WriteLine("\nLogin successful!");
                            // Console.WriteLine(stringContent);
                        }

                        else
                        {
                            Console.WriteLine("\nLogin was not successful. Check credentials or process.\n");
                            //Console.WriteLine(stringContent);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nPOST request failed with status code: {postResponse.StatusCode}\n");
                        //Console.WriteLine(stringContent);
                    }

                    var postResponseContent = await postResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"\nResponse Content:{postResponseContent}\n");
                }
                else
                {
                    Console.WriteLine($"\nGET request failed with status code: {response.StatusCode}");
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response Content:\n{responseContent}");
                }
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
