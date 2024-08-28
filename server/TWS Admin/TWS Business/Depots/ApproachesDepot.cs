﻿using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Approach"/> datasource entity mirror.
/// </summary>
public class ApproachesDepot : BSourceDepot<TWSBusinessSource, Approach> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Approach"/>.
    /// </summary>
    public ApproachesDepot() : base(new(), null) {
    }
}