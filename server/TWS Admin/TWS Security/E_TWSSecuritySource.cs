using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;

using TWS_Security.Sets;

namespace TWS_Security;

public partial class TWSSecuritySource
    : BMigrationSource<TWSSecuritySource> {
    public TWSSecuritySource()
        : base() {

    }

    protected override IMigrationSet[] EvaluateFactory() {
        return [
                new Solution(),
        ];
    }
}
