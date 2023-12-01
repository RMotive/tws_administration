using TWS_Security.Contracts.Interfaces;
using TWS_Security.Entities;
using TWS_Security.Models;

namespace TWS_Security.Repositories;

/// <summary>
///     Represents a repository to handle operations related to Solution entity
/// </summary>
public class SolutionsRepository : IDatasourceDirectoryRepository<SolutionEntity, Solution>
{
    private readonly TWSSecuritySource _source;

    public SolutionsRepository() { }

    public List<SolutionEntity> Build()
    {
        SolutionEntity entity = new SolutionEntity();
        throw new NotImplementedException();
    }
}
