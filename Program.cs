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
            string responseBody = null;
            var loginUri = "https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/B2C_1A_RPBT_SignUpSignIn/SelfAsserted";
            var tx = "";
            var p = "B2C_1A_RPBT_SignUpSignIn";
            var request_type = "RESPONSE";
            var signInName = "sameer99@outlook.com";
            var password = "D@tagifting2113";
            var redirectUri = "https://ee.co.uk/exp/home";
            var sim = new SIM("361308296409");
            //var phone = new SIM("07725917672");

            // create an instance of HttpClientHandler with cookie support
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            handler.UseCookies = true;

            // pass the HttpClientHandler to HttpClient
            HttpClient client = new HttpClient(handler);

            // set the headers
            client.BaseAddress = new Uri("https://auth.ee.co.uk");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/117.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.5");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            client.DefaultRequestHeaders.Add("Origin", "https://auth.ee.co.uk");
            client.DefaultRequestHeaders.Add("DNT", "1");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");

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
                    responseBody = await client.GetStringAsync(uri);

                    Console.WriteLine($"RESPONSE BODY: {responseBody}\n");
                }
                else
                {
                    Console.WriteLine($"\nGET request failed with status code: {response.StatusCode}");
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response Content:\n{responseContent}");
                }

                // Define the prefix to look for
                string csrfPrefix = "\"csrf\":\"";

                    // Find the start and end indexes of the CSRF token
                    int csrfStartIndex = responseBody.IndexOf(csrfPrefix);

                    if (csrfStartIndex != -1)
                    {
                        csrfStartIndex += csrfPrefix.Length; // Move past the prefix

                        int csrfEndIndex = responseBody.IndexOf('"', csrfStartIndex);

                        if (csrfEndIndex != -1)
                        {
                            // Extract the substring containing the CSRF token value
                            string csrf = responseBody.Substring(csrfStartIndex, csrfEndIndex - csrfStartIndex);

                            // Assign csrf token to the X-CSRF-TOKEN header
                            client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", csrf);
                            //Console.WriteLine($"CSRF VALUE = {csrf}\n");
                        }
                        else
                        {
                            Console.WriteLine("Failed to extract CSRF token value.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("CSRF token prefix was not found in the response body.");
                    }

                    // find the start of StateProperties in the response body string
                    int txIndex = responseBody.IndexOf("StateProperties=");

                    if (txIndex != -1)
                    {
                        // find the quotation mark character at the end of StateProperties in the response body string
                        int txEndIndex = responseBody.IndexOf('"', txIndex);

                        // extract the substring containing the StateProperties value
                        string stateProperties = responseBody.Substring(txIndex, txEndIndex - txIndex);

                        // assign StateProperties to the tx variable
                        tx = stateProperties;
                        //Console.WriteLine($"TX VALUE = {tx}\n");
                    }
                    else
                    {
                        // handle the case where "StateProperties=" is not found in the response
                        Console.WriteLine("StateProperties was not found in the response body.");
                    }

                    var postRequestURL = loginUri + $"?tx={tx}&p={p}";
                    Console.Write("POST request header = ");
                    Console.Write($"{postRequestURL}\n");

                    // simulate form data for the POST request
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("request_type", request_type),
                    new KeyValuePair<string, string>("signInName", signInName),
                    new KeyValuePair<string, string>("password", password)
                    });

                    // Modify the Content-Type header of the formContent to include the charset
                    formContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded")
                    {
                        CharSet = "UTF-8"
                    };

                    // send a POST request with the query parameters and form data
                    var postResponse = await client.PostAsync(postRequestURL, formContent);

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
                        Console.WriteLine(postResponse);
                    }

                    var postResponseContent = await postResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"\nResponse Content:{postResponseContent}\n");
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