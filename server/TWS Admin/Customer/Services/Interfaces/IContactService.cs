using CSM_Foundation.Source.Models.Out;

using TWS_Security.Sets;

namespace TWS_Customer.Services.Interfaces;
public interface IContactService {
    Task<SourceTransactionOut<Contact>> Create(Contact[] contact);
}
