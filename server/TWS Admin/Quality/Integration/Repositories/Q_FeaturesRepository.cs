using TWS_Security.Entities;
using TWS_Security.Quality.Contracts.Bases;
using TWS_Security.Repositories;
using TWS_Security.Sets;

namespace TWS_Security.Quality.Integration.Repositories;
public class Q_FeaturesRepository
    : BQ_Repository<TWSSecuritySource, FeaturesRepository, FeatureEntity, Feature> {

    public Q_FeaturesRepository()
        : base(
                0,
                new(),
                new(),
                (token, set) => {
                    set.Name = token;
                    return set;
                }
            ) { }

    protected override Feature UMockFactory(int Pointer, string Token) {
        return new() {
            Name = Token,
            Description = $"Quality description for {Token}",
        };
    }

    protected override (Feature Set, FeatureEntity Entity) XMockFactory() {
        return (
                new(),
                new("", null)
            );
    }

    protected override (Feature Set, FeatureEntity Entity) MockFactory(int Pointer, string Token) {
        string description = $"Q_Description for {Token}";

        return (
                new() {
                    Name = Token,
                    Description = description,
                },
                new(Token, description)
            );
    }
}
