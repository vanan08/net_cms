using Application.Common.Logging;
using Application.Core;
using Application.Core.Specification.Contract;
using Application.DAL.Contract;
using Application.DAL.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Application.DAL
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        #region Members

        IQueryableUnitOfWork _UnitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");

            _UnitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        /// <summary>
        /// 
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(T item)
        {

            if (item != (T)null)
                GetSet().Add(item); // add new item in this set
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Message.info_CannotAddNullEntity, typeof(T).ToString());

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(T item)
        {
            if (item != (T)null)
            {
                //attach item if not exist
                _UnitOfWork.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Message.info_CannotRemoveNullEntity, typeof(T).ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void TrackItem(T item)
        {
            if (item != (T)null)
                _UnitOfWork.Attach<T>(item);
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Message.info_CannotRemoveNullEntity, typeof(T).ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Modify(T item)
        {
            if (item != (T)null)
                _UnitOfWork.SetModified(item);
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Message.info_CannotRemoveNullEntity, typeof(T).ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            if (id != 0)
                return GetSet().Find(id);
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return GetSet();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> AllMatching(ISpecification<T> specification)
        {
            return GetSet().Where(specification.SatisfiedBy());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="KProperty"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetPaged<KProperty>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<T, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetFiltered(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="persisted"></param>
        /// <param name="current"></param>
        public virtual void Merge(T persisted, T current)
        {
            _UnitOfWork.ApplyCurrentValues(persisted, current);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            if (_UnitOfWork != null)
                _UnitOfWork.Dispose();
        }

        #endregion

        #region Private Methods

        IDbSet<T> GetSet()
        {
            return _UnitOfWork.CreateSet<T>();
        }
        #endregion

    }
}
