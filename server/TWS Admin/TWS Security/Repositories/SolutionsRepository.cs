using Foundation.Contracts.Datasources.Bases;

using TWS_Security.Entities;
using TWS_Security.Sets;

namespace TWS_Security.Repositories;

/// <summary>
///     Represents a repository to handle operations related to the Solution entity.
/// </summary>
public class SolutionsRepository
    : BDatasourceRepository<SolutionEntity, Solution>
{
    private readonly TWSSecuritySource _source;
    public SolutionsRepository(TWSSecuritySource Source)
    {
        _source = Source;
    }

    public override SolutionEntity Create(SolutionEntity Entity)
    {
        Solution set = Entity.BuildSet();
        _source.Solutions.Add(set);

        return set.BuildEntity();
    }

    public override SolutionEntity Read(int Pointer)
    {
        throw new NotImplementedException();
    }

    public override List<SolutionEntity> Read()
    {
        throw new NotImplementedException();
    }

    public override List<SolutionEntity> Read(Predicate<Solution> Match, bool FirstOnly = false)
    {
        throw new NotImplementedException();
    }

    public override SolutionEntity Update(SolutionEntity Entity)
    {
        throw new NotImplementedException();
    }
}
