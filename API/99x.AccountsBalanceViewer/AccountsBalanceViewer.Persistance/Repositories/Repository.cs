using AccountsBalanceViewer.Application.Contracts.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Fields
        internal AccountsBalanceViewerContext context = null;
        internal DbSet<T> table = null;
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="Repository{T}" /> class.</summary>
        /// <param name="context">The context.</param>
        public Repository(AccountsBalanceViewerContext context)
        {
            this.context = context;
            this.table = context.Set<T>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = table;

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
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="obj">The object.</param>
        public virtual async Task InsertAsync(T obj)
        {
            await table.AddAsync(obj);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual async Task DeleteAsync(object id)
        {
            T entityToDelete = await table.FindAsync(id);
            DeleteAsync(entityToDelete);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public virtual void DeleteAsync(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                table.Attach(entityToDelete);
            }
            table.Remove(entityToDelete);
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        public virtual void Update(T entityToUpdate)
        {
            table.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        #endregion
    }
}
