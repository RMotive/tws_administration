using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;
using System.ComponentModel.DataAnnotations;
using TWS_Business.Sets;

namespace TWS_Business.Entities
{
    /// <summary>
    ///     Represents a data entity of Maintenance into the business.
    ///     Stores all the relevant data of Maintenance.
    /// </summary>
    public class MaintenanceEntity
    :BEntity<Maintenance,MaintenanceEntity>{
        /// <summary>
        /// Anual Maintenance Date
        /// </summary>
        [Required]
        public DateTime Anual {  get; set; }
        
        /// <summary>
        /// Trimestral Maintenances Date
        /// </summary>
        [Required]
        public DateTime Trimestral { get; set; }
        /// <summary>
        ///     Create a Maintenance entity
        ///     
        ///     A maintenance entity represents the maintenance programation
        ///     for other entities into the business solution.
        /// </summary>
        /// <param name="Anual">
        ///     Anual maintenance date
        /// </param>
        /// <param name="Trimestral">
        ///     Trimestral maintenance date
        /// </param>
        public MaintenanceEntity(DateTime Anual, DateTime Trimestral) { 
            this.Anual = Anual;
            this.Trimestral = Trimestral;
        }

        /// <summary>
        ///     Create a Maintenance entity
        ///     
        ///     A maintenance entity represents the maintenance programation
        ///     for other entities into the business solution.
        /// </summary>
        /// <param name="Set">
        ///     Maintenance Set database representation, will be used to populate this 
        ///     entity object data.
        /// </param>
        public MaintenanceEntity(Maintenance Set) {
            this.Pointer = Set.Id;
            this.Anual = Set.Anual;
            this.Trimestral = Set.Trimestral;
        }
        protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container){
            return Container;
        }
        public override bool EqualsSet(Maintenance Set){
            if(Pointer != Set.Id) return false;
            if(Anual != Set.Anual) return false;
            if(Trimestral != Set.Trimestral) return false;

            return true;
        }

        protected override Maintenance Generate(){
            return new Maintenance{
                Id = Pointer,
                Anual = Anual,
                Trimestral = Trimestral
            };
        }
    }
}
