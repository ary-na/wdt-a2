namespace MCBA_Web_API.Models.Repository;

// Code sourced and adapted from:
// Week 9 Lectorial - IDataRepository.cs

public interface IDataRepository<TEntity, TKey> where TEntity : class
{
    IEnumerable<TEntity> GetAll();
    TEntity Get(TKey id);
    TKey Add(TEntity item);
    TKey Update(TKey id, TEntity item);
    TKey Delete(TKey id);
}
