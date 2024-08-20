namespace credo_bank.Domain.Models;

public class LogEntry : GenericEntity
{
    public int UserId { get; set; }
    public string RequestBody { get; set; }
    public string ResponseBody { get; set; }
    public string RequestAction { get; set; }
    public DateTime Timestamp { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
    public string Exception { get; set; }
    public string Properties { get; set; }
}