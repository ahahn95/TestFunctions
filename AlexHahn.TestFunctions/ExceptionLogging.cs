using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlexHahn.TestFunctions
{
    public class ExceptionLogging
    {
        private readonly TelemetryClient _telemetryClient;

        public ExceptionLogging(TelemetryConfiguration telemetryConfiguration)
        {
            _telemetryClient = new TelemetryClient(telemetryConfiguration);
        }

        [FunctionName("ExceptionLogging")]
        public string Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                if (true)
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception e)
            {
                log.LogError("Argument Exception Thrown");
                //_telemetryClient.TrackException(e);
            }

            const string retStr = "Function Ran"; 
            return retStr;
        }
    }
}