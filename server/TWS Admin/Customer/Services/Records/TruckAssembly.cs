using TWS_Business.Sets;

namespace TWS_Customer.Services.Records;
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
    /// Generate a new insert into the Manufacturer table, in the data source.
    /// This property has a higher prority level over the [ManufacturerPointer] property.
    /// If [Id] property is set a higher value than 0, then this field will be assingned using that pointer.
    /// </summary>
    public required Manufacturer Manufacturer { get; set; }

    /// <summary>
    /// then generate a new insert into the Plate table, in the data source, based in the list lenght.
    /// This property has a higher prority level over the [PlatePointer] property.
    /// If [Id] property is set a higher value than 0, then this field will be assingned using that pointer.
    /// </summary>
    public required List<Plate> Plates { get; set; }

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