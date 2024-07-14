
using CSM_Foundation.Source.Models.Out;

using TWS_Customer.Services.Interfaces;

using TWS_Security.Depots;
using TWS_Security.Sets;

namespace TWS_Customer.Services;
public class ContactService
    : IContactService {
    private readonly ContactsDepot Contacts;
    public ContactService(ContactsDepot contacts) {
        Contacts = contacts;
    }
    public async Task<SourceTransactionOut<Contact>> Create(Contact[] contact) {
        return await Contacts.Create(contact);
    }
}
