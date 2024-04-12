using Foundation.Attributes.Datasources;
using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;
using System.ComponentModel.DataAnnotations;
using TWS_Business.Entities;

namespace TWS_Business.Sets
{
    public partial class Insurances
    : BSet<Insurances, InsurancesEntity>
    {
        public override int Id { get; set; }

        [Required,Unique,MaxLength(20)]
        public string Policy { get; set; } = string.Empty;

        [Required]
        public DateTime Expiration { get; set; }

        [Required, MaxLength(30)]
        public string Country { get; set; } = string.Empty;

        protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container)
        {
            if(Id <= 0)
                Container.Add(nameof(Id), IntegrityFailureReasons.DependencyPointer);
            if (string.IsNullOrWhiteSpace(Policy))
                Container.Add(nameof(Id), IntegrityFailureReasons.NullOrEmpty);
            if(string.IsNullOrWhiteSpace(Country))
                Container.Add(nameof(Country),IntegrityFailureReasons.NullOrEmpty);
            return Container;
        }

        protected override InsurancesEntity Generate()
        => new(this);

        public override bool EqualsEntity(InsurancesEntity Entity){
            if (Id != Entity.Pointer) return false;
            if (Policy != Entity.Policy) return false;
            if (Expiration != Entity.Expiration) return false;
            if (Country != Entity.Country) return false;

            return true;
        }


    }
}
