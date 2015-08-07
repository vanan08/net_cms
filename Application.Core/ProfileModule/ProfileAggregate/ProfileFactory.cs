using System;

namespace Application.Core.ProfileModule.ProfileAggregate
{
    /// <summary>
    /// This is the factory for Profile creation
    /// </summary>
    public static class ProfileFactory
    {
        /// <summary>
        /// Create a New Profile
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="createdBy"></param>
        /// <param name="created"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updated"></param>
        /// <returns></returns>
        public static Profile CreateProfile(string firstName, string lastName, string email, string createdBy, DateTime created, string updatedBy, DateTime updated)
        {
            Profile objProfile = new Profile();

            //Set values for Profile
            objProfile.FirstName = firstName;
            objProfile.LastName = lastName;
            objProfile.Email = email;
            objProfile.Created = created;
            objProfile.CreatedBy = createdBy;
            objProfile.Updated = updated;
            objProfile.UpdatedBy = updatedBy;

            return objProfile;
        }

    }
}
