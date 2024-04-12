using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;
using System.ComponentModel.DataAnnotations;
using TWS_Business.Sets;

namespace TWS_Business.Entities
{  /// <summary>
   ///     Represents a Plate as an entity.
   ///     Stores all the relevant Data for a Plate.
   /// </summary>
    public class PlatesEntity
    : BEntity<Plates, PlatesEntity>
    {
        /// <summary>
        /// Plate number idenficator
        /// </summary>
        [Required, MaxLength(12)]
        public string Identifier { get; set; }

        /// <summary>
        /// Origin state of the plate
        /// </summary>
        [Required, MaxLength(30)]
        public string State { get; set; }

        /// <summary>
        /// Country of the plate
        /// </summary>
        [Required, MaxLength(30)]
        public string Country { get; set; }

        /// <summary>
        ///  Plate expiration date
        /// </summary>
        [Required]
        public DateTime Expiration { get; set; }
        
        /// <summary>
        ///     Creates a plate entity.
        ///     
        ///     A plate entity represents a plate for entities into the business solution.   
        /// </summary>
        /// <param name="Identifier">
        ///     Plate Number
        /// </param>
        /// <param name="State">
        ///     Plate Origin state
        /// </param>
        /// <param name="Country">
        ///     Plate Country
        /// </param>
        /// <param name="Expiration">
        ///     Plate Expiration Date
        /// </param>
        public PlatesEntity(string Identifier, string State, string Country, DateTime Expiration) { 
            this.Identifier = Identifier;
            this.State = State;
            this.Country = Country;
            this.Expiration = Expiration;
        }

        /// <summary>
        ///     Creates a plate entity.
        ///     
        ///     A plate entity represents a plate for entities into the business solution. 
        /// </summary>
        /// <param name="Set">
        ///     Truck Set database representation, will be used to populate this 
        ///     entity object data.
        /// </param>
        public PlatesEntity(Plates Set)
        {
            this.Pointer = Set.Id; 
            this.Identifier = Set.Identifier;
            this.State = Set.State;
            this.Country = Set.Country;
            this.Expiration = Set.Expiration;
        }

        protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container)
        {
            if (string.IsNullOrWhiteSpace(Identifier))
                Container.Add(nameof(Identifier), IntegrityFailureReasons.NullOrEmpty);
            if (string.IsNullOrWhiteSpace(State))
                Container.Add(nameof(State), IntegrityFailureReasons.NullOrEmpty);
            if (string.IsNullOrWhiteSpace(Country))
                Container.Add(nameof(Country), IntegrityFailureReasons.NullOrEmpty);

            return Container;
        }

        public override bool EqualsSet(Plates Set){
            if (Pointer != Set.Id) return false;
            if (Identifier != Set.Identifier) return false;
            if (State != Set.State) return false;
            if (Country != Set.Country) return false;
            if (Expiration != Set.Expiration) return false;

            return true;
         }

        protected override Plates Generate(){
            return new Plates{
                Id = Pointer,
                Identifier = Identifier,
                State = State,
                Country = Country,
                Expiration = Expiration
            };
            
        }
    }
}
