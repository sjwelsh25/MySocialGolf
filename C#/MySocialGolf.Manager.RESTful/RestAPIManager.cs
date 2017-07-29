using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using MySocialGolf.Manager;
using MySocialGolf.Model;

namespace MySocialGolf.Manager.RESTful
{
    public class RestAPIManager
    {
        public string TestRestApi(RestCallModel rcModel)
        {
            string responseString = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(rcModel.BaseAddressUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Task<HttpResponseMessage> response = null;
                switch (rcModel.HttpVerb.ToUpper())
                {
                    case "GET": 
                        var response = httpClient.GetAsync(rcModel.Url).Result;
                        rcModel.StatusCode = ((int)response.StatusCode).ToString();
                        rcModel.ResponseMessage = response.Content.ReadAsStringAsync().Result;
                        break;
                    case "POST":
                        var sc = new System.Net.Http.StringContent(rcModel.InputJson, Encoding.UTF8, "application/json");
                        var responsePost = httpClient.PostAsync(rcModel.Url, sc).Result;
                        rcModel.StatusCode = ((int)responsePost.StatusCode).ToString();
                        rcModel.ResponseMessage = responsePost.Content.ReadAsStringAsync().Result;
                        break;
                    case "PUT":
                        var scPut = new System.Net.Http.StringContent(rcModel.InputJson, Encoding.UTF8, "application/json");
                        var responsePut = httpClient.PutAsync(rcModel.Url, scPut).Result;
                        rcModel.StatusCode = ((int)responsePut.StatusCode).ToString();
                        rcModel.ResponseMessage = responsePut.Content.ReadAsStringAsync().Result;
                        break;
                    case "DELETE":
                        var responseDelete = httpClient.DeleteAsync(rcModel.Url).Result;
                        rcModel.StatusCode = ((int)responseDelete.StatusCode).ToString();
                        rcModel.ResponseMessage = responseDelete.Content.ReadAsStringAsync().Result;
                        break;
                }
            }

            return responseString;
        }
    }
}

