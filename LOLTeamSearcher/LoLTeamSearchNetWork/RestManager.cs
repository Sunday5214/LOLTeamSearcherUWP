using Newtonsoft.Json;
using NoticeDID.Common;
using RestSharp;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace LOLTeamSearcherNetWork
{
    public class UrlSegment
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public UrlSegment(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
    public class QueryParam
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public QueryParam(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
    public class Header
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Header(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
    public class RestManager
    {


        private static RestClient CreateClient()
        {
            var restClient = new RestClient(NetworkOptions.serverUrl) { Timeout = NetworkOptions.timeOut };
            return restClient;
        }
        public async Task<T> GetResponse<T>(string resource, Method method, string parameterJson = null, QueryParam[] queryParams = null, UrlSegment[] urlSegments = null, Header[] headers = null)
        {
            T resp = default(T);
            try
            {
                var client = CreateClient();
                var restRequest = CreateRequest(resource, method, parameterJson, queryParams, urlSegments, headers);
                var response = await client.ExecuteAsync(restRequest);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(response.StatusCode.ToString());
                }
                resp = JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                return default(T);
            }


            return resp;
        }

        private RestRequest CreateRequest(string resource, Method method, string parameterJson, QueryParam[] queryParams, UrlSegment[] urlSegments, Header[] headers)
        {
            var restRequest = new RestRequest(resource, method) { Timeout = NetworkOptions.timeOut };
            restRequest = AddToRequest(restRequest, null, parameterJson, queryParams, urlSegments, headers);

            return restRequest;
        }

        private RestRequest AddToRequest(RestRequest restRequest, object token, string parameterJson, QueryParam[] queryParams, UrlSegment[] urlSegments, Header[] headers)
        {
            if (urlSegments != null)
            {
                foreach (var urlSegment in urlSegments)
                {
                    restRequest.AddUrlSegment(urlSegment.Name, urlSegment.Value);
                }
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    restRequest.AddHeader(header.Name, header.Value);
                }
            }

            if (queryParams != null)
            {
                foreach (var queryParam in queryParams)
                {
                    restRequest.AddParameter(queryParam.Name, queryParam.Value);
                }
            }

            if (!string.IsNullOrEmpty(parameterJson))
            {
                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddParameter("application/json", parameterJson, ParameterType.RequestBody);
            }
            return restRequest;
        }
    }
}
