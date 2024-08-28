﻿using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="TruckExternal"/> datasource entity mirror.
/// </summary>
public class TrucksExternalsDepot : BSourceDepot<TWSBusinessSource, TruckExternal> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="TruckExternal"/>.
    /// </summary>
    public TrucksExternalsDepot() : base(new(), null) {
    }
}