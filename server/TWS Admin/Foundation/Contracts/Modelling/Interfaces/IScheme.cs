namespace Foundation.Contracts.Modelling.Interfaces;
public interface IScheme<TModel>
    where TModel : IModel {
    public TModel GenerateModel();
}
