using Foundation.Contracts.Datasources.Interfaces;

namespace Foundation.Contracts.Datasources.Bases;
public abstract class BDatasourceRepository<TEntity, TSet>
    : IDatasourceRepository<TEntity, TSet>
    where TEntity : IDatasourceEntity
    where TSet : IDatasourceSet
{
    //--> Creat interface <--//
    public abstract TEntity Create(TEntity Entity);
    public List<TEntity> Create(TEntity Entity, int Copies)
    {
        List<TEntity> Resolutions = [];
        for (int Point = 0; Point < Copies; Point++)
        {
            TEntity Resolution = Create(Entity);
            Resolutions.Add(Resolution);
        }

        return Resolutions;
    }
    public List<TEntity> Create(List<TEntity> Entities)
    {
        int Entries = Entities.Count;
        List<TEntity> Resolutions = [];
        for (int Point = 0; Point < Entries; Point++)
        {
            TEntity Pointed = Entities[Point];
            TEntity Resolution = Create(Pointed);
            Resolutions.Add(Resolution);
        }

        return Resolutions;
    }

    //--> Read interface <--//
    public abstract TEntity Read(int Pointer);
    public abstract List<TEntity> Read();
    public List<TEntity> Read(List<int> Pointers)
    {
        int Entries = Pointers.Count;
        List<TEntity> Retrieved = [];
        for (int Point = 0; Point < Entries; Point++)
        {
            int Pointed = Pointers[Point];
            TEntity Found = Read(Pointed);
            Retrieved.Add(Found);
        }
        return Retrieved;
    }
    public abstract List<TEntity> Read(Predicate<TSet> Match, bool FirstOnly = false);

    //--> Update interface <--//
    public abstract TEntity Update(TEntity Entity);

    public TEntity Update(int Pointer, TEntity Entity)
    {
        throw new NotImplementedException();
    }

    public List<TEntity> Update(List<TEntity> Entities)
    {
        throw new NotImplementedException();
    }

    public List<TEntity> Update(List<int> Pointers, List<TEntity> Entities)
    {
        throw new NotImplementedException();
    }

    public List<TEntity> Update(List<int> Pointers, TEntity Entity)
    {
        throw new NotImplementedException();
    }

    public List<TEntity> Update(Predicate<TEntity> Match, Func<TEntity, TEntity> Refactor, bool FirstOnlt = false)
    {
        throw new NotImplementedException();
    }

    public TEntity Delete(TEntity Entity)
    {
        throw new NotImplementedException();
    }

    public TEntity Delete(int Pointer)
    {
        throw new NotImplementedException();
    }

    public List<TEntity> Delete(List<TEntity> Entities)
    {
        throw new NotImplementedException();
    }

    public List<TEntity> Delete(List<int> Entities)
    {
        throw new NotImplementedException();
    }

    public List<TEntity> Delete(Predicate<TEntity> Match, bool FirstOnly = false)
    {
        throw new NotImplementedException();
    }
}
