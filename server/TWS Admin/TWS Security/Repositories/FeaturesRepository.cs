using Foundation.Contracts.Datasources.Bases;

using TWS_Security.Entities;
using TWS_Security.Sets;

namespace TWS_Security.Repositories;
public class FeaturesRepository
    : BRepository<TWSSecuritySource, FeaturesRepository, FeatureEntity, Feature> {
    public FeaturesRepository() : base(new ()) {  }
}
