using Foundation.Attributes.Datasources;
using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;
using System.ComponentModel.DataAnnotations;
using TWS_Business.Sets;

namespace TWS_Business.Entities
{
    /// <summary>
    ///     Represents a truck as an entity.
    ///     Stores all the relevant data for a Truck unit.
    /// </summary>
    public class TruckEntity
        : BEntity<Truck, TruckEntity>
    {
        /// <summary>
        ///   Truck VIN number identificator
        /// </summary>
        [Required, Unique, MaxLength(17)]
        public string VIN {  get; set; }

        /// <summary>
        ///   The  pointer to the related Plate set into the datasource
        /// </summary>
        [Required]
        public uint Plate {  get; set; }

        /// <summary>
        ///   The  pointer to the related Manufacturer set into the datasource
        /// </summary>
        [Required]
        public uint Manufacturer { get; set; }

        /// <summary>
        ///   Unique Motor number identificator
        /// </summary>
        [Required, Unique, MaxLength(16)]
        public string Motor {  get; set; }

        /// <summary>
        ///   Unique SCT number identificator
        /// </summary>
        [Required, Unique, MaxLength(25)]
        public string SCT { get; set; }

        /// <summary>
        ///   The pointer to the related Maintenance set into the datasource
        /// </summary>
        [Required, Unique]
        public uint Maintenance { get; set; }

        /// <summary>
        ///   Current operative Situtation of the truck
        /// </summary>
        [Required, MaxLength(20)]
        public string Situation { get; set; }

        /// <summary>
        ///   The pointer to the related Insurances set into the datasource
        /// </summary>
        [Required, Unique]
        public uint Insurances {  get; set; }

        /// <summary>
        ///     Creates a truck entity.
        ///     
        ///     A truck entity represents a truck unit into the business solution.
        ///     
        /// </summary>
        /// <param name="VIN">
        ///     Truck VIN number identiticator.
        /// </param>
        /// <param name="Plate">
        ///     Truck Plate number identificator.
        /// </param>
        /// <param name="Manufacturer">
        ///     The truck manufaturer
        /// </param>
        /// <param name="Motor">
        ///     The Motor number of the truck
        /// </param>
        /// <param name="SCT">
        ///     Truck SCT number
        /// </param>
        /// <param name="Maintenance">
        ///     The Maintenance information
        /// </param>
        /// <param name="Situation">
        ///     The current truck situation
        /// </param>
        /// <param name="Insurances">
        ///     Related insurace to the truck
        /// </param>
        public TruckEntity(string VIN, uint Plate, uint Manufacturer, string Motor, string SCT, uint Maintenance, string Situation, uint Insurances ) { 
            this.VIN = VIN;
            this.Plate = Plate;
            this.Manufacturer = Manufacturer;
            this.Motor = Motor;
            this.SCT = SCT;
            this.Maintenance = Maintenance;
            this.Situation = Situation;
            this.Insurances = Insurances;
        }
        /// <summary>
        ///     Creates a truck entity.
        ///     
        ///     A truck entity represents a truck unit into the business solution.
        ///     
        /// </summary>
        /// <param name="Set">
        ///     Truck Set database representation, will be used to populate this 
        ///     entity object data.
        /// </param>
        public TruckEntity(Truck Set)
        {
            this.Pointer = Set.Id;
            this.VIN = Set.VIN;
            this.Plate = (uint)Set.Plate;
            this.Manufacturer = (uint)Set.Manufacturer;
            this.Motor = Set.Motor;
            this.SCT = Set.SCT;
            this.Maintenance = (uint)Set.Maintenance;
            this.Situation = Set.Situation;
            this.Insurances = (uint)Set.Insurances; 
        }

        protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container){

            if (string.IsNullOrWhiteSpace(VIN))
                Container.Add(nameof(VIN), IntegrityFailureReasons.NullOrEmpty);
            if (Plate <= 0)
                Container.Add(nameof(Plate), IntegrityFailureReasons.DependencyPointer);
            if (Manufacturer <= 0)
                Container.Add(nameof(Manufacturer), IntegrityFailureReasons.DependencyPointer);
            if (string.IsNullOrWhiteSpace(Motor))
                Container.Add(nameof(Motor), IntegrityFailureReasons.NullOrEmpty);
            if (string.IsNullOrWhiteSpace(SCT))
                Container.Add(nameof(SCT), IntegrityFailureReasons.NullOrEmpty);
            if (Maintenance <= 0)
                Container.Add(nameof(Maintenance), IntegrityFailureReasons.DependencyPointer);
            if (Insurances <= 0)
                Container.Add(nameof(Insurances), IntegrityFailureReasons.DependencyPointer);

            return Container;
        }

        protected override Truck Generate(){
            return new Truck{
                Id = Pointer,
                VIN = VIN,
                Plate = (int)Plate,
                Manufacturer = (int)Manufacturer,
                Motor = Motor,
                SCT = SCT,
                Maintenance = (int)Maintenance,
                Situation = Situation,
                Insurances = (int)Insurances
            };
        }

        public override bool EqualsSet(Truck Set)
        {
            if (Pointer != Set.Id) return false;
            if (VIN != Set.VIN) return false;
            if (Plate != (uint)Set.Plate) return false;
            if (Manufacturer != (uint)Set.Manufacturer) return false;
            if (Motor != Set.Motor) return false;
            if (SCT != Set.SCT) return false;
            if (Maintenance != (uint)Set.Maintenance) return false;
            if (Situation != Set.Situation) return false;
            if (Insurances != (uint)Set.Insurances) return false;

            return true;
        }
    }
}
