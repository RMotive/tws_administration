namespace Server.Quality.Schemes;
public class FailureTemplateScheme<TFailure> {
    public Guid Tracer { get; set; }
    public TFailure? Estela { get; set; }

    public FailureTemplateScheme() {

    }
}
