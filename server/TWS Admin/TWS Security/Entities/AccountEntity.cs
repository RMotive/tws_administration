using System.ComponentModel.DataAnnotations;

using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using Microsoft.IdentityModel.Tokens;

using TWS_Security.Sets;

namespace TWS_Security.Entities;


/// <summary>
///     Entity for [Account].
///     
///     Defines the Entity concept mirror from a Set concept of [Account].
///     
///     [Account] concept: An account is the definition for a combination of solution login credentials, and
///     preserved information.
/// </summary>
public class AccountEntity
    : BEntity<Account, AccountEntity> {
    #region Properties

    /// <summary>
    ///     Account user identification.
    /// </summary>
    /// 
    [Required]
    public string User { get; private set; }
    /// <summary>
    ///     CRITICAL Security key to identify the account
    /// </summary>
    [Required]
    public byte[] Password { get; private set; }
    /// <summary>
    ///     Defines if the account has no-limit access and can use freely all the features.
    /// </summary>
    [Required]
    public bool Wildcard { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Generates a new <see cref="AccountEntity"/>
    /// </summary>
    /// <param name="User">
    ///     Account user identifier
    /// </param>
    /// <param name="Password">
    ///     Account password sign 
    /// </param>
    public AccountEntity(string User, byte[] Password, bool Wildcard) {
        this.User = User;
        this.Password = Password;
        this.Wildcard = Wildcard;
    }
    /// <summary>
    ///     Creates a new Account entity.
    ///     
    ///     The account entity repersents a data object with information 
    ///     related to a solution account and with this validate permissions
    ///     along the solutions.
    /// </summary>
    /// <param name="Set">
    ///     Set that represents this entity. The data will be populated from it.
    /// </param>
    public AccountEntity(Account Set) {
        Pointer = Set.Id;
        User = Set.User;
        Password = Set.Password;
        Wildcard = Set.Wildcard;
    }

    #endregion

    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container) {
        if (string.IsNullOrWhiteSpace(User))
            Container.Add(nameof(User), IntegrityFailureReasons.NullOrEmptyValue);
        if (Password.IsNullOrEmpty())
            Container.Add(nameof(Password), IntegrityFailureReasons.NullOrEmpty);
        return Container;
    }

    protected override Account Generate() {
        return new() {
            Id = Pointer,
            User = User,
            Password = Password,
        };
    }

    public override bool EqualsSet(Account Set) {
        if (Pointer != Set.Id)
            return false;
        if (User != Set.User)
            return false;
        if (!Password.SequenceEqual(Set.Password))
            return false;

        return true;
    }
}
