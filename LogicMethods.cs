using System;
using System.Net.Http.Headers;

namespace DataGifting
{
    public class LogicMethods
    {
        /// <summary>
        /// set the headers
        /// </summary>
        /// <param name="client"></param>
        public static void SetDefaultRequestHeaders(HttpClient client)
        {
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
        }

        /// <summary>
        /// creates the initial GET request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="baseUri"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<string> SendInitialGetRequest(HttpClient client, string baseUri, string uri)
        {
            // send a GET request to retrieve the initial page
            HttpResponseMessage initialResponse = await client.GetAsync(baseUri);

            if (initialResponse.IsSuccessStatusCode)
            {
                //Console.WriteLine($"initial URL = {baseUri}");
                Console.WriteLine("initial GET request was successful\n");

                // extract the new URL from the response
                uri = initialResponse.RequestMessage.RequestUri.ToString();
                //Console.WriteLine($"uri = {uri}");
                return uri;
            }
            else
            {
                Console.WriteLine($"\ninitial GET request failed with status code: {initialResponse.StatusCode}");
                string responseContent = await initialResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{responseContent}");
            }
            return null;
        }

        /// <summary>
        /// creates the login page GET request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri"></param>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        public static async Task<string> SendLoginGetRequest(HttpClient client, string uri, string responseBody)
        {
            // send a GET request to retrieve the login page
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                //Console.WriteLine($"login page URL = {responseBody}");
                Console.WriteLine("login page GET request was successful\n");

                // retrieves the response body as a string
                responseBody = await client.GetStringAsync(uri);
                return responseBody;

                //Console.WriteLine($"RESPONSE BODY: {responseBody}\n");
            }
            else
            {
                Console.WriteLine($"\nlogin page GET request failed with status code: {response.StatusCode}");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{responseContent}");
            }
            return null;
        }

        /// <summary>
        /// extracts the CSRF token value from the response and assigns it to csrf string
        /// </summary>
        /// <param name="client"></param>
        /// <param name="responseBody"></param>
        public static void ExtractCsrfToken(HttpClient client, string responseBody)
        {
            string csrfPrefix = "\"csrf\":\"";

            // find the start of the CSRF token in the response body string
            int csrfStartIndex = responseBody.IndexOf(csrfPrefix);

            if (csrfStartIndex != -1)
            {
                csrfStartIndex += csrfPrefix.Length; // move past the prefix

                // find the quotation mark character at the end of the CSRF token in the response body string
                int csrfEndIndex = responseBody.IndexOf('"', csrfStartIndex);

                if (csrfEndIndex != -1)
                {
                    // extract the substring containing the CSRF token value
                    string csrf = responseBody.Substring(csrfStartIndex, csrfEndIndex - csrfStartIndex);

                    // assign CSRF token to the X-CSRF-TOKEN header
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
        }

        /// <summary>
        /// extracts the tx value from the response and assigns it to tx string
        /// </summary>
        /// <param name="responseBody"></param>
        /// <param name="tx"></param>
        public static void ExtractTxValue(string responseBody, ref string tx)
        {
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
        }

        /// <summary>
        /// creates the POST request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="loginUri"></param>
        /// <param name="tx"></param>
        /// <param name="p"></param>
        /// <param name="request_type"></param>
        /// <param name="signInName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendPostRequest(HttpClient client, string loginUri, string tx, string p, string request_type, string signInName, string password)
        {
            // set the POST request header
            var postRequestURL = loginUri + $"?tx={tx}&p={p}";
            //Console.Write($"POST request header = {postRequestURL}");

            // simulate form data for the POST request
            var formContent = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("request_type", request_type),
                    new KeyValuePair<string, string>("signInName", signInName),
                    new KeyValuePair<string, string>("password", password)
                    });

            // modify the Content-Type header of the formContent to include the charset
            formContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded")
            {
                CharSet = "UTF-8"
            };

            // send a POST request with the query parameters and form data
            var postResponse = await client.PostAsync(postRequestURL, formContent);

            // handle the response content
            var stringContent = await postResponse.Content.ReadAsStringAsync();

            return await client.PostAsync(postRequestURL, formContent);
        }

        /// <summary>
        /// sets the POST response handling
        /// </summary>
        /// <param name="postResponse"></param>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public static async Task HandlePostResponse(HttpResponseMessage postResponse, string redirectUri)
        {
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
                    Console.WriteLine("Login was not successful. Check credentials or process.\n");
                    //Console.WriteLine(stringContent);
                }
            }
            else
            {
                Console.WriteLine($"POST request failed with status code: {postResponse.StatusCode}\n");
                Console.WriteLine(postResponse);
            }

            var postResponseContent = await postResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content:{postResponseContent}\n");
        }
    }
}