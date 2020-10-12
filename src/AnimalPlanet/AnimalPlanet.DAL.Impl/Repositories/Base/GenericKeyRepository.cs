using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AnimalPlanet.DAL.Abstract.IRepositories.Base;
using AnimalPlanet.Models;

using Microsoft.EntityFrameworkCore;

namespace AnimalPlanet.DAL.Impl.Repositories.Base
{
    public class GenericKeyRepository<TKey, TEntity> : IGenericKeyRepository<TKey, TEntity> where TEntity : class
    {
        public GenericKeyRepository(AnimalPlanetDbContext context)
        {
            Context = context;
        }

        public AnimalPlanetDbContext Context { get; }

        public DbSet<TEntity> DbSet => Context.Set<TEntity>();

        public virtual async Task<DataResult<TEntity>> Add(TEntity entity)
        {
            try
            {
                var item = await Context.Set<TEntity>().AddAsync(entity);

                await Save();

                return new DataResult<TEntity>
                {
                    Success = true,
                    Data = item.Entity,
                };
            }
            catch (DbUpdateException dbException)
            {
                if (dbException.InnerException != null)
                {
                    if ((string)dbException.InnerException.Data["SqlState"] == "23505")
                    {
                        return new DataResult<TEntity>
                        {
                            Success = false,
                            ErrorCode = ErrorCode.UniquenessError,
                            Data = entity,
                        };
                    }
                    if ((string)dbException.InnerException.Data["SqlState"] == "23503")
                    {
                        return new DataResult<TEntity>
                        {
                            Success = false,
                            ErrorCode = ErrorCode.ForeignKeyViolation,
                            Data = entity,
                        };
                    }
                }
                
                throw;
            }
        }

        public virtual async Task<Result> Update(TEntity entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;

                await Save();

                return new Result
                {
                    Success = true,
                };
            }
            catch (DbUpdateConcurrencyException dbException)
            {
                if (dbException.Message.Contains("Database operation expected to affect")
                    && dbException.Message.Contains("row(s)"))
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                throw;
            }
            catch (DbUpdateException dbException)
            {
                if (dbException.InnerException != null)
                {
                    if ((string)dbException.InnerException.Data["SqlState"] == "23505")
                    {
                        return new Result
                        {
                            Success = false,
                            ErrorCode = ErrorCode.UniquenessError,
                        };
                    }
                    if ((string)dbException.InnerException.Data["SqlState"] == "23503")
                    {
                        return new Result
                        {
                            Success = false,
                            ErrorCode = ErrorCode.ForeignKeyViolation,
                        };
                    }
                }

                throw;
            }
        }

        public virtual async Task<Result> Delete(TEntity entity)
        {
            try
            {
                TEntity result = Context.Set<TEntity>()
                    .Remove(entity).Entity;

                await Save();

                return new Result {Success = true,};
            }
            catch (DbUpdateConcurrencyException dbException)
            {
                if (dbException.Message.Contains("Database operation expected to affect")
                    && dbException.Message.Contains("row(s)"))
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                throw;
            }
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(TKey id)
        {
            return await Context.Set<TEntity>()
                .FindAsync(id);
        }

        public virtual async Task<int> GetCount()
        {
            return await Context.Set<TEntity>().CountAsync();
        }

        public virtual async Task<List<TEntity>> GetPart(int skip, int take)
        {
            return await Context.Set<TEntity>().Skip(skip).Take(take).ToListAsync();
        }

        public Task Save()
        {
            return Context.SaveChangesAsync();
        }
    }
}
