using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.DTO.ProfileModule
{
    public class ContactForm
    {
        public List<AddressTypeDTO> lstAddressTypeDTO { get; set; }
        public List<PhoneTypeDTO> lstPhoneTypeDTO { get; set; }
    }
}
