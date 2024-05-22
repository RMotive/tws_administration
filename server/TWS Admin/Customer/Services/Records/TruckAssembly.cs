using TWS_Business.Sets;

namespace Customer.Services.Records;
/// <summary>
/// Class that stores all the optional and required values to generate a truck entity.
/// </summary>
public class TruckAssembly {
    /// <summary>
    /// Vin number for the truck.
    /// </summary>
    public required string Vin { get; set; }

    /// <summary>
    /// Motor Number for the truck.
    /// </summary>
    public required string Motor { get; set; }

    /// <summary>
    /// Optional property. If this property is not null, 
    /// then generate a new insert into the Manufacturer table, in the data source.
    /// This property has a higher prority level over the [ManufacturerPointer] property.
    /// </summary>
    public Manufacturer? Manufacturer { get; set; }

    /// <summary>
    /// Optional property. If this property is not null and [Manufacturer] property is null,
    /// then it use the pointer value to assign/relate this truck with the appropriate [Manufacturer] table value.
    /// (Use this property in case that the Manufacturer data already exist in the data source).

    /// </summary>
    public int? ManufacturerPointer { get; set; }


    /// <summary>
    /// Optional property. If this property is not null, 
    /// then generate a new insert into the Plate table, in the data source, based in the list lenght.
    /// This property has a higher prority level over the [PlatePointer] property.
    /// </summary>
    public List<Plate>? Plates { get; set; }

    /// <summary>
    /// Optional property. If this property is not null and [Manufacturer] property is null,
    /// then it use the pointer value list to assign/relate this truck with the appropriate [Plate] table value.
    /// (Use this property in case that the plate data already exist in the data source).
    /// </summary>
    public List<int>? PlatePointer { get; set; }

    /// <summary>
    /// Optional Maintenance data for the truck.
    /// </summary>
    public Maintenance? Maintenance { get; set; }

    /// <summary>
    /// Optional Situation data for the truck.
    /// </summary>
    public Situation? Situation { get; set; }

    /// <summary>
    /// Optional Insurance data for the truck.
    /// </summary>
    public Insurance? Insurance { get; set; }

    /// <summary>
    /// Optional Sct data for the truck.
    /// </summary>
    public Sct? Sct { get; set; }


}