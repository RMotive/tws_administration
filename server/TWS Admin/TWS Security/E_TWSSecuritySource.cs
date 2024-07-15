using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;

using TWS_Security.Sets;

namespace TWS_Security;

public partial class TWSSecuritySource
    : BSource<TWSSecuritySource> {
    public TWSSecuritySource()
        : base() {

    }

    protected override ISourceSet[] EvaluateFactory() {
        return [
                new Solution(),
        ];
    }
}
