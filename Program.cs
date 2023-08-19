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
            var uri = $"https://auth.ee.co.uk/e2ea8fbf-98c0-4cf1-a2df-ee9d55ef69c3/B2C_1A_RPBT_SignUpSignIn/oauth2/v2.0/authorize?client_id=d353847f-d273-43b1-b605-aed025068daf&nonce=defaultNonce&redirect_uri=https://api.ee.co.uk/v1/identity/authorize/login&scope=openid%20d353847f-d273-43b1-b605-aed025068daf&response_mode=form_post&response_type=code&state=7a0e14ad-571c-45de-a480-b40ac156521f&requestId=7a0e14ad-571c-45de-a480-b40ac156521f";
            var p = "B2C_1A_RPBT_SignUpSignIn/";
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

                    // handle the response content
                    var stringContent = await postResponse.Content.ReadAsStringAsync();

                    // check if the POST request was successful
                    if (postResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine("POST request was successful\n");

                        // check if the response URL matches the successful login redirect URL
                        if (postResponse.RequestMessage.RequestUri.AbsoluteUri == "https://ee.co.uk/exp/home")
                        {
                            Console.WriteLine("Login successful!");
                            Console.WriteLine(stringContent);
                        }

                        else
                        {
                            Console.WriteLine("Login was not successful. Check credentials or process.");
                            Console.WriteLine(stringContent);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"POST request failed with status code: {postResponse.StatusCode}\n");
                        Console.WriteLine(stringContent);
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