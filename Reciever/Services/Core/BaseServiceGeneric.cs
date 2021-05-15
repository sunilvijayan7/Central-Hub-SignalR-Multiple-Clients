using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reciever.Services.Core
{
    public interface IBaseServiceGeneric<T> where T : class
    {
        
    }
    public class BaseServiceGeneric<T> : IBaseServiceGeneric<T> where T : class
    {
        protected HttpClient HttpClient { get; set; }
        protected string BaseServiceUrl { get; set; }
        //protected readonly IJsonResponseSerializer ResponseSerializer;
        //public BaseServiceGeneric(HttpClient httpClient, string serviceName, IJsonResponseSerializer responseSerializer)
        //{
        //    HttpClient = httpClient;
        //    BaseServiceUrl = $"api/{serviceName}";
        //    ResponseSerializer = responseSerializer;
        //}

    }
}
