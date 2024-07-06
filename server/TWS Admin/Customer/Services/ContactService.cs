
using Customer.Services.Interfaces;
using CSMFoundation.Migration.Records;
using TWS_Security.Depots;
using TWS_Security.Sets;

namespace Customer.Services;
public class ContactService
    : IContactService {

    readonly ContactsDepot Contacts;
    public ContactService(ContactsDepot contacts) {
        this.Contacts = contacts;
    }
    public async Task<MigrationTransactionResult<Contact>> Create(Contact[] contact) {
        return await this.Contacts.Create(contact);
    }
}
