using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Server.Shared.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> Table { get; }
    IQueryable<T> TableNoTracking { get; }
    DatabaseFacade Database { get; }
    
    DbContext Context { get; }
    T GetById(object id);
    Task<T> GetByIdAsync(object id);
    IQueryable<T> GetAll();
    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    IQueryable<T> FindByAsNoTracking(Expression<Func<T, bool>> predicate);
    IQueryable<T> FindWith(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] selectors);
    IQueryable<T> FindWithAsNoTracking(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] selectors);
    T Insert(T entity);
    Task<T> InsertAsync(T entity);
    void InsertRange(IEnumerable<T> entities);
    Task<int> InsertRangeAsync(IEnumerable<T> entities);
    T Update(T entity);
    Task<T?> UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> entities);
    void UpdateRange(IEnumerable<T> entities);
    T Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    Task DeleteByAsync(Expression<Func<T, bool>> predicate);
    Task<T?> DeleteAsync(T entity);
    Task<int> DeleteRangeAsync(IEnumerable<T> entities);
}