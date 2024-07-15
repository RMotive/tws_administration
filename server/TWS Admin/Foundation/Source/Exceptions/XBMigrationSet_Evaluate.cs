namespace CSM_Foundation.Source.Exceptions;
public class XBMigrationSet_Evaluate
    : Exception {

    public Type Set;
    public (string Property, XIValidator_Evaluate[])[] Unvalidations;

    public XBMigrationSet_Evaluate(Type Set, (string Property, XIValidator_Evaluate[])[] Unvalidations)
        : base($"EvaluateRead failed for ({Set}) with ({Unvalidations.Length}) faults. Sample | [{Unvalidations[0].Property}]") {
        this.Set = Set;
        this.Unvalidations = Unvalidations;
    }
}
