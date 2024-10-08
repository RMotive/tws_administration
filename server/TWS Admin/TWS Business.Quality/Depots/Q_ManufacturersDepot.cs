﻿using Foundation.Migrations.Quality.Bases;
using Foundation.Utils;
using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="ManufacturersDepot"/>.
/// </summary>
public class Q_ManufacturersDepot
    : BQ_MigrationDepot<Manufacturer, ManufacturersDepot, TWSBusinessSource> {
    public Q_ManufacturersDepot()
        : base(nameof(Manufacturer.Brand)) {
    }

    protected override Manufacturer MockFactory() {
        DateOnly date = new(2024, 12, 12);

        return new() {
            Model = RandomUtils.String(30),
            Brand = RandomUtils.String(15),
            Year = date
        };
    }
}