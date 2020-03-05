using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeDID.Common
{
    public static class NetworkOptions
    {
        public static string serverUrl { get; set; } = "http://did.dodam.b1nd.com";
        public static int timeOut { get; set; } = 30000;
    }
}
