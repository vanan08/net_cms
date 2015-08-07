using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.DTO.ProfileModule
{
    public class ProfileDTO
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<PhoneDTO> PhoneDTO { get; set; }
        public List<AddressDTO> AddressDTO { get; set; }

    }

    
}
