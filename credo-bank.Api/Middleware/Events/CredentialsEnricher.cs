using System.Text.RegularExpressions;
using credo_bank.Application.Models.DTO.Helper;
using Newtonsoft.Json;
using Serilog.Core;
using Serilog.Events;

namespace credo_bank.Middleware.Events;

public class CredentialsEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Properties.ContainsKey("RequestBody"))
        {
            var originalRequestBody = logEvent.Properties["RequestBody"].ToString();
            var maskedRequestBody = MaskLoginCredentials(originalRequestBody);
            logEvent.RemovePropertyIfPresent("RequestBody");
            logEvent.AddPropertyIfAbsent(new LogEventProperty("RequestBody", new ScalarValue(maskedRequestBody)));
        }

        if (logEvent.Properties.ContainsKey("ResponseBody"))
        {
            var originalResponseBody = logEvent.Properties["ResponseBody"].ToString();
            var maskedResponseBody = MaskLoginCredentials(originalResponseBody);
            logEvent.RemovePropertyIfPresent("ResponseBody");
            logEvent.AddPropertyIfAbsent(new LogEventProperty("ResponseBody", new ScalarValue(maskedResponseBody)));
        }
    }

    private object MaskLoginCredentials(string originalData)
    {
        var jsonPattern = @"\b(?:password)\b";

        if (Regex.IsMatch(originalData, jsonPattern, RegexOptions.IgnoreCase))
        {
            var intermediate = JsonConvert.DeserializeObject<string>(originalData);
            var deserializedData = JsonConvert.DeserializeObject<LoginDeserialazingData>(intermediate);

            deserializedData.Password = "*****";

            originalData = JsonConvert.SerializeObject(deserializedData);
        }

        return originalData;
    }
}