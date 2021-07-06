using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using ToDo.DB;

namespace To_Do_api.Controllers
{
    public class GenericController : ApiController
    {
        public RequestInput GetRequestInput()
        {
            RequestInput requestInput = new RequestInput();
            try
            {
                var re = Request;
                var headers = re.Headers;
                try
                {
                    string RawContent = re.Content.ReadAsStringAsync().Result;
                    var jobj = JObject.Parse(RawContent, new JsonLoadSettings());
                    ChangePropertiesToLowerCase(jobj);
                    requestInput.dPO = jobj;
                }
                catch (Exception)
                {
                }
                try
                {
                    requestInput.RequestID = headers.GetValues("RequestID").First();
                }
                catch (Exception)
                {
                    Guid obj = Guid.NewGuid();
                    requestInput.RequestID = obj.ToString();
                    headers.Add("RequestID", requestInput.RequestID);
                }

            }
            catch (Exception ex)
            {

            }
            return requestInput;
        }
        private static void ChangePropertiesToLowerCase(JObject jsonObject)
        {

            foreach (var property in jsonObject.Properties().ToList())
            {
                if (property.Value.Type == JTokenType.Object)// replace property names in child object
                    ChangePropertiesToLowerCase((JObject)property.Value);

                property.Replace(new JProperty(property.Name.ToLower(), property.Value));// properties are read-only, so we have to replace them
            }
        }
       
        public string GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }
    }
}
