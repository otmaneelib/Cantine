using System.Linq.Expressions;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly ILogger<RepositoryBase<T>> _logger;

        public RepositoryBase(AppDbContext context, ILogger<RepositoryBase<T>> logger)
        {
            _context = context;
            _logger = logger;
            _context.ChangeTracker.LazyLoadingEnabled = true; // Enable lazy loading
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                _logger.LogInformation("Getting all entities of type {EntityType}", typeof(T).Name);
                return _context.Set<T>().AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all entities of type {EntityType}", typeof(T).Name);
                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Getting entity of type {EntityType} with ID {Id}", typeof(T).Name, id);
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting entity of type {EntityType} with ID {Id}", typeof(T).Name, id);
                throw;
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                _logger.LogInformation("Adding entity of type {EntityType}", typeof(T).Name);
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully added entity of type {EntityType}", typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding entity of type {EntityType}", typeof(T).Name);
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _logger.LogInformation("Updating entity of type {EntityType}", typeof(T).Name);
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully updated entity of type {EntityType}", typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating entity of type {EntityType}", typeof(T).Name);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting entity of type {EntityType} with ID {Id}", typeof(T).Name, id);
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity != null)
                {
                    _context.Set<T>().Remove(entity);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Successfully deleted entity of type {EntityType} with ID {Id}", typeof(T).Name, id);
                }
                else
                {
                    _logger.LogWarning("Entity of type {EntityType} with ID {Id} not found", typeof(T).Name, id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting entity of type {EntityType} with ID {Id}", typeof(T).Name, id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                _logger.LogInformation("Checking existence of entity of type {EntityType} with specified predicate", typeof(T).Name);
                return await _context.Set<T>().AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking existence of entity of type {EntityType} with specified predicate", typeof(T).Name);
                throw;
            }
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            try
            {
                _logger.LogInformation("Finding entities of type {EntityType} with specified condition", typeof(T).Name);
                return _context.Set<T>().Where(expression).AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while finding entities of type {EntityType} with specified condition", typeof(T).Name);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting all entities of type {EntityType}", typeof(T).Name);
                return await _context.Set<T>().CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while counting all entities of type {EntityType}", typeof(T).Name);
                throw;
            }
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                _logger.LogInformation("Counting entities of type {EntityType} with specified predicate", typeof(T).Name);
                return await _context.Set<T>().CountAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while counting entities of type {EntityType} with specified predicate", typeof(T).Name);
                throw;
            }
        }
    }
}
