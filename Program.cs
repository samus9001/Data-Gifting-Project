using System;
using System.Net.Http;
using System.Xml.Linq;

namespace DataGifting
{
    internal class Program
    {
        //const string MB_UNITS = "MB";
        //const string GB_UNITS = "GB";

        //static void Main(string[] args)
        //{
        //    //SIM sim1 = new SIM();
        //    //sim1.PhoneNumber = "07725917672";
        //    //sim1.SerialNumber = 8944303613082964096;
        //}


            //TODO: look into httpclient class
            //TODO: try to recreate login sequence (enable cookies :) )
        public static HttpClient client = new HttpClient();

        static async Task Main()
        {
            var uri = "http://id.ee.co.uk/";
            var signInName = "sameer99%40outlook.com";
            var password = "D%40tagifting2113";

            // Create an instance of HttpClientHandler with cookie support
            var handler = new HttpClientHandler
            {
                UseCookies = true
            };

            // Pass the HttpClientHandler to HttpClient
            client = new HttpClient(handler);

            //enable manual cookies for specific site (note sure if needed?)
            //var baseAddress = new Uri("http://id.ee.co.uk");
            //using (var handler = new HttpClientHandler { UseCookies = false })
            //using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            //{
            //    var message = new HttpRequestMessage(HttpMethod.Get, "/test");
            //    message.Headers.Add("Cookie", "cookie1=value1; cookie2=value2");
            //    var result = await client.SendAsync(message);
            //    result.EnsureSuccessStatusCode();
            //}

            //retrieves the entire HTTP response
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

            //retrieves the response body as a string
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

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("signInName", signInName),
                new KeyValuePair<string, string>("password", password)
            });

            var HttpClient = new HttpClient();
            var response = await HttpClient.PostAsync(uri.ToString(), formContent);

            // Check if the POST request was successful
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("POST request was successful!\n");
            }
            else
            {
                Console.WriteLine($"POST request failed with status code: {response.StatusCode}\n");
            }

            var stringContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(stringContent);
        }
    }
}