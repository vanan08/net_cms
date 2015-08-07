using Application.Core.ProfileModule.ProfilePhoneAggregate;
using Application.DAL;

namespace Application.Repository.ProfileModule
{
    public class ProfilePhoneRepository : Repository<ProfilePhone>, IProfilePhoneRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public ProfilePhoneRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #endregion
    }
}
