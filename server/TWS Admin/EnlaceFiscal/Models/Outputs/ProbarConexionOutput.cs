using System.Text.Json.Serialization;

namespace EnlaceFiscal.Models.Outputs;
public class ProbarConexionOutput {
    [JsonPropertyName("numeroReferencia")]
    required public int NumeroReferencia { get; init; }

    [JsonPropertyName("estatusDocumento")]
    required public string EstatusDocumento {  get; init; }

    [JsonPropertyName("fechaMensaje")]
    required public string FechaMensaje { get; init; }

    [JsonPropertyName("version")]
    required public string Version {  get; init; }


    public DateTime Fecha {
        get {
            return DateTime.Parse(FechaMensaje);
        }
    }
}
