using Foundation.Migrations.Bases;
using TWS_Business.Sets;


namespace TWS_Business.Depots
{
    /// <summary>
    ///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
    ///     representing a depot to handle <see cref="Sct"/> datasource entity mirror.
    /// </summary>
    public class SctsDepot
    : BMigrationDepot<TWSBusinessSource, Sct>
    {
        /// <summary>
        ///     Generates a new depot handler for <see cref="Sct"/>.
        /// </summary>

        public SctsDepot()
            : base(new(), null)
        {

        }

    }
}
