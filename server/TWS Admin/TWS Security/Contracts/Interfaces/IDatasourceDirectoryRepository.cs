using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_Security.Contracts.Interfaces;

/// <summary>
///     Forces a contract to represent a repository that works over a catalogue
///     stored in a datasource
/// </summary>
public interface IDatasourceDirectoryRepository<TEntity, TSet>
    where TEntity : IDatasourceEntity<TSet>
{
    public List<TEntity> Build();
}
