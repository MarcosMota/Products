using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Products.Domain.Data.DbContexts;
using Products.Domain.Entities;
using Products.Domain.Extensions;

namespace Products.Domain.Data.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ProductDbContext context;
        private DbSet<TEntity> _entities;

        public BaseRepository(ProductDbContext context)
        {
            this.context = context;

        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);

            context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll(Pagination pagination = null, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if(pagination != null)
            {
                query.Paginate(pagination);
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

        public TEntity GetById(int Id)
        {
            return Entities.FirstOrDefault(p=>p.Id == Id);
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Add(entity);

            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entry = context.Entry<TEntity>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = context.Set<TEntity>();
                TEntity attachedEntity = set.Local.SingleOrDefault(e => e.Id == entity.Id); 

                if (attachedEntity != null)
                {
                    var attachedEntry = context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Retorna a tabela
        /// </summary>
        public virtual IQueryable<TEntity> Table
        {
            get
            {
                return Entities;
            }
        }

        /// <summary>
        /// Obtém uma tabela com "sem rastreamento" ativada (recurso EF) Use-a somente quando você carrega registro (s) somente para operações de somente leitura
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = context.Set<TEntity>();
                return _entities;
            }
        }
    }
}
