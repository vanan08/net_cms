using Application.Core.ProfileModule.PhoneAggregate;
using Application.DAL;

namespace Application.Repository.ProfileModule
{
    public class PhoneRepository : Repository<Phone>, IPhoneRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public PhoneRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
}
