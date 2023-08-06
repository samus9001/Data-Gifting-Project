using System.Net.Http;

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

        //    //TODO: look into httpclient class
        //    //TODO: try to recreate login sequence (enable cookies :) )

        public static HttpClient client = new HttpClient();

        static async Task Main()
        {
            //retrieves the entire HTTP response
            try
            {
                var uri = "http://id.ee.co.uk/";
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("HTTP RESPONSE:\n");
                    Console.WriteLine(response);
                }
                else
                {
                    Console.WriteLine("\nException Caught!");
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

            Console.WriteLine("\n");

            //retrieves the response body as a string
            try
            {
                string responseBody = await client.GetStringAsync("http://id.ee.co.uk/");
                Console.WriteLine("RESPONSE BODY:\n");
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}