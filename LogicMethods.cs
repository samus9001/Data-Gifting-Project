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
        /// set the request headers for the first GET request
        /// </summary>
        /// <param name="client"></param>
        public static void SetFirstGETRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/119.0");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.5");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("DNT", "1");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "none");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
            client.DefaultRequestHeaders.Add("Sec-GPC", "1");
        }

        /// <summary>
        /// sets the first GET request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="baseUri"></param>
        /// <returns></returns>
        public static async Task<Uri> SendFirstGetRequest(HttpClient client, string baseUri)
        {
            // send a GET request to retrieve the first page
            HttpResponseMessage response = await client.GetAsync(baseUri);

            // handle the case when the GET request is successful
            if (response.StatusCode == HttpStatusCode.Found)
            {
                Console.WriteLine("\nfirst GET request was successful");
                Console.WriteLine($"first GET request URI = {baseUri}");
            }
            else
            {
                // handle the case when the GET request is not successful
                Console.WriteLine($"initial GET request failed with status code: {response.StatusCode}");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{responseContent}");
                Console.WriteLine($"first GET request URI = {baseUri}");
                return null;
            }

            // store the first response Location header value
            var firstLocationHeader = response.Headers.Location;

            Console.WriteLine($"\nfirst Location header = {firstLocationHeader}");

            return firstLocationHeader;
        }

        /// <summary>
        /// set the request headers for the first redirect GET request
        /// </summary>
        /// <param name="client"></param>
        public static void SetFirstRedirectGETRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/119.0");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.5");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("DNT", "1");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "none");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
            client.DefaultRequestHeaders.Add("Sec-GPC", "1");
        }

        /// <summary>
        /// sets the first redirect GET request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="baseUri"></param>
        /// <param name="firstLocationHeader"></param>
        /// <returns></returns>
        public static async Task<Uri> SendFirstRedirectGetRequest(HttpClient client, string baseUri, Uri firstLocationHeader)
        {
            // send a GET request to retrieve the first redirect page
            HttpResponseMessage response = await client.GetAsync($"{baseUri}{firstLocationHeader}");

            // handle the case when the GET request is successful
            if (response.StatusCode == HttpStatusCode.Found)
            {
                Console.WriteLine("\n\nfirst redirect GET request was successful");
                Console.WriteLine($"first redirect GET request URI = {baseUri}{firstLocationHeader}");

                // return the uri from the first redirect
                //return response.RequestMessage.RequestUri;
            }
            else
            {
                // handle the case when the GET request is not successful
                Console.WriteLine($"first redirect GET request failed with status code: {response.StatusCode}");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{responseContent}");
                Console.WriteLine($"\n\nfirst redirect GET request URI = {baseUri}{firstLocationHeader}");
                return null;
            }

            // store the second response Location header value
            var secondLocationHeader = response.Headers.Location;

            Console.WriteLine($"\n\nsecond Location header = {secondLocationHeader}");

            return secondLocationHeader;
        }

        /// <summary>
        /// set the request headers for the second redirect GET request
        /// </summary>
        /// <param name="client"></param>
        public static void SetSecondRedirectGETRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/119.0");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.5");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("DNT", "1");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "none");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
            client.DefaultRequestHeaders.Add("Sec-GPC", "1");
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

            if (response.StatusCode == HttpStatusCode.Found)
            {
                Console.WriteLine("\nsecond redirect GET request was successful");
                Console.WriteLine($"second redirect GET request URI = {secondLocationRedirect}");
            }
            else
            {
                // handle the case when the GET request is not successful
                Console.WriteLine($"second redirect GET request failed with status code: {response.StatusCode}");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{responseContent}");
                //Console.WriteLine($"\n\nsecond redirect GET request URI = {secondLocationRedirect}");
                return null;
            }

            // store the third response Location header value
            var thirdLocationHeader = response.Headers.Location;

            Console.WriteLine($"\n\nthird Location header = {thirdLocationHeader}");

            Uri authorizeReferrerUri = new Uri($"{thirdLocationHeader}");

            return thirdLocationHeader;
        }

        /// <summary>
        /// set the request headers for the login GET request
        /// </summary>
        /// <param name="client"></param>
        public static void SetLoginGETRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/119.0");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.5");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("DNT", "1");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "none");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
            client.DefaultRequestHeaders.Add("Sec-GPC", "1");
        }

        /// <summary>
        /// sets the login page GET request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="thirdLocationRedirect"></param>
        /// <returns></returns>
        public static async Task<string> SendLoginGetRequest(HttpClient client, Uri thirdLocationRedirect)
        {
            // send a GET request to retrieve the login page
            HttpResponseMessage response = await client.GetAsync(thirdLocationRedirect);

            //Console.WriteLine($"\nlogin page GET request URL = {thirdLocationRedirect}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nlogin page GET request was successful");
                Console.WriteLine($"third redirect GET request URI = {thirdLocationRedirect}\n");

                // retrieves the response body as a string
                string responseBody = await client.GetStringAsync(thirdLocationRedirect);
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
        /// extracts the clientId value from the response and assigns it to clientId string
        /// </summary>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        public static string ExtractClientIdValue(string responseBody)
        {
            string clientIdPrefix = "clientid=";

            // find the start of the clientId value in the response body string
            int clientIdStartIndex = responseBody.IndexOf(clientIdPrefix);

            if (clientIdStartIndex != -1)
            {
                clientIdStartIndex += clientIdPrefix.Length; // move past the prefix

                // find the quotation mark character at the end of the clientId value in the response body string
                int clientIdEndIndex = responseBody.IndexOf('"', clientIdStartIndex);

                if (clientIdEndIndex != -1)
                {
                    // extract the substring containing the clientId value
                    string extractedClientId = responseBody.Substring(clientIdStartIndex, clientIdEndIndex - clientIdStartIndex);


                    //Console.WriteLine($"\nCLIENTID VALUE = {extractedClientId}\n");

                    return extractedClientId;
                }
                else
                {
                    Console.WriteLine("Failed to extract ClientId value.");
                }
            }
            else
            {
                // handle the case where "clientId" is not found in the response
                Console.WriteLine("clientId was not found in the response body.");

            }
            return null;
        }

        /// <summary>
        /// extracts the CSRF token value from the response and assigns it to csrf_token
        /// </summary>
        /// <param name="client"></param>
        /// <param name="responseBody"></param>
        public static string ExtractCsrfToken(HttpClient client, string responseBody)
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
                    string csrf_token = responseBody.Substring(csrfStartIndex, csrfEndIndex - csrfStartIndex);

                    // remove the existing "X-CSRF-TOKEN" header if it exists
                    if (client.DefaultRequestHeaders.Contains("X-CSRF-TOKEN"))
                    {
                        client.DefaultRequestHeaders.Remove("X-CSRF-TOKEN");
                    }

                    //Console.WriteLine($"RESPONSE BODY: {responseBody}\n");

                    // assign CSRF token to the X-CSRF-TOKEN header
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", csrf_token);
                    //Console.WriteLine($"\nCSRF VALUE = {csrf_token}\n");

                    return csrf_token;
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
            return null;
        }

        /// <summary>
        /// extracts the tx value from the response and assigns it to tx string
        /// </summary>
        /// <param name="responseBody"></param>
        /// <param name="tx"></param>
        public static string ExtractTxValue(string responseBody)
        {
            string txPrefix = "StateProperties=";

            // find the start of StateProperties in the response body string
            int txStartIndex = responseBody.IndexOf(txPrefix);

            if (txStartIndex != -1)
            {
                // find the quotation mark character at the end of StateProperties in the response body string
                int txEndIndex = responseBody.IndexOf('"', txStartIndex);

                if (txEndIndex != -1)
                {
                    // extract the substring containing the StateProperties value
                    string tx = responseBody.Substring(txStartIndex, txEndIndex - txStartIndex);

                    //Console.WriteLine($"TX VALUE = {tx}\n");

                    return tx;
                }
                else
                {
                    Console.WriteLine("Failed to extract TX value.");
                }
            }
            else
            {
                Console.WriteLine("StateProperties prefix was not found in the response body.");
            }
            return null;
        }

        /// <summary>
        /// extracts the pageViewId value from the response and assigns it to pageViewId string
        /// </summary>
        /// <param name="responseBody"></param>
        /// <param name="pageViewId"></param>
        public static string ExtractPageViewIdValue(string responseBody)
        {
            string pageViewIdPrefix = "\"pageViewId\":\"";

            // find the start of the pageViewId value in the response body string
            int pageViewIdStartIndex = responseBody.IndexOf(pageViewIdPrefix);

            if (pageViewIdStartIndex != -1)
            {
                pageViewIdStartIndex += pageViewIdPrefix.Length; // move past the prefix

                // find the quotation mark character at the end of the pageViewId value in the response body string
                int pageViewIdEndIndex = responseBody.IndexOf('"', pageViewIdStartIndex);

                if (pageViewIdEndIndex != -1)
                {
                    // extract the substring containing the pageViewId value
                    string extractedPageViewId = responseBody.Substring(pageViewIdStartIndex, pageViewIdEndIndex - pageViewIdStartIndex);


                    //Console.WriteLine($"\nPAGEVIEWID VALUE = {extractedPageViewId}\n");

                    return extractedPageViewId;
                }
                else
                {
                    Console.WriteLine("Failed to extract PageViewId value.");
                }
            }
            else
            {
                // handle the case where "pageViewId" is not found in the response
                Console.WriteLine("pageViewId was not found in the response body.");

            }
            return null;
        }

        /// <summary>
        /// set the request headers for the first static page GET request
        /// </summary>
        /// <param name="client"></param>
        public static void SetFirstStaticPageGETRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/119.0");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.5");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("DNT", "1");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        }

        /// <summary>
        /// sets the first static page GET request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="signupUriQueries"></param>
        /// <param name="thirdLocationRedirect"></param>
        /// <returns></returns>
        public static async Task<Uri> SendFirstStaticPageGetRequest(HttpClient client, string signupUriQueries, Uri thirdLocationRedirect)
        {
            // create a HttpRequestMessage to set the Referrer header
            var request = new HttpRequestMessage(HttpMethod.Get, signupUriQueries);
            request.Headers.Referrer = thirdLocationRedirect;

            // send the GET request with the modified headers
            HttpResponseMessage response = await client.SendAsync(request);

            // handle the case when the GET request is successful
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nsignup page GET request was successful");
                // retrieves the response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseBody);
            }
            else
            {
                // handle the case when the GET request is not successful
                Console.WriteLine($"confirmed login GET request failed with status code: {response.StatusCode}");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{responseContent}");
                return null;
            }
            return null;
        }

        public static async Task<HttpResponseMessage> SendFirstPostRequest(HttpClient client, string loginUri, string tx, string p, string request_type, string signInName, Uri refererUri)
        {
            // set the POST request header
            var initialPostRequestURL = loginUri + $"?tx={tx}&p={p}";
            //Console.Write($"POST request header = {postRequestURL}");

            // create a HttpRequestMessage to set the Referer header
            var request = new HttpRequestMessage(HttpMethod.Post, initialPostRequestURL);
            request.Headers.Referrer = refererUri;

            // simulate form data for the POST request
            var formContent = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("request_type", request_type),
                    new KeyValuePair<string, string>("signInName", signInName)
            });

            // modify the Content-Type header of the formContent to include the charset
            formContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded")
            {
                CharSet = "UTF-8"
            };

            // set the form data in the request
            request.Content = formContent;

            // send a POST request with the query parameters and form data
            var postResponse = await client.SendAsync(request);

            // handle the response content
            var stringContent = await postResponse.Content.ReadAsStringAsync();

            return postResponse;
        }

        public static async Task HandleFirstPostResponse(HttpResponseMessage postResponse)
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

        public static void SetSecondGETRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/119.0");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.5");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("DNT", "1");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
            client.DefaultRequestHeaders.Add("Sec-GPC", "1");
        }

        public static async Task<string> SendSecondGetRequest(HttpClient client, string confirmedUriQueries, Uri referrerUri)
        {
            // create a HttpRequestMessage to set the Referer header
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, confirmedUriQueries);
            request.Headers.Referrer = referrerUri;

            // send the GET request with the modified headers
            HttpResponseMessage response = await client.SendAsync(request);

            // handle the case when the GET request is successful
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nconfirmed login GET request was successful");

                // retrieves the response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseBody);

                return responseBody;
            }
            else
            {
                // handle the case when the GET request is not successful
                Console.WriteLine($"confirmed login GET request failed with status code: {response.StatusCode}");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{responseContent}");
                return null;
            }
        }

        public static void SetSecondStaticPageGETRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/119.0");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.5");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("DNT", "1");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
            client.DefaultRequestHeaders.Add("Sec-GPC", "1");
        }

        /// <summary>
        /// sets the second static page GET request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="signupUriQueries"></param>
        /// <param name="referrerUrl"></param>
        /// <returns></returns>
        public static async Task<Uri> SendSecondStaticPageGetRequest(HttpClient client, string signupUriQueries, string referrerUrl)
        {
            // create a HttpRequestMessage to set the Referrer header
            var request = new HttpRequestMessage(HttpMethod.Get, signupUriQueries);
            request.Headers.Referrer = new Uri(referrerUrl);

            // send the GET request with the modified headers
            HttpResponseMessage response = await client.SendAsync(request);

            // handle the case when the GET request is successful
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nsignup page GET request was successful");
                // retrieves the response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseBody);
            }
            else
            {
                // handle the case when the GET request is not successful
                Console.WriteLine($"confirmed login GET request failed with status code: {response.StatusCode}");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{responseContent}");
                return null;
            }
            return null;
        }

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
        public static async Task<HttpResponseMessage> SendSecondPostRequest(HttpClient client, string loginUri, string tx, string p, string request_type, string signInName, string password, string refererUrl)
        {
            // set the POST request header
            var initialPostRequestURL = loginUri + $"?tx={tx}&p={p}";
            //Console.Write($"POST request header = {postRequestURL}");

            // create a HttpRequestMessage to set the Referer header
            var request = new HttpRequestMessage(HttpMethod.Post, initialPostRequestURL);
            request.Headers.Referrer = new Uri(refererUrl);

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

            // set the form data in the request
            request.Content = formContent;

            // send a POST request with the query parameters and form data
            var postResponse = await client.SendAsync(request);

            // handle the response content
            var stringContent = await postResponse.Content.ReadAsStringAsync();

            return postResponse;
        }

        /// <summary>
        /// sets the initial POST request response handling
        /// </summary>
        /// <param name="postResponse"></param>
        /// <returns></returns>
        public static async Task HandleSecondPostResponse(HttpResponseMessage postResponse)
        {
            // check if the POST request was successful
            if (postResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"\nPOST request was successful: {postResponse.StatusCode}\n");
            }
            else
            {
                Console.WriteLine($"\nPOST request failed with status code: {postResponse.StatusCode}\n");
                Console.WriteLine(postResponse);
            }

            var postResponseContent = await postResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content:{postResponseContent}\n");
        }

        /// <summary>
        /// sets a GET request to confirm the login is successful
        /// </summary>
        /// <param name="client"></param>
        /// <param name="confirmedUri"></param>
        /// <param name="rememberMe"></param>
        /// <param name="csrf"></param>
        /// <param name="tx"></param>
        /// <param name="p"></param>
        /// <param name="diags"></param>
        /// <returns></returns>
        public static async Task<Uri> SendFourthGetRequest(HttpClient client, string confirmedUriQueries, string referrerUrl)
        {
            // create a HttpRequestMessage to set the Referrer header
            var request = new HttpRequestMessage(HttpMethod.Get, confirmedUriQueries);
            request.Headers.Referrer = new Uri(referrerUrl);

            // send the GET request with the modified headers
            HttpResponseMessage response = await client.SendAsync(request);

            // handle the case when the GET request is successful
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nconfirmed login GET request was successful");
                // retrieves the response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseBody);
            }
            else
            {
                // handle the case when the GET request is not successful
                Console.WriteLine($"confirmed login GET request failed with status code: {response.StatusCode}");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{responseContent}");
                return null;
            }
            return null;
        }

        /// <summary>
        /// sets the GET request for the Dashboard page
        /// </summary>
        /// <param name="client"></param>
        /// <param name="dashboardUri"></param>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public static async Task<Uri> SendDashboardGetRequest(HttpClient client, string dashboardUri)
        {
            // send a GET request to retrieve the dashboard page
            HttpResponseMessage initialDashboardResponse = await client.GetAsync(dashboardUri);

            if (initialDashboardResponse.StatusCode == HttpStatusCode.Found)
            {
                Console.WriteLine("\nDashboard GET request was successful\n");
            }
            else
            {
                Console.WriteLine($"\nDashboard GET request failed with status code: {initialDashboardResponse.StatusCode}");
                string initialDashboardResponseContent = await initialDashboardResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content:\n{initialDashboardResponseContent}");
            }
            return null;
        }

        public static async Task<Uri> SendDashboardRedirectGetRequest(HttpClient client, string dashboardRedirectUri)
        {
            // send a GET request to retrieve the redirect page
            HttpResponseMessage dashboardResponse = await client.GetAsync(dashboardRedirectUri);

            if (dashboardResponse.StatusCode == HttpStatusCode.Found)
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

        public static async Task<HttpResponseMessage> SendAuthenticationPostRequest(HttpClient client, string authenticationUri, string client_id, string redirect_uri, string response_type, string scope, string grant_type)
        {
            // set the POST request header
            var authenticationPostRequestURL = authenticationUri;
            //Console.Write($"POST request header = {postRequestURL}");

            // simulate form data for the POST request
            var formContent = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("client_id", client_id),
                    new KeyValuePair<string, string>("redirect_uri", redirect_uri),
                    new KeyValuePair<string, string>("response_type", response_type),
                    new KeyValuePair<string, string>("scope", scope),
                    new KeyValuePair<string, string>("grant_type", grant_type)
            });

            // modify the Content-Type header of the formContent to include the charset
            formContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded")
            {
                CharSet = "UTF-8"
            };

            // send a POST request with the query parameters and form data
            var postResponse = await client.PostAsync(authenticationPostRequestURL, formContent);

            // handle the response content
            var stringContent = await postResponse.Content.ReadAsStringAsync();

            return await client.PostAsync(authenticationPostRequestURL, formContent);
        }

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

        /// <summary>
        /// set the defualt headers for GET requests
        /// </summary>
        /// <param name="client"></param>
        public static void SetDefaultGETRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/119.0");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.5");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("DNT", "1");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        }

        /// <summary>
        /// set the defualt headers for POST requests
        /// </summary>
        /// <param name="client"></param>
        public static void SetDefaultPOSTRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/119.0");
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
        /// remove the defualt headers for GET requests
        /// </summary>
        /// <param name="client"></param>
        public static void RemoveGETRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Remove("User-Agent");
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.Remove("Accept-Language");
            client.DefaultRequestHeaders.Remove("Accept-Encoding");
            client.DefaultRequestHeaders.Remove("DNT");
            client.DefaultRequestHeaders.Remove("Connection");
            client.DefaultRequestHeaders.Remove("Sec-Fetch-Dest");
            client.DefaultRequestHeaders.Remove("Sec-Fetch-Mode");
            client.DefaultRequestHeaders.Remove("Sec-Fetch-Site");
            client.DefaultRequestHeaders.Remove("Upgrade-Insecure-Requests");
            client.DefaultRequestHeaders.Remove("Sec-Fetch-User");
            client.DefaultRequestHeaders.Remove("Sec-GPC");
        }

        /// <summary>
        /// remove the defualt headers for POST requests
        /// </summary>
        /// <param name="client"></param>
        public static void RemovePOSTRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Remove("User-Agent");
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.Remove("Accept-Language");
            client.DefaultRequestHeaders.Remove("Accept-Encoding");
            client.DefaultRequestHeaders.Remove("X-Requested-With");
            client.DefaultRequestHeaders.Remove("Origin");
            client.DefaultRequestHeaders.Remove("DNT");
            client.DefaultRequestHeaders.Remove("Connection");
            client.DefaultRequestHeaders.Remove("Sec-Fetch-Dest");
            client.DefaultRequestHeaders.Remove("Sec-Fetch-Mode");
            client.DefaultRequestHeaders.Remove("Sec-Fetch-Site");
        }
    }
}