namespace Customer.Contracts.Models;
public interface IPrivateModel<TPublicModel>
    where TPublicModel : class, new() {
    public TPublicModel GeneratePublicDerivation();
}
