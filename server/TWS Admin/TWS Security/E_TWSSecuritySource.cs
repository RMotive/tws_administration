using CSMFoundation.Migration.Bases;
using CSMFoundation.Migration.Interfaces;

using TWS_Security.Sets;

namespace TWS_Security;

public partial class TWSSecuritySource
    : BMigrationSource<TWSSecuritySource> {
    public TWSSecuritySource()
        : base() {

    }

    protected override ISourceSet[] EvaluateFactory() {
        return [
                new Solution(),
        ];
    }
}
