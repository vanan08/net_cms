
using System;

namespace Application.Core.ProfileModule.PhoneAggregate
{
    /// <summary>
    /// This is the factory for Phone creation
    /// </summary>
    public static class PhoneFactory
    {
        /// <summary>
        /// Create a New Phone
        /// </summary>
        /// <param name="number"></param>
        /// <param name="createdBy"></param>
        /// <param name="created"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updated"></param>
        /// <returns></returns>
        public static Phone CreatePhone(string number, string createdBy, DateTime created, string updatedBy, DateTime updated)
        {
            Phone objPhone = new Phone();

            //Set values for Phone
            objPhone.Number = number;
            objPhone.Created = created;
            objPhone.CreatedBy = createdBy;
            objPhone.Updated = updated;
            objPhone.UpdatedBy = updatedBy;

            return objPhone;
        }

    }
}
