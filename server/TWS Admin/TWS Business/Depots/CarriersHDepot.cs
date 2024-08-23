﻿using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="CarrierH"/> datasource entity mirror.
/// </summary>
public class CarriersHDepot : BSourceDepot<TWSBusinessSource, CarrierH> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="CarrierH"/>.
    /// </summary>
    public CarriersHDepot() : base(new(), null) {
    }
}
