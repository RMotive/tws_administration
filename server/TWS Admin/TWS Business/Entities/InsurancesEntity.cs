using Foundation.Attributes.Datasources;
using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;
using System.ComponentModel.DataAnnotations;
using TWS_Business.Sets;

namespace TWS_Business.Entities
{
    /// <summary>
    ///     Represents a data entity of Insurance into the business.
    ///     Stores all the relevant data for an Insurance.
    /// </summary>
    public class InsurancesEntity
        : BEntity<Insurances,InsurancesEntity>{

        /// <summary>
        /// Policy Number for the insurance
        /// </summary>
        [Required, Unique, MaxLength(20)]
        public string Policy { get; set; } = string.Empty;
        /// <summary>
        /// Expiration Date for the insurance
        /// </summary>
        [Required]
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Country of the insurance 
        /// </summary>
        [Required, MaxLength(30)]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        ///     Create a Insurances entity.
        ///     
        ///     An insurance entity represents a insurances for 
        ///     other entities into the business solution.
        /// </summary>
        /// <param name="policy">
        ///     The Policy number.
        /// </param>
        /// <param name="expiration">
        ///     Insurances Expiration Date
        /// </param>
        /// <param name="country">
        ///     Insurance country
        /// </param>
        public InsurancesEntity(string policy, DateTime expiration, string country)
        {
            this.Policy = policy;
            this.Expiration = expiration;
            this.Country = country;
        }
        /// <summary>
        ///     Create a Insurances entity.
        ///     
        ///     An insurance entity represents a insurances for 
        ///     other entities into the business solution.
        /// </summary>
        /// <param name="Set">
        ///     Insurance Set database representation, will be used to populate this 
        ///     entity object data.
        /// </param>
        public InsurancesEntity(Insurances Set) {
            this.Pointer = Set.Id;
            this.Policy = Set.Policy;
            this.Expiration = Set.Expiration;
            this.Country = Set.Country;
        }
        protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container)
        {
            if (string.IsNullOrWhiteSpace(Policy))
                Container.Add(nameof(Policy),IntegrityFailureReasons.NullOrEmpty);
            if (string.IsNullOrWhiteSpace(Country))
                Container.Add(nameof(Country),IntegrityFailureReasons.NullOrEmpty);

            return Container;
        }
        protected override Insurances Generate(){
            return new Insurances
            {
                Id = Pointer,
                Policy = Policy,
                Expiration = Expiration,
                Country = Country
            };
        }
        public override bool EqualsSet(Insurances Set)
        {
            if (Pointer != Set.Id) return false;
            if (Policy != Set.Policy) return false;
            if (Expiration != Set.Expiration) return false; 
            if (Country != Set.Country) return false;

            return true;
        }
    }
}
