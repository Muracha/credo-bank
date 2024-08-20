using credo_bank.Domain.Models;
using credo_bank.Infrastructure.Repositories.Interfaces;
using Serilog.Events;

namespace credo_bank.Middleware.Events;

public class EfCoreSink : Serilog.Sinks.PeriodicBatching.IBatchedLogEventSink
{
    public async Task EmitBatchAsync(IEnumerable<LogEvent> batch)
    {
        foreach (var logEvent in batch)
        {
            var rednerMessage = logEvent.RenderMessage();

            if (rednerMessage.StartsWith("Credo"))
            {
                var log = new LogEntry
                {
                    RequestBody = logEvent.Properties.ContainsKey("RequestBody") ? logEvent.Properties["RequestBody"].ToString() : "",
                    ResponseBody = logEvent.Properties.ContainsKey("ResponseBody") ? logEvent.Properties["ResponseBody"].ToString() : "",
                    RequestAction = $"{logEvent.Properties["Method"]} {logEvent.Properties["Url"]} => {logEvent.Properties["StatusCode"]}",
                    UserId = Convert.ToInt32(logEvent.Properties["UserId"].ToString()),
                    Timestamp = logEvent.Timestamp.UtcDateTime,
                    Level = logEvent.Level.ToString(),
                    Message = rednerMessage,
                    Exception = logEvent.Exception?.ToString(),
                    Properties = logEvent.Properties != null ? System.Text.Json.JsonSerializer.Serialize(logEvent.Properties) : null
                };
            }
        }
    }

    public Task OnEmptyBatchAsync()
    {
        return Task.CompletedTask;
    }
}