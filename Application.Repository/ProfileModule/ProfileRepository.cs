using Application.Core.ProfileModule.ProfileAggregate;
using Application.DAL;

namespace Application.Repository.ProfileModule
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public ProfileRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
}
