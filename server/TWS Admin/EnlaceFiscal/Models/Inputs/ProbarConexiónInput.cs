using System.Text.Json.Serialization;

using EnlaceFiscal.Core.Enums;

namespace EnlaceFiscal.Models.Inputs;
public class ProbarConexiónInput {
    [JsonPropertyName("modo")]
    required public string Modo { get; init; }
    [JsonPropertyName("rfc")]
    required public string RFC { get; init; }
    [JsonPropertyName("accion"), JsonInclude]
    required public string Accion = "probarConexion";
}
