using Application.Common.Logging;
using Application.DTO;
using Application.Manager.Contract;
using Application.Manager.Resources;
using Application.Repository.ProfileModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Manager.Conversion;
using Application.Core.ProfileModule.ProfileAggregate;
using Application.Common.Validator;
using Application.Common;
using Application.Core.ProfileModule.AddressAggregate;
using Application.Core.ProfileModule.PhoneAggregate;
using Application.Core.ProfileModule.ProfileAddressAggregate;
using Application.Core.ProfileModule.ProfilePhoneAggregate;
using Application.DTO.ProfileModule;

namespace Application.Manager.Implementation
{
    public class ContactManager : IContactManager
    {
        #region Global Declearation

        AddressRepository _addressRepository;
        AddressTypeRepository _addressTypeRepository;
        PhoneRepository _phoneRepository;
        PhoneTypeRepository _phoneTypeRepository;
        ProfileAddressRepository _profileAddressRepository;
        ProfilePhoneRepository _profilePhoneRepository;
        ProfileRepository _profileRepository;

        #endregion Global Declearation

        #region Constructor

        public ContactManager(AddressRepository addressRepository,
                              AddressTypeRepository addressTypeRepository,
                              PhoneRepository phoneRepository,
                              PhoneTypeRepository phoneTypeRepository,
                              ProfileAddressRepository profileAddressRepository,
                              ProfilePhoneRepository profilePhoneRepository,
                              ProfileRepository profileRepository)
        {
            if (addressRepository == null)
                throw new ArgumentNullException("addressRepository");

            if (addressTypeRepository == null)
                throw new ArgumentNullException("addressTypeRepository");

            if (phoneRepository == null)
                throw new ArgumentNullException("phoneRepository");

            if (phoneTypeRepository == null)
                throw new ArgumentNullException("phoneTypeRepository");

            if (profileAddressRepository == null)
                throw new ArgumentNullException("profileAddressRepository");

            if (profilePhoneRepository == null)
                throw new ArgumentNullException("profilePhoneRepository");

            if (profileRepository == null)
                throw new ArgumentNullException("profileRepository");

            _addressRepository = addressRepository;
            _addressTypeRepository = addressTypeRepository;
            _phoneRepository = phoneRepository;
            _phoneTypeRepository = phoneTypeRepository;
            _profileAddressRepository = profileAddressRepository;
            _profilePhoneRepository = profilePhoneRepository;
            _profileRepository = profileRepository;
        }

        #endregion Constructor

        #region Interface Implementation

        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<ProfileDTO> FindProfiles(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var profiles = _profileRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.Created, false);
            var addressTypes = _addressTypeRepository.GetAll().ToList();
            var phoneTypes = _phoneTypeRepository.GetAll().ToList();

            if (profiles != null
                &&
                profiles.Any())
            {
                List<ProfileDTO> lstProfileDTO = new List<ProfileDTO>();
                foreach (var profile in profiles)
                {
                    lstProfileDTO.Add( Conversion.Mapping.ProfileToProfileDTO(profile, addressTypes, phoneTypes));
                }
                return lstProfileDTO;
            }
            else // no data
                return new List<ProfileDTO>();
        }

        /// <summary>
        /// Delete profile
        /// </summary>
        /// <param name="profileId"></param>
        public void DeleteProfile(int profileId)
        {
            var profile = _profileRepository.Get(profileId);

            if (profile != null) //if profile exist
            {
                //Delete all addresses associate with this profile 
                List<ProfileAddress> lstProfileAddress = profile.ProfileAddresses.ToList();
                foreach (ProfileAddress objProfileAddress in lstProfileAddress)
                {
                    this.DeleteProfileAddress(objProfileAddress);
                }

                //Delete all phones associate with this profile 
                List<ProfilePhone> lstProfilePhone = profile.ProfilePhones.ToList();
                foreach (ProfilePhone objProfilePhone in lstProfilePhone)
                {
                    this.DeleteProfilePhone(objProfilePhone);
                }

                _profileRepository.Remove(profile);

                //commit changes
                _profileRepository.UnitOfWork.Commit();
            }
            else //the customer not exist, cannot remove
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingProfile);
        }

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProfileDTO FindProfileById(int id)
        {

            //recover orders
            var profile = _profileRepository.Get(id);
            var addressTypes = _addressTypeRepository.GetAll().ToList();
            var phoneTypes = _phoneTypeRepository.GetAll().ToList();

            if (profile != null)
            {
                return Conversion.Mapping.ProfileToProfileDTO(profile, addressTypes, phoneTypes);
            }
            else //no data
                return new ProfileDTO();

        }

        /// <summary>
        /// Get all initialization data for Contact page
        /// </summary>
        /// <returns></returns>
        public ContactForm InitializePageData()
        {
            var addressTypes = _addressTypeRepository.GetAll().ToList();
            var phoneTypes = _phoneTypeRepository.GetAll().ToList();

            ContactForm objContactForm = new ContactForm();
            objContactForm.lstAddressTypeDTO = Conversion.Mapping.AddressTypeToAddressTypeDTO(addressTypes);
            objContactForm.lstPhoneTypeDTO = Conversion.Mapping.PhoneTypeToPhoneTypeDTO(phoneTypes);

            return objContactForm;
        }

        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        public void SaveProfileInformation(ProfileDTO profileDTO)
        {
            //if profileDTO data is not valid
            if (profileDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new profile entity
            var newProfile = ProfileFactory.CreateProfile(profileDTO.FirstName, profileDTO.LastName, profileDTO.Email, "Anand", DateTime.Now, "Anand", DateTime.Now);

            //Save Profile
            newProfile = SaveProfile(newProfile);

            //if profileDTO contains any address
            if (profileDTO.AddressDTO != null)
            {
                foreach (AddressDTO objAddressDTO in profileDTO.AddressDTO)
                {
                    this.SaveAddress(objAddressDTO, newProfile);
                }
            }

            //if profileDTO contains any phone
            if (profileDTO.PhoneDTO != null)
            {
                foreach (PhoneDTO objPhoneDTO in profileDTO.PhoneDTO)
                {
                    this.SavePhone(objPhoneDTO, newProfile);
                }
            }
        }

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        public void UpdateProfileInformation(int id, ProfileDTO profileDTO)
        {
            //if profileDTO data is not valid
            if (profileDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new profile entity
            var currentProfile = _profileRepository.Get(id);

            //Assign updated value to existing profile
            var updatedProfile = new Profile();
            updatedProfile.ProfileId = id;
            updatedProfile.FirstName = profileDTO.FirstName;
            updatedProfile.LastName = profileDTO.LastName;
            updatedProfile.Email = profileDTO.Email;

            //Update Profile
            updatedProfile = this.UpdateProfile(currentProfile, updatedProfile);

            //Update Address
            List<AddressDTO> lstUpdatedAddressDTO = profileDTO.AddressDTO;
            List<ProfileAddress> lstCurrentAddress = _profileAddressRepository.GetFiltered(x => x.ProfileId.Equals(id)).ToList();

            UpdateAddress (lstUpdatedAddressDTO, lstCurrentAddress, updatedProfile);

            //Update Phone
            List<PhoneDTO> lstUpdatedPhoneDTO = profileDTO.PhoneDTO;
            List<ProfilePhone> lstCurrentPhone = _profilePhoneRepository.GetFiltered(x => x.ProfileId.Equals(id)).ToList();

            UpdatePhone(lstUpdatedPhoneDTO, lstCurrentPhone, updatedProfile);
        }

        #endregion Interface Implementation

        #region Private Methods

        /// <summary>
        /// Add new address
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <param name="profile"></param>
        void SaveAddress(AddressDTO addressDTO, Profile profile)
        {
            //if addressDTO data is not valid
            if (addressDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new Address entity
            var newAddress = AddressFactory.CreateAddress(addressDTO.AddressLine1, addressDTO.AddressLine2, addressDTO.City, addressDTO.State, addressDTO.Country,
                                                          addressDTO.ZipCode, "Anand", DateTime.Now, "Anand", DateTime.Now);
            //Save new Address
            SaveAddress(newAddress);

            var addressType = _addressTypeRepository.Get(addressDTO.AddressTypeId);

            //Create a new Profile Address entity
            var newProfileAddress = ProfileAddressFactory.ProfileAddress(profile, newAddress, addressType, "Anand", DateTime.Now, "Anand", DateTime.Now);

            //Save new Address
            SaveProfileAddress(newProfileAddress);
        }

        /// <summary>
        /// Save Address
        /// </summary>
        /// <param name="address"></param>
        Address SaveAddress(Address address)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(address)) //if entity is valid save
            {
                //add address and commit changes
                _addressRepository.Add(address);
                _addressRepository.UnitOfWork.Commit();
                return address;
            }
            else //if not valid, throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(address));
        }

        /// <summary>
        /// Save Profile Address
        /// </summary>
        /// <param name="profileAddress"></param>
        void SaveProfileAddress(ProfileAddress profileAddress)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(profileAddress))//if entity is valid save. 
            {
                //add profile address and commit changes
                _profileAddressRepository.Add(profileAddress);
                _profileAddressRepository.UnitOfWork.Commit();
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(profileAddress));
        }

        /// <summary>
        /// Update profile address
        /// </summary>
        /// <param name="lstUpdatedAddressDTO"></param>
        /// <param name="lstCurrentAddress"></param>
        void UpdateAddress (List<AddressDTO> lstUpdatedAddressDTO, List<ProfileAddress> lstCurrentAddress, Profile profile)
        {
            //if addressDTO data is not valid
            if (lstUpdatedAddressDTO == null && lstCurrentAddress == null) return;

            #region If user has deleted all existing address

            if (lstUpdatedAddressDTO == null && lstCurrentAddress != null)
            {
                foreach (ProfileAddress profileAddress in lstCurrentAddress)
                {
                    DeleteProfileAddress(profileAddress);
                }
                return;
            }

            #endregion If user has deleted all existing address

            #region If user has added new address and there was not any existing address

            if (lstUpdatedAddressDTO != null && lstCurrentAddress == null)
            {
                foreach (AddressDTO addressDTO in lstUpdatedAddressDTO)
                {
                    this.SaveAddress(addressDTO, profile);
                }
                return;
            }

            #endregion If user has added new address and there was not any existing address

            #region if user has updated or Deleted any record

            List<AddressDTO> lstNewAddress = lstUpdatedAddressDTO;

            //Check if address exist in database
            foreach (ProfileAddress profileAddress in lstCurrentAddress)
            {
                AddressDTO objAddressDTO = lstUpdatedAddressDTO.FirstOrDefault(x => x.AddressId.Equals(profileAddress.AddressId));

                if (objAddressDTO != null)
                {
                    Address updatedAddress = new Address();
                    updatedAddress.AddressId = objAddressDTO.AddressId;
                    updatedAddress.AddressLine1 = objAddressDTO.AddressLine1;
                    updatedAddress.AddressLine2 = objAddressDTO.AddressLine2;
                    updatedAddress.City = objAddressDTO.City;
                    updatedAddress.State = objAddressDTO.State;
                    updatedAddress.Country = objAddressDTO.Country;
                    updatedAddress.ZipCode = objAddressDTO.ZipCode;
                    UpdateAddress(profileAddress.Address, updatedAddress);
                    lstNewAddress.Remove(objAddressDTO);
                }
                else
                {
                    DeleteProfileAddress(profileAddress);
                }
            }

            //Save new address
            foreach (AddressDTO addressDTO in lstNewAddress)
            {
                this.SaveAddress(addressDTO, profile);
            }

            #endregion if user has updated or Deleted any record
        }

        /// <summary>
        /// Update existing Address
        /// </summary>
        /// <param name="profile"></param>
        void UpdateAddress(Address currentAddress, Address updatedAddress)
        {
            updatedAddress.Created = currentAddress.Created;
            updatedAddress.CreatedBy = currentAddress.CreatedBy;
            updatedAddress.Updated = DateTime.Now;
            updatedAddress.UpdatedBy = "Updated User";

            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(updatedAddress))//if entity is valid save. 
            {
                //add profile and commit changes
                _addressRepository.Merge(currentAddress, updatedAddress);
                _addressRepository.UnitOfWork.Commit();
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(updatedAddress));
        }

        /// <summary>
        /// Delete profile address
        /// </summary>
        /// <param name="profileId"></param>
        public void DeleteProfileAddress(ProfileAddress profileAddress)
        {
            var address = _addressRepository.Get(profileAddress.AddressId);

            if (address != null) //if address exist
            {
                _profileAddressRepository.Remove(profileAddress);
                //commit changes
                _profileAddressRepository.UnitOfWork.Commit();

                _addressRepository.Remove(address);
                //commit changes
                _addressRepository.UnitOfWork.Commit();
            }
            else //the customer not exist, cannot remove
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingProfile);
        }

        /// <summary>
        /// Add new phone
        /// </summary>
        /// <param name="phoneDTO"></param>
        /// <param name="profile"></param>
        void SavePhone(PhoneDTO phoneDTO, Profile profile)
        {
            //if phoneDTO data is not valid
            if (phoneDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new Phone entity
            var newPhone = PhoneFactory.CreatePhone(phoneDTO.Number, "Anand", DateTime.Now, "Anand", DateTime.Now);

            //Save new Phone
            newPhone = SavePhone(newPhone);

            var phoneType = _phoneTypeRepository.Get(phoneDTO.PhoneTypeId);

            //Create a new Profile Phone entity
            var newProfilePhone = ProfilePhoneFactory.CreateProfilePhone(profile, newPhone, phoneType, "Anand", DateTime.Now, "Anand", DateTime.Now);

            //Save new Profile Phone
            SaveProfilePhone(newProfilePhone);
        }

        /// <summary>
        /// Save Phone
        /// </summary>
        /// <param name="phone"></param>
        Phone SavePhone(Phone phone)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(phone)) //if entity is valid save
            {
                //add phone and commit changes
                _phoneRepository.Add(phone);
                _phoneRepository.UnitOfWork.Commit();
                return phone;
            }
            else //if not valid, throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(phone));
        }
    
        /// <summary>
        /// Save Profile Phone
        /// </summary>
        /// <param name="profilePhone"></param>
        void SaveProfilePhone(ProfilePhone profilePhone)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(profilePhone))//if entity is valid save. 
            {
                //add profile phone and commit changes
                _profilePhoneRepository.Add(profilePhone);
                _profilePhoneRepository.UnitOfWork.Commit();
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(profilePhone));
        }

        /// <summary>
        /// Update profile phone
        /// </summary>
        /// <param name="lstUpdatedPhoneDTO"></param>
        /// <param name="lstCurrentPhone"></param>
        void UpdatePhone(List<PhoneDTO> lstUpdatedPhoneDTO, List<ProfilePhone> lstCurrentPhone, Profile profile)
        {
            //if addressDTO data is not valid
            if (lstUpdatedPhoneDTO == null && lstCurrentPhone == null) return;

            #region If user has deleted all existing Phone

            if (lstUpdatedPhoneDTO == null && lstCurrentPhone != null)
            {
                foreach (ProfilePhone profilePhone in lstCurrentPhone)
                {
                    DeleteProfilePhone(profilePhone);
                }
                return;
            }

            #endregion If user has deleted all existing Phone

            #region If user has added new Phone and there was not any existing Phone

            if (lstUpdatedPhoneDTO != null && lstCurrentPhone == null)
            {
                foreach (PhoneDTO phoneDTO in lstUpdatedPhoneDTO)
                {
                    this.SavePhone(phoneDTO, profile);
                }
                return;
            }

            #endregion If user has added new Phone and there was not any existing Phone

            #region if user has updated or Deleted any record

            List<PhoneDTO> lstNewPhone = lstUpdatedPhoneDTO;

            //Check if Phone exist in database
            foreach (ProfilePhone profilePhone in lstCurrentPhone)
            {
                PhoneDTO objPhoneDTO = lstUpdatedPhoneDTO.FirstOrDefault(x => x.PhoneId.Equals(profilePhone.PhoneId));

                if (objPhoneDTO != null)
                {
                    Phone updatedPhone = new Phone();
                    updatedPhone.PhoneId = objPhoneDTO.PhoneId;
                    updatedPhone.Number = objPhoneDTO.Number;
                    UpdatePhone(profilePhone.Phone, updatedPhone);
                    lstNewPhone.Remove(objPhoneDTO);
                }
                else
                {
                    DeleteProfilePhone(profilePhone);
                }
            }

            //Save new address
            foreach (PhoneDTO phoneDTO in lstNewPhone)
            {
                this.SavePhone(phoneDTO, profile);
            }

            #endregion if user has updated or Deleted any record
        }

        /// <summary>
        /// Update existing Phone
        /// </summary>
        /// <param name="currentPhone"></param>
        /// <param name="updatedPhone"></param>
        void UpdatePhone(Phone currentPhone, Phone updatedPhone)
        {

            updatedPhone.Created = currentPhone.Created;
            updatedPhone.CreatedBy = currentPhone.CreatedBy;
            updatedPhone.Updated = DateTime.Now;
            updatedPhone.UpdatedBy = "Updated User";

            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(updatedPhone))//if entity is valid save. 
            {
                //add profile and commit changes
                _phoneRepository.Merge(currentPhone, updatedPhone);
                _phoneRepository.UnitOfWork.Commit();
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(updatedPhone));
        }

        /// <summary>
        /// Delete profile phone
        /// </summary>
        /// <param name="profilePhone"></param>
        public void DeleteProfilePhone(ProfilePhone profilePhone)
        {
            var phone = _phoneRepository.Get(profilePhone.PhoneId);

            if (phone != null) //if phone exist
            {
                _profilePhoneRepository.Remove(profilePhone);
                //commit changes
                _profilePhoneRepository.UnitOfWork.Commit();

                _phoneRepository.Remove(phone);
                //commit changes
                _phoneRepository.UnitOfWork.Commit();
            }
            else //the customer not exist, cannot remove
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingProfile);
        }

        /// <summary>
        /// Save Profile
        /// </summary>
        /// <param name="profile"></param>
        Profile SaveProfile(Profile profile)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(profile))//if entity is valid save. 
            {
                //add profile and commit changes
                _profileRepository.Add(profile);
                _profileRepository.UnitOfWork.Commit();
                return profile;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(profile));
        }

        /// <summary>
        /// Update existing Profile
        /// </summary>
        /// <param name="profile"></param>
        Profile UpdateProfile(Profile currentProfile, Profile updatedProfile)
        {
            updatedProfile.Created = currentProfile.Created;
            updatedProfile.CreatedBy = currentProfile.CreatedBy;
            updatedProfile.Updated = DateTime.Now;
            updatedProfile.UpdatedBy = "Updated User";

            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(updatedProfile))//if entity is valid save. 
            {
                //add profile and commit changes
                _profileRepository.Merge(currentProfile, updatedProfile);
                _profileRepository.UnitOfWork.Commit();
                return updatedProfile;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(updatedProfile));
        }

        #endregion

    }
}
