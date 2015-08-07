using Application.Common;
using Application.Core.ProfileModule.AddressAggregate;
using Application.Core.ProfileModule.ProfileAggregate;
using Application.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Core.ProfileModule.ProfileAddressAggregate
{
    public partial class ProfileAddress : Entity, IValidatableObject
    {
        #region Property

        [Key]
        public int ProfileAddressId { get; set; }
        public int ProfileId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Address Address { get; set; }
        public virtual AddressType AddressType { get; set; }
        public virtual Profile Profile { get; set; }

        #endregion Property

        #region Public Methods

        /// <summary>
        /// Associate existing Profile to this ProfileAddress
        /// </summary>
        /// <param name="profile"></param>
        public void AssociateProfileForThisProfileAddress(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentException(Messages.exception_ProfileAddressCannotAssociateNullProfile);
            }

            //fix relation
            this.ProfileId = profile.ProfileId;

            this.Profile = profile;
        }

        /// <summary>
        /// Set the Profile reference for this ProfileAddress
        /// </summary>
        /// <param name="profileId"></param>
        public void SetTheProfileReference(int profileId)
        {
            if (profileId != 0)
            {
                //fix relation
                this.ProfileId = profileId;

                this.Profile = null;
            }
        }

        /// <summary>
        /// Associate existing Address to this ProfileAddress
        /// </summary>
        /// <param name="address"></param>
        public void AssociateAddressForThisProfileAddress(Address address)
        {
            if (address == null)
            {
                throw new ArgumentException(Messages.exception_ProfileAddressCannotAssociateNullAddress);
            }

            //fix relation
            this.AddressId = address.AddressId;

            this.Address = address;
        }

        /// <summary>
        /// Set the Address reference for this ProfileAddress
        /// </summary>
        /// <param name="addressId"></param>
        public void SetTheAddressReference(int addressId)
        {
            if (addressId != 0)
            {
                //fix relation
                this.AddressId = addressId;

                this.Address = null;
            }
        }

        /// <summary>
        /// Associate existing AddressType to this ProfileAddress
        /// </summary>
        /// <param name="addressType"></param>
        public void AssociateAddressTypeForThisProfileAddress(AddressType addressType)
        {
            if (addressType == null)
            {
                throw new ArgumentException(Messages.exception_ProfileAddressCannotAssociateNullAddressType);
            }

            //fix relation
            this.AddressTypeId = addressType.AddressTypeId;

            this.AddressType = addressType;
        }

        /// <summary>
        /// Set the AddressType reference for this ProfileAddress
        /// </summary>
        /// <param name="addressTypeId"></param>
        public void SetTheAddressTypeReference(int addressTypeId)
        {
            if (addressTypeId != 0)
            {
                //fix relation
                this.AddressTypeId = addressTypeId;

                this.AddressType = null;
            }
        }

        #endregion Public Methods

        #region IValidatableObject Members

        /// <summary>
        /// This will validate entity for all  the conditions
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            //-->Check Profile identifier
            if (this.ProfileId == 0)
                validationResults.Add(new ValidationResult(Messages.validation_ProfileAddressProfileIDCannotBeEmpty,
                                                          new string[] { "ProfileId" }));

            //-->Check Address identifier
            if (this.AddressId == 0)
                validationResults.Add(new ValidationResult(Messages.validation_ProfileAddressAddressIDCannotBeEmpty,
                                                          new string[] { "AddressId" }));

            //-->Check AddressType identifier
            if (this.AddressTypeId == 0)
                validationResults.Add(new ValidationResult(Messages.validation_ProfileAddressAddressTypeIDCannotBeEmpty,
                                                          new string[] { "AddressTypeId" }));

            return validationResults;
        }

        #endregion

    }
}
