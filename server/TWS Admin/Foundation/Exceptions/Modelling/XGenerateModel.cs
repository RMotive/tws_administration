using Foundation.Contracts.Exceptions;
using Foundation.Contracts.Modelling.Bases;
using Foundation.Contracts.Modelling.Interfaces;
using Foundation.Models;

namespace Foundation.Exceptions.Modelling;
public class XGenerateModel<TScheme, TModel>
    : BException
    where TScheme : BScheme<TModel>
    where TModel : IModel
{
    private readonly Type Scheme;
    private readonly List<SchemeConvertionBreakModel> Breaks;

    public XGenerateModel(List<SchemeConvertionBreakModel> breaks)
        : base("Exception converting a scheme into a model")
    {
        Scheme = typeof(TScheme);
        Breaks = breaks;
    }

    public override Dictionary<string, dynamic> ToDisplay()
    {
        string breaksToDisplay = "";
        for (int pointer = 0; pointer < Breaks.Count; pointer++)
        {
            breaksToDisplay += $"({pointer}) [{Breaks[pointer].Property}]: [{Breaks[pointer].Reason}]";
        }


        return new Dictionary<string, dynamic>
        {
            {nameof(Scheme), Scheme.ToString()},
            {nameof(Breaks), breaksToDisplay }
        };
    }
}
