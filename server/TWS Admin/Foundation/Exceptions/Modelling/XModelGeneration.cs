using Foundation.Contracts.Exceptions.Bases;
using Foundation.Contracts.Modelling.Interfaces;
using Foundation.Exceptions.Modelling.Failures;
using Foundation.Models;

namespace Foundation.Exceptions.Modelling;
public class XModelGeneration<TScheme, TModel>
    : BException<XFModelGeneration>
    where TScheme : IScheme<TModel>
    where TModel : IModel {
    const string MESSAGE = "Exception converting a scheme into a model";

    private readonly Type Scheme;
    private readonly List<SchemeConvertionBreakModel> Breaks;

    public XModelGeneration(List<SchemeConvertionBreakModel> breaks)
        : base(MESSAGE) {
        Scheme = typeof(TScheme);
        Breaks = breaks;
    }

    protected override XFModelGeneration DesignFailure()
    => new() {
        Message = MESSAGE,
        Failure = new() {
            {nameof(Scheme), Scheme },
            {nameof(Breaks), Breaks }
        },
    };
}
