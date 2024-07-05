namespace EnlaceFiscal.Models.Inputs;
public class OSolicitud<T> {
    public T Solicitud { get; set; }

    public OSolicitud(T Solicitud) {
        this.Solicitud = Solicitud; 
    }
}
