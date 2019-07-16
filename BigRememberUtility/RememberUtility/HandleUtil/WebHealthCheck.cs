using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSampleCode.HandleUtil
{
    public static class WebHealthCheck
    {
        public static string TestWebSite(string website)
        {
            try
            {
                var request = WebRequest.Create(website);
                var response = (HttpWebResponse)request.GetResponse();

                return response.StatusCode.ToString();
            }
            catch
            {
                return "404";
            }
        }
    }
}
