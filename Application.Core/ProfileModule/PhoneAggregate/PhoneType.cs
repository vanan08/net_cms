using Application.Common;
using Application.Core.ProfileModule.ProfilePhoneAggregate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Core.ProfileModule.PhoneAggregate
{
    public class PhoneType : Entity
    {
        #region Constructor

        public PhoneType()
        {
            this.ProfilePhones = new HashSet<ProfilePhone>();
        }

        #endregion Constructor

        #region Property

        [Key]
        public int PhoneTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProfilePhone> ProfilePhones { get; set; }

        #endregion Property

    }
}
