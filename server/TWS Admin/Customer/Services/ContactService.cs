
using Customer.Services.Interfaces;
using TWS_Security.Depots;
using TWS_Security.Sets;
using CSMFoundation.Source.Models.Out;

namespace Customer.Services;
public class ContactService
    : IContactService {

    readonly ContactsDepot Contacts;
    public ContactService(ContactsDepot contacts) {
        this.Contacts = contacts;
    }
    public async Task<SourceTransactionOut<Contact>> Create(Contact[] contact) {
        return await this.Contacts.Create(contact);
    }
}
