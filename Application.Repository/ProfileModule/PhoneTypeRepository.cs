using Application.Core.ProfileModule.PhoneAggregate;
using Application.DAL;

namespace Application.Repository.ProfileModule
{
    public class PhoneTypeRepository : Repository<PhoneType>, IPhoneTypeRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public PhoneTypeRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
}
