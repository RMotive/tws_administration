using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;
using System.ComponentModel.DataAnnotations;
using TWS_Business.Entities;

namespace TWS_Business.Sets
{
    public partial class Maintenance
        : BSet<Maintenance, MaintenanceEntity>{
        public override int Id { get; set; }

        [Required]
        public DateTime Anual { get; set; }

        [Required]
        public DateTime Trimestral { get; set; }
        public override bool EqualsEntity(MaintenanceEntity Entity){
            if (Id != Entity.Pointer) return false;
            if (Anual != Entity.Anual) return false;
            if (Trimestral != Entity.Trimestral) return false;

            return true;
        }

        protected override MaintenanceEntity Generate()
        => new(this);

        protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container){
            return Container;
        }
    }
}
