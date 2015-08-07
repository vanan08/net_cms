using Application.Common;
using Application.Core.ProfileModule.PhoneAggregate;
using Application.Core.ProfileModule.ProfileAggregate;
using Application.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Core.ProfileModule.ProfilePhoneAggregate
{
    public partial class ProfilePhone : Entity, IValidatableObject
    {
        #region Property

        [Key]
        public int ProfilePhoneId { get; set; }
        public int ProfileId { get; set; }
        public int PhoneId { get; set; }
        public int PhoneTypeId { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Phone Phone { get; set; }
        public virtual PhoneType PhoneType { get; set; }
        public virtual Profile Profile { get; set; }

        #endregion Property

        #region Public Methods

        /// <summary>
        /// Associate existing Profile to this ProfilePhone
        /// </summary>
        /// <param name="profile"></param>
        public void AssociateProfileForThisProfilePhone(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentException(Messages.exception_ProfilePhoneCannotAssociateNullProfile);
            }

            //fix relation
            this.ProfileId = profile.ProfileId;

            this.Profile = profile;
        }

        /// <summary>
        /// Set the Profile reference for this ProfilePhone
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
        /// Associate existing Phone to this ProfilePhone
        /// </summary>
        /// <param name="phone"></param>
        public void AssociatePhoneForThisProfilePhone(Phone phone)
        {
            if (phone == null)
            {
                throw new ArgumentException(Messages.exception_ProfilePhoneCannotAssociateNullPhone);
            }

            //fix relation
            this.PhoneId = phone.PhoneId;

            this.Phone = phone;
        }

        /// <summary>
        /// Set the Phone reference for this ProfilePhone
        /// </summary>
        /// <param name="phoneId"></param>
        public void SetThePhoneReference(int phoneId)
        {
            if (phoneId != 0)
            {
                //fix relation
                this.PhoneId = phoneId;

                this.Phone = null;
            }
        }

        /// <summary>
        /// Associate existing PhoneType to this ProfilePhone
        /// </summary>
        /// <param name="phoneType"></param>
        public void AssociatePhoneTypeForThisProfilePhone(PhoneType phoneType)
        {
            if (phoneType == null)
            {
                throw new ArgumentException(Messages.exception_ProfilePhoneCannotAssociateNullPhoneType);
            }

            //fix relation
            this.PhoneTypeId = phoneType.PhoneTypeId;

            this.PhoneType = phoneType;
        }

        /// <summary>
        /// Set the PhoneType reference for this ProfilePhone
        /// </summary>
        /// <param name="phoneTypeId"></param>
        public void SetThePhoneTypeReference(int phoneTypeId)
        {
            if (phoneTypeId != 0)
            {
                //fix relation
                this.PhoneTypeId = phoneTypeId;

                this.PhoneType = null;
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
                validationResults.Add(new ValidationResult(Messages.validation_ProfilePhoneProfileIDCannotBeEmpty,
                                                          new string[] { "ProfileId" }));

            //-->Check Phone identifier
            if (this.PhoneId == 0)
                validationResults.Add(new ValidationResult(Messages.validation_ProfilePhonePhoneIDCannotBeEmpty,
                                                          new string[] { "PhoneId" }));

            //-->Check AddressType identifier
            if (this.PhoneTypeId == 0)
                validationResults.Add(new ValidationResult(Messages.validation_ProfilePhonePhoneTypeIDCannotBeEmpty,
                                                          new string[] { "PhoneTypeId" }));

            return validationResults;
        }

        #endregion

    }
}
