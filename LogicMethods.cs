using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

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
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/118.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
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
        /// sets the first GET request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="baseUri"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<Uri> SendFirstGetRequest(HttpClient client, string baseUri, string uri)
        {
            // send a GET request to retrieve the first page
            HttpResponseMessage response = await client.GetAsync(baseUri);
            Console.WriteLine($"first GET request URI = {baseUri}");

            // handle the case when the GET request is successful
            if (response.StatusCode == HttpStatusCode.Found)
            {
                Console.WriteLine("\nfirst GET request was successful");
            }
            else
            {
                // handle the case when the GET request is not successful
                Console.WriteLine($"initial GET request failed with status code: {response.StatusCode}");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{responseContent}");
                return null;
            }

            // store the first response Location header value
            var firstLocationHeader = response.Headers.Location;

            return firstLocationHeader;
        }

        /// <summary>
        /// sets the first redirect GET request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="baseUri"></param>
        /// <param name="locationHeader"></param>
        /// <returns></returns>
        public static async Task<Uri> SendFirstRedirectGetRequest(HttpClient client, string baseUri, Uri firstLocationHeader)
        {
            // send a GET request to retrieve the first redirect page
            HttpResponseMessage response = await client.GetAsync($"{baseUri}{firstLocationHeader}");

            Console.WriteLine($"\n\nfirst redirect GET request URI = {baseUri}{firstLocationHeader}");

            // handle the case when the GET request is successful
            if (response.StatusCode == HttpStatusCode.Found)
            {
                Console.WriteLine("\nfirst redirect GET request was successful");

                // return the uri from the first redirect
                return response.Headers.Location;
            }
            else
            {
                // handle the case when the GET request is not successful
                Console.WriteLine($"first redirect GET request failed with status code: {response.StatusCode}");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{responseContent}");
                return null;
            }
        }

        /// <summary>
        /// sets the second redirect GET request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="secondLocationRedirect"></param>
        /// <returns></returns>
        public static async Task<Uri> SendSecondRedirectGetRequest(HttpClient client, Uri secondLocationRedirect)
        {
            // send a GET request to retrieve the second redirect page
            HttpResponseMessage response = await client.GetAsync(secondLocationRedirect);

            // store the second response Location header value
            var secondLocationHeader = response.Headers.Location;

            Console.WriteLine($"\n\nsecond redirect GET request URI = {secondLocationHeader}");

            if (secondLocationHeader != null)
            {
                Console.WriteLine("\nsecond redirect GET request was successful");
            }

            return secondLocationHeader;
        }

        // Parse the query string
        //NameValueCollection queryParameters = HttpUtility.ParseQueryString(finalLoc.Query);


        //    if (initialResponse.IsSuccessStatusCode)
        //    {
        //        //Console.WriteLine($"initial URL = {baseUri}");
        //        Console.WriteLine("initial GET request was successful\n");

        //        // extract the new URL from the response
        //        uri = initialResponse.RequestMessage.RequestUri.ToString();
        //        //Console.WriteLine($"uri = {uri}");
        //        return uri;
        //    }
        //    else
        //    {
        //        Console.WriteLine($"\ninitial GET request failed with status code: {initialResponse.StatusCode}");
        //        string responseContent = await initialResponse.Content.ReadAsStringAsync();
        //        Console.WriteLine($"Response Content:\n{responseContent}");
        //    }
        //    return null;
        //}

        /// <summary>
        /// sets the login page GET request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri"></param>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        public static async Task<string> SendLoginGetRequest(HttpClient client, string uri)
        {
            // send a GET request to retrieve the login page
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                //Console.WriteLine($"login page URL = {responseBody}");
                Console.WriteLine("login page GET request was successful\n");

                // retrieves the response body as a string
                string responseBody = await client.GetStringAsync(uri);
                //Console.WriteLine($"RESPONSE BODY: {responseBody}\n");

                return responseBody;
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

        //public static async Task<HttpResponseMessage> SendPreInitialPostRequest(HttpClient client, string loginUri, string tx, string p)
        //{
        //    // set the POST request header
        //    var initialPostRequestURL = loginUri + $"?tx={tx}&p={p}";
        //    Console.Write($"POST request header = {initialPostRequestURL}");

        //    // Create an empty request content (no data in the request body)
        //    var emptyContent = new StringContent(string.Empty);

        //    // Send the POST request with empty content and receive the response
        //    HttpResponseMessage postResponse = await client.PostAsync(initialPostRequestURL, emptyContent);

        //    return postResponse;
        //}

        ///// <summary>
        ///// sets the initial POST request response handling
        ///// </summary>
        ///// <param name="postResponse"></param>
        ///// <param name="redirectUri"></param>
        ///// <returns></returns>
        //public static async Task HandleInitialPostResponse(HttpResponseMessage postResponse)
        //{
        //    // check if the POST request was successful
        //    if (postResponse.IsSuccessStatusCode)
        //    {
        //        Console.WriteLine($"\nPOST request was successful: {postResponse.StatusCode}\n");

        //        // handle the response content
        //        var responseContent = await postResponse.Content.ReadAsStringAsync();

        //        Console.WriteLine($"Response Content:\n{responseContent}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"POST request failed with status code: {postResponse.StatusCode}\n");
        //        Console.WriteLine(postResponse);
        //    }

        //    var postResponseContent = await postResponse.Content.ReadAsStringAsync();
        //    Console.WriteLine($"Response Content:{postResponseContent}\n");
        //}

        /// <summary>
        /// sets the initial POST request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="loginUri"></param>
        /// <param name="tx"></param>
        /// <param name="p"></param>
        /// <param name="request_type"></param>
        /// <param name="signInName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendInitialPostRequest(HttpClient client, string loginUri, string tx, string p, string request_type, string signInName, string password)
        {
            // set the POST request header
            var initialPostRequestURL = loginUri + $"?tx={tx}&p={p}";
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
            var postResponse = await client.PostAsync(initialPostRequestURL, formContent);

            // handle the response content
            var stringContent = await postResponse.Content.ReadAsStringAsync();

            return await client.PostAsync(initialPostRequestURL, formContent);
        }

        /// <summary>
        /// sets the initial POST request response handling
        /// </summary>
        /// <param name="postResponse"></param>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public static async Task HandleInitialPostResponse(HttpResponseMessage postResponse)
        {
            // check if the POST request was successful
            if (postResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"\nPOST request was successful: {postResponse.StatusCode}\n");
            }
            else
            {
                Console.WriteLine($"POST request failed with status code: {postResponse.StatusCode}\n");
                Console.WriteLine(postResponse);
            }

            var postResponseContent = await postResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content:{postResponseContent}\n");
        }

        /// <summary>
        /// sets the GET request for the Dashboard page
        /// </summary>
        /// <param name="client"></param>
        /// <param name="redirectUri"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<string> SendDashboardGetRequest(HttpClient client, string dashboardUri, string redirectUri)
        {
            // send a GET request to retrieve the dashboard page
            HttpResponseMessage initialDashboardResponse = await client.GetAsync(dashboardUri);

            // send a GET request to retrieve the redirect page
            HttpResponseMessage dashboardResponse = await client.GetAsync(redirectUri);

            if (initialDashboardResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("\nDashboard GET request was successful\n");
            }
            else
            {
                Console.WriteLine($"\nDashboard GET request failed with status code: {initialDashboardResponse.StatusCode}");
                string initialDashboardResponseContent = await initialDashboardResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{initialDashboardResponseContent}");
            }

            if (dashboardResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("\nRedirect page GET request was successful\n");
            }
            else
            {
                Console.WriteLine($"\nRedirect page GET request failed with status code: {dashboardResponse.StatusCode}");
                string dashboardResponseContent = await dashboardResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{dashboardResponseContent}");
            }
            return null;
        }

        public static async Task<string> SendOpenidGetRequest(HttpClient client, string openidUri)
        {
            // send a GET request to retrieve the openid information
            HttpResponseMessage openidResponse = await client.GetAsync(openidUri);

            if (openidResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("\nOpen ID GET request was successful\n");
                // retrieves the response body as a string
                //string openidResponseBody = await client.GetStringAsync(openidUri);
                //Console.WriteLine($"RESPONSE BODY: {openidResponseBody}\n");

                //return openidResponseBody;
            }
            else
            {
                Console.WriteLine($"\nOpen ID GET request failed with status code: {openidResponse.StatusCode}");
                string openidResponseContent = await openidResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{openidResponseContent}");
            }
            return null;
        }

        //public static async Task<HttpResponseMessage> SendAuthenticationPostRequest(HttpClient client, string authenticationUri, string client_id, string redirect_uri, string response_type, string scope, string grant_type)
        //{
        //    // set the POST request header
        //    var authenticationPostRequestURL = authenticationUri;
        //    //Console.Write($"POST request header = {postRequestURL}");

        //    // simulate form data for the POST request
        //    var formContent = new FormUrlEncodedContent(new[]
        //    {
        //            new KeyValuePair<string, string>("client_id", client_id),
        //            new KeyValuePair<string, string>("redirect_uri", redirect_uri),
        //            new KeyValuePair<string, string>("response_type", response_type),
        //            new KeyValuePair<string, string>("scope", scope),
        //            new KeyValuePair<string, string>("grant_type", grant_type)
        //    });

        //    // modify the Content-Type header of the formContent to include the charset
        //    formContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded")
        //    {
        //        CharSet = "UTF-8"
        //    };

        //    // send a POST request with the query parameters and form data
        //    var postResponse = await client.PostAsync(authenticationPostRequestURL, formContent);

        //    // handle the response content
        //    var stringContent = await postResponse.Content.ReadAsStringAsync();

        //    return await client.PostAsync(authenticationPostRequestURL, formContent);
        //}

        public static async Task<string> SendUseridGetRequest(HttpClient client, string useridUri)
        {
            // send a GET request to retrieve the identity data information
            HttpResponseMessage useridResponse = await client.GetAsync(useridUri);

            if (useridResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("User ID GET request was successful\n");
            }
            else
            {
                Console.WriteLine($"\nUser ID GET request failed with status code: {useridResponse.StatusCode}");
                string useridResponseContent = await useridResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{useridResponseContent}");
            }
            return null;
        }

        public static async Task<string> SendIdentityGetRequest(HttpClient client, string identityUri)
        {
            // send a GET request to retrieve the identity data information
            HttpResponseMessage identityResponse = await client.GetAsync(identityUri);

            if (identityResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Identity GET request was successful\n");

                // retrieves the response body as a string
                // string identityResponseBody = await client.GetStringAsync(identityUri);
                // Console.WriteLine($"RESPONSE BODY: {identityResponseBody}\n");

                //return identityResponseBody;
            }
            else
            {
                Console.WriteLine($"\nIdentity  GET request failed with status code: {identityResponse.StatusCode}");
                string identityResponseContent = await identityResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{identityResponseContent}");
            }
            return null;
        }

            public static async Task<string> SendUsageDataGetRequest(HttpClient client, string usageDataUri)
        {
            // send a GET request to retrieve the usage data information
            HttpResponseMessage usageDataResponse = await client.GetAsync(usageDataUri);

            if (usageDataResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Usage data GET request was successful\n");

                // retrieves the response body as a string
                // string authEndpointResponseBody = await client.GetStringAsync(authEndpointUri);
                // Console.WriteLine($"RESPONSE BODY: {authEndpointResponseBody}\n");

                // return authEndpointResponseBody;
            }
            else
            {
                Console.WriteLine($"\nUsage data GET request failed with status code: {usageDataResponse.StatusCode}");
                string usageDataResponseContent = await usageDataResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{usageDataResponseContent}");
            }
            return null;
        }

        public static async Task<string> SendUserDataGetRequest(HttpClient client, string userDataUri)
        {
            HttpResponseMessage userDataResponse = await client.GetAsync(userDataUri);

            if (userDataResponse.IsSuccessStatusCode)
            {
                string userDataJson = await userDataResponse.Content.ReadAsStringAsync();
                Console.WriteLine("User Data JSON:");
                Console.WriteLine(userDataJson);

                // retrieves the response body as a string
                string userDataResponseBody = await client.GetStringAsync(userDataUri);
                Console.WriteLine($"RESPONSE BODY: {userDataResponseBody}\n");

                return userDataResponseBody;
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {userDataResponse.StatusCode}");
                //string userDataResponseContent = await userDataResponse.Content.ReadAsStringAsync();
                //Console.WriteLine($"Response Content:\n{userDataResponseContent}");
            }

            return null;
        }

        public static async Task<string> SendDataGiftGetRequest(HttpClient client, string dataGiftUri)
        {
            HttpResponseMessage dataGiftResponse = await client.GetAsync(dataGiftUri);

            if (dataGiftResponse.IsSuccessStatusCode)
            {
                // send a GET request to retrieve the data gift page
                Console.WriteLine("Data Gift GET request was successful\n");
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {dataGiftResponse.StatusCode}");
                string dataGiftResponseContent = await dataGiftResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{dataGiftResponseContent}");
            }

            return null;
        }
    }
}