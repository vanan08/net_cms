using Application.Core.ProfileModule.AddressAggregate;
using Application.DAL;

namespace Application.Repository.ProfileModule
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public AddressRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
}
