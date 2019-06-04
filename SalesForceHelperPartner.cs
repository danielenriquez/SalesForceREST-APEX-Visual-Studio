
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Services.Protocols;

namespace SalesForcePartnerCnn {

    public class SalesForceHelperPartner {
        public string sessionId;
        public string instanceUrl;

        public static SforceService Binding;

        /// <summary>
        /// Set secure SalesForce connection protocol
        /// </summary>
        public static void SfProtocolConn() {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        /// <summary>
        /// SalesForce Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool Login(string username, string password, string token) {
            password = password + token;
            // Create a service object  
            Binding = new SforceService { Timeout = 360000 };
            // Timeout after 2 minute  
            // Try logging in  
            LoginResult lr;

            try {
                lr = Binding.login(username, password);
                lr.passwordExpired = false;
            }

            // ApiFault is a proxy stub generated from the WSDL contract when  
            // the web service was imported  
            catch (SoapException e) {
                // Write the fault code to the console  
                Console.WriteLine(e.Code);
                // Write the fault message to the console  
                Console.WriteLine("An unexpected error has occurred: " + e.Message);
                // Write the stack trace to the console  
                Console.WriteLine(e.StackTrace);
                // Return False to indicate that the login was not successful  
                //wrongCredentiallb.Text = "An unexpected error has occurred: " + e.Message;
                return false;
            }

            // Check if the password has expired  
            if (lr.passwordExpired) {
                Console.WriteLine("An error has occurred. Your password has expired.");
                //wrongCredentiallb.Text = "An error has occurred. Your password has expired.";
                return false;
            }

            /** Once the client application has logged in successfully, it will use
             * the results of the login call to reset the endpoint of the service
             * to the virtual server instance that is servicing your organization
             */

            Binding.Url = lr.serverUrl;
            /** The sample client application now has an instance of the SforceService
             * that is pointing to the correct endpoint. Next, the sample client
             * application sets a persistent SOAP header (to be included on all
             * subsequent calls that are made with SforceService) that contains the
             * valid sessionId for our login credentials. To do this, the sample
             * client application creates a new SessionHeader object and persist it to
             * the SforceService. Add the session ID returned from the login to the
             * session header
             */

            Binding.SessionHeaderValue = new SessionHeader();
            Binding.SessionHeaderValue.sessionId = lr.sessionId;
            // Return true to indicate that we are logged in, pointed  
            // at the right URL and have our security token in place.  
            return true;
        }

        #region Lead
        /// <summary>
        /// Connect to SalesForce rest webservices
        /// PostLeadRestServiceAsync
        /// </summary>
        /// <returns></returns>
        public static async Task<string> PostLeadRestServiceAsync(string jsonParams) {

            string requestMessage = jsonParams; // "{\"email\": \"test@test.test\", \"phone\": \"0034922777888\", \"lastName\": \"Tester\", \"firstName\": \"Testy\"}";

            HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");

            string[] urlParts = Binding.Url.Split('/');
            string mainUrl = urlParts[0] + "//" + urlParts[2];
            //SalesForce REST Service URL
            string requestUrl = mainUrl + @"/services/apexrest/signallia-leads/";
            //create request message associated with POST verb
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            //return JSON to the caller
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //add token to header
            request.Headers.Add("Authorization", "Bearer " + Binding.SessionHeaderValue.sessionId);
   
            //add content to HttpRequestMessage;
            request.Content = content;
            
            HttpClient putClient = new HttpClient();

            var response = await putClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK) {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            } else {
                //resturn the error message                
                return Convert.ToString(response.StatusCode);    
            }
        }

        /// <summary>
        /// Connect to SalesForce rest webservices
        /// PutLeadRestServiceAsync
        /// </summary>
        /// <returns></returns>
        public static async Task<string> PutLeadRestServiceAsync(string jsonParams) {

            string requestMessage = jsonParams; // "{\"email\": \"test1111@test.test\", \"phone\": \"0034922777888\", \"lastName\": \"Tester\", \"firstName\": \"Testy\"}";

            HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");

            string[] urlParts = Binding.Url.Split('/');
            string mainUrl = urlParts[0] + "//" + urlParts[2];
            //SalesForce REST Service URL
            string requestUrl = mainUrl + @"/services/apexrest/signallia-leads/";
            //create request message associated with PUT verb
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUrl);
            //return JSON to the caller
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //add token to header
            request.Headers.Add("Authorization", "Bearer " + Binding.SessionHeaderValue.sessionId);

            //add content to HttpRequestMessage;
            request.Content = content;

            HttpClient putClient = new HttpClient();

            var response = await putClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK) {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            } else {
                //resturn the error message                
                return Convert.ToString(response.StatusCode);
            }
        }

        /// <summary>
        /// Connect to SalesForce rest webservices
        /// GetApexRestServiceAsync
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetLeadRestServiceAsync(string Id) {

            string[] urlParts = Binding.Url.Split('/');
            string mainUrl = urlParts[0] + "//" + urlParts[2];
            //SalesForce REST Service URL
            string requestUrl = mainUrl + @"/services/apexrest/signallia-leads/" + Id;
            //create request message associated with GET verb
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            //return JSON to the caller
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //add token to header
            request.Headers.Add("Authorization", "Bearer " + Binding.SessionHeaderValue.sessionId);

            HttpClient putClient = new HttpClient();

            var response = await putClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK) {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            } else {
                //resturn the error message                
                return Convert.ToString(response.StatusCode);
            }
        }

        /// <summary>
        /// Connect to SalesForce rest webservices
        /// GetApexRestServiceAsync
        /// </summary>
        /// <returns></returns>
        public static async Task<string> DeleteLeadRestServiceAsync(string Id) {

            string[] urlParts = Binding.Url.Split('/');
            string mainUrl = urlParts[0] + "//" + urlParts[2];
            //SalesForce REST Service URL
            string requestUrl = mainUrl + @"/services/apexrest/signallia-leads/" + Id;
            //create request message associated with GET verb
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);
            //return JSON to the caller
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //add token to header
            request.Headers.Add("Authorization", "Bearer " + Binding.SessionHeaderValue.sessionId);

            HttpClient putClient = new HttpClient();

            var response = await putClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK) {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            } else {
                //resturn the error message                
                return Convert.ToString(response.StatusCode);
            }
        }

        #endregion

        #region Preferences
        /// <summary>
        /// Connect to SalesForce rest webservices
        /// GetApexRestServiceAsync
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetKeysPreferencesServiceAsync(string keysNumber) {

            string[] urlParts = Binding.Url.Split('/');
            string mainUrl = urlParts[0] + "//" + urlParts[2];
            //SalesForce REST Service URL
            string requestUrl = mainUrl + @"/services/apexrest/signallia-keys-preferences/" + keysNumber;
            //create request message associated with GET verb
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            //return JSON to the caller
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //add token to header
            request.Headers.Add("Authorization", "Bearer " + Binding.SessionHeaderValue.sessionId);

            HttpClient putClient = new HttpClient();

            var response = await putClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK) {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            } else {
                //resturn the error message                
                return Convert.ToString(response.StatusCode);
            }
        }

        public static async Task<string> PutKeysPreferencesServiceAsync(string keysNumber, string jsonParams) {

            string requestMessage = jsonParams; // "{\"email\": \"test1111@test.test\", \"phone\": \"0034922777888\", \"lastName\": \"Tester\", \"firstName\": \"Testy\"}";

            HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");

            string[] urlParts = Binding.Url.Split('/');
            string mainUrl = urlParts[0] + "//" + urlParts[2];
            //SalesForce REST Service URL
            string requestUrl = mainUrl + @"/services/apexrest/signallia-keys-preferences/" + keysNumber;
            //create request message associated with PUT verb
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUrl);
            //return JSON to the caller
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //add token to header
            request.Headers.Add("Authorization", "Bearer " + Binding.SessionHeaderValue.sessionId);

            //add content to HttpRequestMessage;
            request.Content = content;

            HttpClient putClient = new HttpClient();

            var response = await putClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK) {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            } else {
                //resturn the error message                
                return Convert.ToString(response.StatusCode);
            }
        }

        #endregion

        #region Request
        public static async Task<string> PostRequestRestServiceAsync(string jsonParams) {

            string requestMessage = jsonParams; // "{\"email\": \"test1111@test.test\", \"phone\": \"0034922777888\", \"lastName\": \"Tester\", \"firstName\": \"Testy\"}";

            HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");

            string[] urlParts = Binding.Url.Split('/');
            string mainUrl = urlParts[0] + "//" + urlParts[2];
            //SalesForce REST Service URL
            string requestUrl = mainUrl + @"/services/apexrest/signallia-keys-request/";
            //create request message associated with POST verb
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            //return JSON to the caller
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //add token to header
            request.Headers.Add("Authorization", "Bearer " + Binding.SessionHeaderValue.sessionId);

            //add content to HttpRequestMessage;
            request.Content = content;

            HttpClient putClient = new HttpClient();

            var response = await putClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK) {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            } else {
                //resturn the error message                
                return Convert.ToString(response.StatusCode);
            }
        }
    }
}
