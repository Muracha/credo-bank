using Newtonsoft.Json;

namespace credo_bank.Application.Models.DTO.Helper;

public class LoginDeserialazingData
{
    [JsonProperty("identificationNumber")]
    public string IdentificationNumber { get; set; } = string.Empty;

    [JsonProperty("password")]
    public string Password { get; set; } = string.Empty;
}