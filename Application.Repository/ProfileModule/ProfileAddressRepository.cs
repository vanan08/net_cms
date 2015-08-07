using Application.Core.ProfileModule.ProfileAddressAggregate;
using Application.DAL;

namespace Application.Repository.ProfileModule
{
    public class ProfileAddressRepository : Repository<ProfileAddress>, IProfileAddressRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public ProfileAddressRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
}
