using Foundation.Migrations.Bases;
using TWS_Business.Sets;

namespace TWS_Business.Depots
{
    /// <summary>
    ///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
    ///     representing a depot to handle <see cref="Plate"/> datasource entity mirror.
    /// </summary>
    public class PlatesDepot 
        : BMigrationDepot<TWSBusinessSource, Plate>{
        /// <summary>
        ///     Generates a new depot handler for <see cref="Plate"/>.
        /// </summary>
        
        public PlatesDepot() 
            : base(new()){
        }
    }
}
