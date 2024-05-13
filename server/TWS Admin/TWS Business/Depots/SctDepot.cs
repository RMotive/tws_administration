using Foundation.Migrations.Bases;
using System;
using TWS_Business.Sets;


namespace TWS_Business.Depots
{
    /// <summary>
    ///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
    ///     representing a depot to handle <see cref="Sct"/> datasource entity mirror.
    /// </summary>
    public class SctDepot
    : BMigrationDepot<TWSBusinessSource,Sct>{
        /// <summary>
        ///     Generates a new depot handler for <see cref="Sct"/>.
        /// </summary>

        public SctDepot() 
            : base(new()){

        }

    }
}
