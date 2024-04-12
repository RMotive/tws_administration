using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;
using System.ComponentModel.DataAnnotations;
using TWS_Business.Entities;

namespace TWS_Business.Sets
{
    public partial class Plates
    : BSet<Plates, PlatesEntity>
    {
        public override int Id { get; set; }

        [Required, MaxLength(12)]
        public string Identifier { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string State { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string Country { get; set; } = string.Empty;

        [Required]
        public DateTime Expiration { get; set; }

        protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container){
            if(Id <= 0)
                Container.Add(nameof(Id), IntegrityFailureReasons.DependencyPointer);
            if (string.IsNullOrWhiteSpace(Identifier))
                Container.Add(nameof(Identifier), IntegrityFailureReasons.NullOrEmpty);
            if (string.IsNullOrWhiteSpace(State))
                Container.Add(nameof(State), IntegrityFailureReasons.NullOrEmpty);
            if(string.IsNullOrWhiteSpace(Country))
                Container.Add(nameof(Country), IntegrityFailureReasons.NullOrEmpty);

            return Container;
        }
        protected override PlatesEntity Generate()
        => new(this);
        public override bool EqualsEntity(PlatesEntity Entity){
            if (Id != Entity.Pointer) return false;
            if (Identifier != Entity.Identifier) return false;
            if (State != Entity.State) return false;
            if (Country != Entity.Country) return false;
            if (Expiration != Entity.Expiration) return false;

            return true;
        }

        

      
    }
}
