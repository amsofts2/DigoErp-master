using DigoErp.Repository.Edmx;
using DigoErp.Repository.Exceptions;
using DigoErp.Resources.App_Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace DigoErp.Repository.UnitOFWork
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal DigoErp_Db Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(DigoErp_Db context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual DataTableResponse<TEntity> GetPaginationByRawSql<T>(string filterSql, int take, int skip)
        {
            try
            {
                var cars = Context.Database.SqlQuery<TEntity>(filterSql).ToList();

                return new DataTableResponse<TEntity>
                {
                    data = cars,
                    recordsFiltered = cars.Count,
                    recordsTotal = cars.Count
                };
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.GetErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.GetErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.GetErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.GetErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.GetErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.GetErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }
        public virtual DataTableResponse<TEntity> GetPagination<T>(int take, int skip, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            try
            {
                IQueryable<TEntity> query = DbSet;
                int count;
                if (filter != null)
                {
                    query = query.Where(filter);
                    count = query.Where(filter).Count();
                }
                else
                {
                    count = query.Count();
                }
                var entities = orderBy(query).Skip(skip).Take(take);

                return new DataTableResponse<TEntity>
                {
                    data = entities.ToList(),
                    recordsTotal = count,
                    recordsFiltered = count
                };
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.GetErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.GetErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.GetErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.GetErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.GetErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.GetErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = DbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }

            catch (DbEntityValidationException ex)
            {
                var message = AppResource.GetErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.GetErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.GetErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.GetErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.GetErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.GetErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }

        public virtual IQueryable<TEntity> GetIQuerable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {

                IQueryable<TEntity> query = DbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query);
                }
                else
                {
                    return query;
                }
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.GetErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.GetErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.GetErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.GetErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.GetErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.GetErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                IQueryable<TEntity> query = DbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                return query.Count();
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.GetErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.GetErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.GetErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.GetErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.GetErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.GetErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }

        public void NotUpdateColsInDB(TEntity entityToUpdate, string[] excluded)
        {
            try
            {
                if (Context.Entry(entityToUpdate).State == EntityState.Detached)
                {
                    DbSet.Attach(entityToUpdate);
                }
                foreach (var name in excluded)
                {
                    Context.Entry(entityToUpdate).Property(name.ToString()).IsModified = false;
                }

            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.UpdateErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.UpdateErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.UpdateErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.UpdateErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.UpdateErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.UpdateErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var errorMessage = "";
                errorMessage = ex.Message.ToString();
                throw ex;
            }
        }

        public void RunSqlQuery(string sqlquery)
        {
            try
            {
                Context.Database.ExecuteSqlCommand(sqlquery);
            }
            catch (Exception ex)
            {

            }
        }
        public void UpdateSomeCols(TEntity entityToUpdate, params string[] properties)
        {
            try
            {
                foreach (var propertyName in properties)
                {
                    Context.Entry(entityToUpdate).Property(propertyName).IsModified = true;
                }
                Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.UpdateErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.UpdateErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.UpdateErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.UpdateErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.UpdateErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.UpdateErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IQueryable<TEntity> UntrackedTable
        {
            get
            {
                return DbSet.AsNoTracking<TEntity>();
            }
        }
        public virtual TEntity GetByID(object id)
        {
            try
            {
                return DbSet.Find(id);
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.GetErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.GetErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.GetErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.GetErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.GetErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.GetErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }
        public virtual TEntity GetByIdAsNoTracking(object id)
        {
            try
            {
                var entity = DbSet.Find(id);
                Context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.GetErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.GetErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.GetErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.GetErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.GetErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.GetErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }

        public virtual TEntity GetByIdWithDeAttachedEntity(object id)
        {
            try
            {
                DbSet.AsNoTracking();
                return DbSet.Find(id);
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.GetErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.GetErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.GetErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.GetErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.GetErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.GetErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }
        public virtual IEnumerable<TEntity> GetWithDeAttached(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
        {
            try
            {

                IQueryable<TEntity> query = DbSet.AsNoTracking();

                if (filter != null)
                {
                    query = query.Where(filter).AsNoTracking();
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty).AsNoTracking();
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.AsNoTracking().ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.GetErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.GetErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.GetErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.GetErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.GetErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.GetErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }
        public virtual long? GetMaxId(Expression<Func<TEntity, long?>> filter)
        {
            try
            {
                IQueryable<TEntity> query = DbSet;
                return query.Max(filter);
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.GetErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.GetErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.GetErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.GetErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.GetErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.GetErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }

        }

        public virtual TEntity Insert(TEntity entity)
        {
            try
            {
                DbSet.Add(entity);
                return entity;
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.InsertErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.InsertErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.InsertErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.InsertErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.InsertErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.InsertErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }


        public virtual void Delete(object id)
        {
            try
            {
                var entityToDelete = DbSet.Find(id);
                Delete(entityToDelete);
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.DeleteErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.DeleteErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.DeleteErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.DeleteErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.DeleteErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.DeleteErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            try
            {
                if (Context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    DbSet.Attach(entityToDelete);
                }
                DbSet.Remove(entityToDelete);
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.DeleteErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.DeleteErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.DeleteErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.DeleteErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.DeleteErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.DeleteErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }

        public virtual TEntity Update(TEntity entityToUpdate)
        {
            try
            {
                DbSet.Attach(entityToUpdate);
                Context.Entry(entityToUpdate).State = EntityState.Modified;
                return entityToUpdate;
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.UpdateErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.UpdateErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.UpdateErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.UpdateErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.UpdateErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.UpdateErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }

        public virtual TEntity DeAttached(TEntity entityToUpdate)
        {
            try
            {
                Context.Entry(entityToUpdate).State = EntityState.Detached;
                return entityToUpdate;
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.UpdateErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.UpdateErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.UpdateErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.UpdateErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.UpdateErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;

            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.UpdateErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }
    }
}
