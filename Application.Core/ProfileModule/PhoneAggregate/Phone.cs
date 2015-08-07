using Application.Common;
using Application.Core.ProfileModule.ProfilePhoneAggregate;
using Application.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Core.ProfileModule.PhoneAggregate
{
    public partial class Phone : Entity, IValidatableObject
    {
        #region Constructor

        public Phone()
        {
            this.ProfilePhones = new HashSet<ProfilePhone>();
        }

        #endregion Constructor

        #region Property

        [Key]
        public int PhoneId { get; set; }
        public string Number { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<ProfilePhone> ProfilePhones { get; set; }

        #endregion Property


        #region IValidatableObject Members

        /// <summary>
        /// This will validate entity for all  the conditions
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            //-->Check FirstName property
            if (String.IsNullOrWhiteSpace(this.Number))
            {
                validationResults.Add(new ValidationResult(Messages.validation_PhoneNumberCannotBeNull,
                                                           new string[] { "Number" }));
            }

            return validationResults;
        }

        #endregion

    }
}
