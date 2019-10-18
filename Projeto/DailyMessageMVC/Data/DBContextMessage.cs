using DailyMessageMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace DailyMessageMVC.Data
{
    public class DBContextMessage<T> : DbContext where T : class
    {
        public int CurrentUser { get; set; }
        protected DBContextMessage<T> _connection;
        public DbSet<T> DbSet { get; set; }
        public DBContextMessage() : base("DB_MESSAGE")
        {
            Database.SetInitializer<DBContextMessage<T>>(null);
        }
        #region Constructor
        public DBContextMessage(int? currentUser = null)
            : base("Name=DB_MESSAGE")
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            if (currentUser.HasValue)
                CurrentUser = currentUser.Value;
        }
        #endregion Constructor
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            var typesToMapping = (from x in Assembly.GetExecutingAssembly().GetTypes()
                                  where x.IsClass && typeof(IMapping).IsAssignableFrom(x)
                                  select x).ToList();

            foreach (var mapping in typesToMapping)
            {
                dynamic mappingClass = Activator.CreateInstance(mapping);
                modelBuilder.Configurations.Add(mappingClass);

            }

            base.OnModelCreating(modelBuilder);
        }
        public virtual void ChangeObjectState(object model, EntityState state)
        {
            ((IObjectContextAdapter)this)
                          .ObjectContext
                          .ObjectStateManager
                          .ChangeObjectState(model, state);
        }

        public virtual async Task<int> SaveAsync(T model)
        {
            this.DbSet.Add(model);
            return await this.SaveChangesAsync();
        }
        public virtual async Task<int> UpadateAsync(T model)
        {
            var entry = this.Entry(model);

            if (entry.State == EntityState.Detached)
                this.DbSet.Attach(model);

            this.ChangeObjectState(model, EntityState.Modified);
            return await this.SaveChangesAsync();
        }

        public virtual void Delete(T model)
        {
            var entry = this.Entry(model);

            if (entry.State == EntityState.Detached)
                this.DbSet.Attach(model);

            this.ChangeObjectState(model, EntityState.Deleted);
            this.SaveChangesAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.DbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await this.DbSet.FindAsync(id);
        }
        public virtual IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            return this.DbSet.Where(expression);
        }
        public IEnumerable<T> OrderBy(Expression<Func<T, bool>> expression)
        {
            return this.DbSet.OrderBy(expression);
        }
        public IQueryable<T> QueryAll()
        {
            return this.DbSet.AsNoTracking().AsQueryable();
        }

        public IEnumerable<T> ExecuteQuery(string spQuery, object[] parameters)
        {
            using (_connection = new DBContextMessage<T>())
            {
                using (var dbContextTransaction = _connection.Database.BeginTransaction())
                {
                    try
                    {
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
                return _connection.Database.SqlQuery<T>(spQuery, parameters).ToList();
            }
        }
        public T ExecuteQuerySingle(string spQuery, object[] parameters)
        {
            using (_connection = new DBContextMessage<T>())
            {
                return _connection.Database.SqlQuery<T>(spQuery, parameters).FirstOrDefault();
            }
        }
        public int ExecuteCommand(string spQuery, object[] parameters)
        {
            int result = 0;
            using (_connection = new DBContextMessage<T>())
            {
                using (var dbContextTransaction = _connection.Database.BeginTransaction())
                {
                    try
                    {
                        result = _connection.Database.SqlQuery<int>(spQuery, parameters).FirstOrDefault();
                        dbContextTransaction.Commit();

                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }

            return result;
        }

        public int ExecuteSQLCommand(string spQuery, object[] parameters)
        {
            int result = 0;
            using (_connection = new DBContextMessage<T>())
            {
                using (var dbContextTransaction = _connection.Database.BeginTransaction())
                {
                    try
                    {
                        result = _connection.Database.ExecuteSqlCommand(spQuery, parameters);
                        dbContextTransaction.Commit();

                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }

            return result;
        }
    }
}
