using EnlaceFiscal.Models.Outputs;

namespace EnlaceFiscal;
public interface IEnlaceFiscal {
    Task<OAckEnlaceFiscal<ProbarConexionOutput>> ProbarConexión();
}
