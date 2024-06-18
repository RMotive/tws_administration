

using Foundation.Migrations.Records;
using TWS_Security.Sets;

namespace Customer.Services.Interfaces;
public interface IContactService {
    Task<MigrationTransactionResult<Contact>> Create(Contact[] contact);
}
