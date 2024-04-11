using Foundation.Attributes.Datasources;
using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;
using System.ComponentModel.DataAnnotations;
using TWS_Business.Entities;

namespace TWS_Business.Sets
{
    public partial class Truck
        :BSet<Truck,TruckEntity>{

        public override int Id {  get; set; }

        [Required,Unique, MaxLength(17)]
        public string VIN { get; set; } = string.Empty;

        [Required]
        public int Plate { get; set; }

        [Required]
        public int Manufacturer { get; set; }

        [Required, Unique, MaxLength(16)]
        public string Motor { get; set; } = string.Empty;

        [Required, Unique, MaxLength(25)]
        public string SCT { get; set; } = string.Empty;

        [Required, Unique]
        public int Maintenance { get; set; }

        [Required, MaxLength(20)]
        public string Situation { get; set; } = string.Empty;

        [Required, Unique]
        public int Insurances { get; set; }

        protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container){
            if(Id <= 0)
                Container.Add(nameof(Id), IntegrityFailureReasons.LessOrEqualZero);
            if (string.IsNullOrWhiteSpace(VIN))
                Container.Add(nameof(VIN), IntegrityFailureReasons.NullOrEmpty);
            if(Plate <= 0)
                Container.Add(nameof(Plate),IntegrityFailureReasons.LessOrEqualZero);
            if (Manufacturer <= 0)
                Container.Add(nameof(Manufacturer), IntegrityFailureReasons.DependencyPointer);
            if(string.IsNullOrWhiteSpace(Motor))
                Container.Add(nameof(Motor), IntegrityFailureReasons.NullOrEmpty);
            if (string.IsNullOrWhiteSpace(SCT))
                Container.Add(nameof(SCT), IntegrityFailureReasons.NullOrEmpty);
            if (Maintenance <= 0)
                Container.Add(nameof(Maintenance), IntegrityFailureReasons.DependencyPointer);
            if(Insurances <= 0)
                Container.Add(nameof(Insurances),IntegrityFailureReasons.DependencyPointer);
            return Container;
        }

        protected override TruckEntity Generate()
         => new(this);
        public override bool EqualsEntity(TruckEntity Entity)
        {
            if (Id != Entity.Pointer) return false;
            if (VIN != Entity.VIN) return false;
            if (Plate != Entity.Plate) return false;
            if (Manufacturer != Entity.Manufacturer) return false;
            if (Motor != Entity.Motor) return false;
            if (SCT != Entity.SCT) return false;
            if (Maintenance != Entity.Maintenance) return false;
            if (Situation != Entity.Situation) return false;
            if (Insurances != Entity.Insurances) return false;
                  
            return true;
        }
    }
}
