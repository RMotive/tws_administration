using System.Reflection;

using Foundation.Migrations.Quality.Bases;

using TWS_Security.Entities;
using TWS_Security.Sets;

namespace TWS_Security.Quality.Entities;
public class Q_FeatureEntity
    : BQ_Entity<FeatureEntity, Feature> {

    protected override Dictionary<PropertyInfo[], FeatureEntity> EvaluateFactory() {
        string name = "Q_Name";


        return new() {
            {[], new(1, name, null) },
            {[], new(0, name, null) },
        };
    }
}
