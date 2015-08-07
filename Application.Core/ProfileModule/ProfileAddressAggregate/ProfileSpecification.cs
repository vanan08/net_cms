using Application.Core.ProfileModule.ProfileAggregate;
using Application.Core.Specification;
using Application.Core.Specification.Implementation;

namespace Application.Core.ProfileModule.ProfileAddressAggregate
{
    /// <summary>
    /// A list of Profile specification
    /// </summary>
    public static class ProfileSpecification
    {

        /// <summary>
        /// Profile with firstName or LastName or Email equal to
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <returns>Associated specification for this creterion</returns>
        public static Specification<Profile> GetProfileByFilter(string firstName, string lastName, string email)
        {
            Specification<Profile> specProfile = new TrueSpecification<Profile>();

            if ( !string.IsNullOrEmpty(firstName))
                specProfile &= new DirectSpecification<Profile>(p => p.FirstName.Contains(firstName));

            if (!string.IsNullOrEmpty(lastName))
                specProfile &= new DirectSpecification<Profile>(p => p.LastName.Contains(lastName));

            if (!string.IsNullOrEmpty(email))
                specProfile &= new DirectSpecification<Profile>(p => p.Email.Contains(email));

            return specProfile;
        }

    }
}
