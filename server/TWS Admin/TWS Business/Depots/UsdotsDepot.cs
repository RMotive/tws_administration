﻿using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Usdot"/> datasource entity mirror.
/// </summary>
public class UsdotsDepot : BSourceDepot<TWSBusinessSource, Usdot> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Usdot"/>.
    /// </summary>
    public UsdotsDepot() : base(new(), null) {
    }
}
