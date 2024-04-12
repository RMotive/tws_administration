using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;
using System.ComponentModel.DataAnnotations;
using TWS_Business.Entities;

namespace TWS_Business.Sets
{
    public partial class Manufacturers
    : BSet<Manufacturers, ManufacturersEntity>
    {
        public override int Id { get; set; }

        [Required, MaxLength(30)]
        public string Model { get; set; } = string.Empty;

        [Required, MaxLength(15)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public DateTime Year { get; set; }

        protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container)
        {
            if(Id <= 0)
                Container.Add(nameof(Id), IntegrityFailureReasons.DependencyPointer);
            if (string.IsNullOrWhiteSpace(Model))
                Container.Add(nameof(Model), IntegrityFailureReasons.NullOrEmpty);
            if (string.IsNullOrWhiteSpace(Brand))
            if (string.IsNullOrWhiteSpace(Brand))
                Container.Add(nameof(Brand), IntegrityFailureReasons.NullOrEmpty);

            return Container;
        }
        protected override ManufacturersEntity Generate()
        => new(this);
        public override bool EqualsEntity(ManufacturersEntity Entity){
            if (Id != Entity.Pointer) return false;
            if (Model != Entity.Model) return false;
            if (Brand != Entity.Brand) return false;
            if (Year != Entity.Year) return false;

            return true;
        }

       

       
    }
}
