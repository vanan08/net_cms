using Application.Core.ProfileModule.AddressAggregate;
using Application.Core.ProfileModule.PhoneAggregate;
using Application.Core.ProfileModule.ProfileAggregate;
using Application.DTO;
using Application.DTO.ProfileModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Manager.Conversion
{
    public static class Mapping
    {
        public static ProfileDTO ProfileToProfileDTO(Profile profile, List<AddressType> addressTypes, List<PhoneType> phoneTypes)
        {
            ProfileDTO objProfileDTO = new ProfileDTO();
            objProfileDTO.ProfileId = profile.ProfileId;
            objProfileDTO.FirstName = profile.FirstName;
            objProfileDTO.LastName = profile.LastName;
            objProfileDTO.Email = profile.Email;
            objProfileDTO.AddressDTO = new List<AddressDTO>();
            objProfileDTO.PhoneDTO = new List<PhoneDTO>();

            foreach (var profileAddress in profile.ProfileAddresses)
            {
                AddressDTO objAddressDTO = AddressToAddressDTO(profileAddress.Address);
                objAddressDTO.AddressTypeId = profileAddress.AddressTypeId;
                objProfileDTO.AddressDTO.Add(objAddressDTO);
            }

            foreach (var profilePhone in profile.ProfilePhones)
            {
                PhoneDTO objPhoneDTO = PhoneToPhoneDTO(profilePhone.Phone);
                objPhoneDTO.PhoneTypeId = profilePhone.PhoneTypeId;
                objProfileDTO.PhoneDTO.Add(objPhoneDTO);
            }
            return objProfileDTO;
        }

        public static AddressDTO AddressToAddressDTO(Address address)
        {
            AddressDTO objAddressDTO = new AddressDTO();
            if (address != null)
            {
                objAddressDTO.AddressId = address.AddressId;
                objAddressDTO.AddressLine1 = address.AddressLine1;
                objAddressDTO.AddressLine2 = address.AddressLine2;
                objAddressDTO.ZipCode = address.ZipCode;
                objAddressDTO.Country = address.Country;
                objAddressDTO.State = address.State;
                objAddressDTO.City = address.City;
            }
            return objAddressDTO;
        }

        public static List<AddressTypeDTO> AddressTypeToAddressTypeDTO(List<AddressType> addressTypes)
        {
            List<AddressTypeDTO> lstAddressTypeDTO = new List<AddressTypeDTO>();
 
            foreach (AddressType addressType in addressTypes)
            {
                AddressTypeDTO objAddressTypeDTO = new AddressTypeDTO();
                objAddressTypeDTO.AddressTypeId = addressType.AddressTypeId;
                objAddressTypeDTO.Name = addressType.Name;
                lstAddressTypeDTO.Add(objAddressTypeDTO);
            }
            return lstAddressTypeDTO;
        }

        public static List<PhoneTypeDTO> PhoneTypeToPhoneTypeDTO(List<PhoneType> phoneTypes)
        {
            List<PhoneTypeDTO> lstPhoneTypeDTO = new List<PhoneTypeDTO>();

            foreach (PhoneType phoneType in phoneTypes)
            {
                PhoneTypeDTO objPhoneTypeDTO = new PhoneTypeDTO();
                objPhoneTypeDTO.PhoneTypeId = phoneType.PhoneTypeId;
                objPhoneTypeDTO.Name = phoneType.Name;
                lstPhoneTypeDTO.Add(objPhoneTypeDTO);
            }
            return lstPhoneTypeDTO;
        }

        public static PhoneDTO PhoneToPhoneDTO(Phone phone)
        {
            PhoneDTO objPhoneDTO = new PhoneDTO();
            if (phone != null)
            {
                objPhoneDTO.PhoneId = phone.PhoneId;
                objPhoneDTO.Number = phone.Number;
            }
            return objPhoneDTO;
        }
    }
}

