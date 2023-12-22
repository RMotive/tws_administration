using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;
using Foundation.Exceptions.Datasources;

using Microsoft.IdentityModel.Tokens;

using System.ComponentModel.DataAnnotations;

using TWS_Security.Sets;

namespace TWS_Security.Entities;
/// <summary>
///     Represents an Account entity based on a datasource set, works as an interface
///     between bussines and datasource to create a soft validation entity.
///     
///     The account is used to represent a solution user, it is the mirror representation 
///     of a logical solutions user, anything that have their own permits and information
///     to use the bussines solution features.
/// </summary>
public class AccountEntity
    : BDatasourceEntity<Account, AccountEntity>
{
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
    ///     Creates a new Account entity.
    ///     
    ///     The account entity repersents a data object with information 
    ///     related to a solution account and with this validate permissions
    ///     along the solutions.
    /// </summary>
    /// <param name="User">
    ///     Account user identifier
    /// </param>
    /// <param name="Password">
    ///     Account password sign 
    /// </param>
    public AccountEntity(string User, byte[] Password)
    {
        this.User = User;
        this.Password = Password;
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
    public AccountEntity(Account Set)
    {
        this.Pointer = Set.Id;
        this.User = Set.User;
        this.Password = Set.Password;
    }

    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container)
    {
        if (String.IsNullOrWhiteSpace(User))
            Container.Add(nameof(User), IntegrityFailureReasons.NullOrEmptyValue);
        if (Password.IsNullOrEmpty())
            Container.Add(nameof(Password), IntegrityFailureReasons.NullOrEmptyValue);
        return Container;
    }

    protected override Account GenerateSet()
    {
        return new()
        {
            Id = Pointer,
            User = User,
            Password = Password,
        };
    }

    public override bool EvaluateSet(Account Set)
    {
        if (Pointer != Set.Id)
            return false;
        if (User != Set.User)
            return false;
        if (Password != Set.Password)
            return false;

        return true;
    }
}
