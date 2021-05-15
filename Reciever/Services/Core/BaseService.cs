using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reciever.Services.Core
{   
    public class BaseService
    {
        protected HttpClient HttpClient { get; set; }
        public BaseService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }        
    }
}
