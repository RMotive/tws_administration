using Foundation.Migrations.Bases;
using TWS_Business.Sets;

namespace TWS_Business.Depots
{
    /// <summary>
    ///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
    ///     representing a depot to handle <see cref="Situation"/> datasource entity mirror.
    /// </summary>
    public class SituationsDepot
        : BMigrationDepot<TWSBusinessSource, Situation> {

        /// <summary>
        ///     Generates a new depot handler for <see cref="Situation"/>.
        /// </summary>
        public SituationsDepot() : base(new()){
        
        }
    }


}
