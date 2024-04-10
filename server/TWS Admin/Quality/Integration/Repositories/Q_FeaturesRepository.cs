using TWS_Security.Entities;
using TWS_Security.Quality.Contracts.Bases;
using TWS_Security.Repositories;
using TWS_Security.Sets;

namespace TWS_Security.Quality.Integration.Repositories;
public class Q_FeaturesRepository 
    : BQ_Repository<TWSSecuritySource, FeaturesRepository, FeatureEntity, Feature> {

    public Q_FeaturesRepository() : base(new(), new()) { }

    protected override (FeatureEntity[] Mocks, (Feature[] Sets, FeatureEntity[] Entities) XMocks) InitMocks() {
        throw new NotImplementedException();
    }

    protected override Task<(Feature[] Sets, FeatureEntity[] Entities)> InitLiveMocks() {
        throw new NotImplementedException();
    }

    public override void Create() {
        throw new NotImplementedException();
    }

    public override void Read() {
        throw new NotImplementedException();
    }

    public override void Update() {
        throw new NotImplementedException();
    }

    public override void Delete() {
        throw new NotImplementedException();
    }
}
