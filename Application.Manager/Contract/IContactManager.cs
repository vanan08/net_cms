using Application.DTO;
using Application.DTO.ProfileModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Manager.Contract
{
    public interface IContactManager
    {
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<ProfileDTO> FindProfiles(int pageIndex, int pageCount);

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProfileDTO FindProfileById(int id);

        /// <summary>
        /// To Delete a Profile
        /// </summary>
        /// <param name="profileId"></param>
        void DeleteProfile(int profileId);

         /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        void SaveProfileInformation(ProfileDTO profileDTO);

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        void UpdateProfileInformation(int id, ProfileDTO profileDTO);

        /// <summary>
        /// Get all initialization data for Contact page
        /// </summary>
        /// <returns></returns>
        ContactForm InitializePageData();
    }
}
