using Application.Core.ProfileModule.AddressAggregate;
using Application.DAL;

namespace Application.Repository.ProfileModule
{
    public class AddressTypeRepository : Repository<AddressType>, IAddressTypeRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public AddressTypeRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
}
