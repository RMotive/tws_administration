using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;
using System.ComponentModel.DataAnnotations;
using TWS_Business.Sets;

namespace TWS_Business.Entities
{
    /// <summary>
    ///     Represents a Manufacturer as an entity.
    ///     Stores all the relevant data for a Manufacturer.
    /// </summary>
    public class ManufacturersEntity
    : BEntity<Manufacturers, ManufacturersEntity>
    {
        /// <summary>
        ///     Specific model
        /// </summary>
        [Required, MaxLength(30)]
        public string Model { get; set; }

        /// <summary>
        ///     Manufacturer Brand
        /// </summary>
        [Required, MaxLength(15)]
        public string Brand { get; set; }

        /// <summary>
        ///     Year of the model
        /// </summary>
        [Required]
        public DateTime Year { get; set; }

        /// <summary>
        ///     Creates a Manufacturers entity.
        ///     
        ///     A Manufacturer entity represents a Manufacturer for other 
        ///     entities into the business solution.
        /// </summary>
        /// <param name="Model">
        ///     Specifici model
        /// </param>
        /// <param name="Brand">
        ///     Manufacterer Brand 
        /// </param>
        /// <param name="Year">
        ///    Year of the model
        /// </param>
        public ManufacturersEntity(string Model, string Brand, DateTime Year) { 
            this.Model = Model;
            this.Brand = Brand;
            this.Year = Year;
        }
        protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container){

            if (string.IsNullOrWhiteSpace(Model))
                Container.Add(nameof(Model), IntegrityFailureReasons.NullOrEmpty);
            if (string.IsNullOrWhiteSpace(Brand))
                Container.Add(nameof(Brand), IntegrityFailureReasons.NullOrEmpty);

            return Container;
        }
        public ManufacturersEntity(Manufacturers Set) {
            this.Pointer = Set.Id;
            this.Model = Set.Model;
            this.Brand = Set.Brand;
            this.Year = Set.Year;
        }
        public override bool EqualsSet(Manufacturers Set){
            if (Pointer != Set.Id) return false;
            if (Model != Set.Model) return false;
            if (Brand != Set.Brand) return false;
            if (Year != Set.Year) return false;

            return true;
        }

        protected override Manufacturers Generate(){
            return new Manufacturers{
                Id = Pointer,
                Model = Model,
                Brand = Brand,
                Year = Year
            };
        }

        
    }
}
