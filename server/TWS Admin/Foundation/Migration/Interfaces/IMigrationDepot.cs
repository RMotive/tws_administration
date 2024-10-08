﻿using Foundation.Migration.Interfaces.Depot;
using Foundation.Migrations.Interfaces.Depot;

namespace Foundation.Migrations.Interfaces;
/// <summary>
///     Determines how a complex <see cref="IMigrationDepot{TMigrationSet}"/> 
///     implementation should behave.
///     
///     <br>
///         A <see cref="IMigrationDepot{TMigrationSet}"/> is considered as a
///         data repository for a specific <typeparamref name="TMigrationSet"/>.
///         providing data, storing data, updating data, etc...
///     </br>
/// </summary>
/// <typeparam name="TMigrationSet">
///     The datasource object that the implementation handles.
/// </typeparam>
public interface IMigrationDepot<TMigrationSet>
    : IMigrationDepot_View<TMigrationSet>
    , IMigrationDepot_Read<TMigrationSet>
    , IMigrationDepot_Create<TMigrationSet>
    , IMigrationDepot_Delete<TMigrationSet>
    where TMigrationSet : IMigrationSet { }
