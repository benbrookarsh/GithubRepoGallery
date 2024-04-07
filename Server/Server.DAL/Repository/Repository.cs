using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Server.DAL.Context;
using Server.Shared.Interfaces;

namespace Server.DAL.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    #region ctor

    private readonly DbContext _context;
    private DbSet<T> _entities;
    private readonly ILogger<Repository<T>> _logger;
    private readonly object _lockobject = new();
    public Repository(AppDbContext context, ILogger<Repository<T>> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion

    #region Properties

    
    public virtual IQueryable<T> Table
    {
        get
        {
            lock (_lockobject)
            {
                return Entities;
            }
        }
    }
    public virtual IQueryable<T> TableNoTracking
    {
        get
        {
            lock (_lockobject)
            {
                return Entities.AsNoTracking();
            }
        }
    }
    public DatabaseFacade Database => _context.Database;
    public DbContext Context => _context;

    // public ApplicationDbContext Context => _context;

    protected virtual DbSet<T> Entities
    {
        get
        {
            lock (_lockobject)
            {
                return _entities ??= _context.Set<T>();
            }
        }
    }
    

    #endregion

    public T GetById(object id)
    {
        lock (_lockobject)
        {
            return Entities.Find(id);
        }
    }

    public async Task<T> GetByIdAsync(object id)
    {
        lock (_lockobject)
        {
            return Entities.FindAsync(id).Result;

        }
        //return await Entities.FindAsync(id);
    }

    public IQueryable<T> GetAll()
    {
        lock (_lockobject)
        {
            return Entities;
        }
    }

    public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
    {
        lock (_lockobject)
        {
            return Entities.Where(predicate);
        }
    }

    public IQueryable<T> FindByAsNoTracking(Expression<Func<T, bool>> predicate)
    {
        lock (_lockobject)
        {
            return Entities.Where(predicate).AsNoTracking();
        }
    }

    public IQueryable<T> FindWith(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] selectors)
    {
        lock (_lockobject)
        {
            var query = Entities.Where(predicate);

            query = selectors.Aggregate(query, (current, selector) =>
                current.Include(selector));

            return query;
        }
    }

    public IQueryable<T> FindWithAsNoTracking(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] selectors)
    {
        lock (_lockobject)
        {
            var query = Entities.Where(predicate);

            query = selectors.Aggregate(query, (current, selector) =>
                current.Include(selector));

            return query.AsNoTracking();
        }
    }

    public T Insert(T entity)
    {
        lock (_lockobject)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Add(entity);

                var result = _context.SaveChanges();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }

    public async Task<T> InsertAsync(T entity)
    {
        lock (_lockobject)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                this.Entities.Add(entity);

                var result = _context.SaveChangesAsync().Result;
                
                return result > 0 ? entity : null;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }

    public void InsertRange(IEnumerable<T> entities)
    {
        lock (_lockobject)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }

    public async Task<int> InsertRangeAsync(IEnumerable<T> entities)
    {
        lock (_lockobject)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    Entities.Add(entity);
                
                return _context.SaveChangesAsync().Result;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
    }

    public T Update(T entity)
    {
        lock (_lockobject)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var res = _context.SaveChanges();
                if (res > 0)
                {
                    return entity;
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            
            return null;
        }
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        lock (_lockobject)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var res = _context.SaveChangesAsync().Result;
                if (res > 0)
                {
                    return entity;
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            return null;
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        lock (_lockobject)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        lock (_lockobject)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }

    public T Delete(T entity)
    {
        lock (_lockobject)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Remove(entity);

                var res = _context.SaveChanges();
                if (res > 0)
                {
                    return entity;
                }

                return null;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
    
    public async Task DeleteByAsync(Expression<Func<T, bool>> predicate)
    {
        lock (_lockobject)
        {
            var entity = Entities.FirstOrDefaultAsync(predicate).Result;

            if (entity is null) return;

            Entities.Remove(entity);

            _context.SaveChanges();
        }
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        lock (_lockobject)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities.ToList())
                    Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }

    public async Task<T?> DeleteAsync(T entity)
    {
        lock (_lockobject)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Remove(entity);

                var res = _context.SaveChangesAsync().Result;
                if (res > 0)
                {
                    return entity;
                }

                return null;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }

    public async Task<int> DeleteRangeAsync(IEnumerable<T> entities)
    {
        lock (_lockobject)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities.ToList())
                    Entities.Remove(entity);

                return _context.SaveChangesAsync().Result;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}