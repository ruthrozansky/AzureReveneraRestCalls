using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace AzureReveneraRestCalls
{
    public static class Function1
    {

        [FunctionName("GetDevicesBySoldToAcctId")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
             ExecutionContext context, ILogger log)
        {

            //parameters
            string customerId = req.Query["customerId"];

            //Read Config values
            var config = new ConfigurationBuilder()
              .SetBasePath(context.FunctionAppDirectory)
              // This gives you access to your application settings in your local development environment
              .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
              // This is what actually gets you the application settings in Azure
              .AddEnvironmentVariables()
              .Build();
            string URI = "https://flex1369-uat.flexnetoperations.com/flexnet/operations/manageDevice/2.0/devices"; // config["URI"];
            string username = "ruthrozansky@eaton.com"; // config["UserId"];
            string password = "Vista7605!"; // config["Password"];

            ReqDevice.Root root = new ReqDevice.Root();
            root.batchSize = 10;
            root.pageNumber = 1;
            ReqDevice.QueryParams qry = new ReqDevice.QueryParams();
            ReqDevice.SoldToAcctId soldTo = new ReqDevice.SoldToAcctId();
            soldTo.searchType = "EQUALS";
            soldTo.value = customerId;
            qry.soldToAcctId = soldTo;
            qry.hosted = true;
            root.queryParams = qry;
            string strJson = JsonConvert.SerializeObject(root);

            //call post method
            var request = (HttpWebRequest)WebRequest.Create(URI);
            request.Method = "POST";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8;";
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(strJson);
                streamWriter.Flush();
            }

            dynamic content = "";
            try
            {
                HttpWebResponse resp = request.GetResponse() as HttpWebResponse;
                if (resp.StatusDescription == "OK")
                {
                    using (StreamReader Reader = new StreamReader(resp.GetResponseStream()))
                    {
                        content = JsonConvert.DeserializeObject(Reader.ReadToEnd());
                        
                    }
                }
                resp.Close();
                // Releases the resources of the Stream.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new OkObjectResult(content);
        }
    }
}
