using CSMFoundation.Source.Models.Out;
using TWS_Security.Sets;

namespace Customer.Services.Interfaces;
public interface IContactService {
    Task<SourceTransactionOut<Contact>> Create(Contact[] contact);
}
