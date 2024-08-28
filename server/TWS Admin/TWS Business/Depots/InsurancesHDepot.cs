﻿using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="InsurancesHDepot"/> datasource entity mirror.
/// </summary>
public class InsurancesHDepot
: BSourceDepot<TWSBusinessSource, InsuranceH> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="InsurancesHDepot"/>.
    /// </summary>
    public InsurancesHDepot() : base(new(), null) {
    }
}
