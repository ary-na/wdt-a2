namespace MCBA_Web_API.Models.Repository;

// Code sourced and adapted from:
// Week 9 Lectorial - IDataRepository.cs
// https://rmit.instructure.com/courses/102750/files/26419005?wrap=1

public interface IDataRepository<TEntity, TKey> where TEntity : class
{
    IEnumerable<TEntity> GetAll();
    TEntity Get(TKey id);
    TKey Add(TEntity item);
    TKey Update(TKey id, TEntity item);
    TKey Delete(TKey id);
}