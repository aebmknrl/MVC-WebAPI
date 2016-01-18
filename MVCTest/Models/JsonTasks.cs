using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MVCTest.Models
{
    public class JsonTasks
    {
        public HttpResponseMessage parseJson(string txtToParse)
        {
            try
            {
                HttpResponseMessage result = new HttpResponseMessage()
                {
                    Content = new StringContent(txtToParse)
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return result;
            }
            catch (Exception e)
            {
                HttpResponseMessage result = new HttpResponseMessage()
                {
                    Content = new StringContent("{ \"error\": \"" + e.Message + "\"")
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return result;
                throw;
            }

        }
    }
}