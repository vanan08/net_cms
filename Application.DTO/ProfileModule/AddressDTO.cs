using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.DTO.ProfileModule
{
    public class AddressDTO
    {
        public int AddressTypeId { get; set; }
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
