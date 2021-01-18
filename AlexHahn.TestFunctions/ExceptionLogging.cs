using System;
using System.Collections.Generic;
using Domain.Exceptions.Critical;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
                throw new CriticalException();
            }
            catch (Exception e)
            {
                log.LogError("Argument Exception Thrown");
                _telemetryClient.TrackException(e, new Dictionary<string, string> {{"name", "customException"}});
            }

            const string retStr = "Function Ran";
            return retStr;
        }
    }
}