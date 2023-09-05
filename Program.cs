using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
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

        public static HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            var baseUri = "https://id.ee.co.uk/";
            var uri = $"https://auth.ee.co.uk/";
            var tx = "";
            var p = "B2C_1A_RPBT_SignUpSignIn";
            var request_type = "RESPONSE";
            var signInName = "sameer99%40outlook.com";
            var password = "D%40tagifting2113";
            var sim = new SIM("361308296409");
            //var phone = new SIM("07725917672");

            // create an instance of HttpClientHandler with cookie support
            var handler = new HttpClientHandler
            {
                UseCookies = true
            };

            // pass the HttpClientHandler to HttpClient
            client = new HttpClient(handler);

            try
            {
                // send a GET request to retrieve the initial page
                HttpResponseMessage response = await client.GetAsync(baseUri);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("GET request was successful\n");

                    // simulate form data for the POST request
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("request_type", request_type),
                    new KeyValuePair<string, string>("signInName", signInName),
                    new KeyValuePair<string, string>("password", password)
                });

                    // send a POST request
                    var postResponse = await client.PostAsync(uri, formContent);

                    // retrieves the response body as a string
                    string responseBody = await client.GetStringAsync("http://id.ee.co.uk/");

                    //Console.WriteLine("RESPONSE BODY:\n");
                    //Console.WriteLine(responseBody);

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
                    }
                    else
                    {
                        // handle the case where "StateProperties=" is not found in the response
                        Console.WriteLine("StateProperties was not found in the response body.");
                    }

                    //Console.WriteLine($"TX VALUE = {tx}");

                    postResponse = await client.PostAsync(uri, formContent);

                    // handle the response content
                    var stringContent = await postResponse.Content.ReadAsStringAsync();

                    // check if the POST request was successful
                    if (postResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"POST request was successful: {postResponse.StatusCode}\n");

                        // check if the response URL matches the successful login URL
                        if (postResponse.RequestMessage.RequestUri.AbsoluteUri == "https://ee.co.uk/exp/home")
                        {
                            Console.WriteLine("Login successful!");
                           // Console.WriteLine(stringContent);
                        }

                        else
                        {
                            Console.WriteLine("Login was not successful. Check credentials or process.\n");
                            //Console.WriteLine(stringContent);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"POST request failed with status code: {postResponse.StatusCode}\n");
                        //Console.WriteLine(stringContent);
                    }
                }
                else
                {
                    Console.WriteLine($"GET request failed with status code: {response.StatusCode}");
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

            // retrieves the entire HTTP response
            //try
            //{
            //    var uri = "http://id.ee.co.uk/";
            //    var response = await client.GetAsync(uri);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        Console.WriteLine("HTTP RESPONSE:\n");
            //        Console.WriteLine(response);
            //    }
            //    else
            //    {
            //        Console.WriteLine("\nException Caught!");
            //    }
            //}
            //catch (HttpRequestException e)
            //{
            //    Console.WriteLine("\nHttpRequestException Caught!");
            //    Console.WriteLine("Message :{0} ", e.Message);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("\nAn unexpected exception occurred!");
            //    Console.WriteLine("Message :{0} ", ex.Message);
            //}

            //Console.WriteLine("\n");

            // retrieves the response body as a string
            //try
            //{
            //    string responseBody = await client.GetStringAsync("http://id.ee.co.uk/");
            //    Console.WriteLine("RESPONSE BODY:\n");
            //    Console.WriteLine(responseBody);
            //}
            //catch (HttpRequestException e)
            //{
            //    Console.WriteLine("\nException Caught!");
            //    Console.WriteLine("Message :{0} ", e.Message);
            //}
        }
    }
}
