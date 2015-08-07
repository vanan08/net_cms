using Application.DTO.ProfileModule;
using Application.Manager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Application.Web.Controllers
{
    public class ContactController : Controller
    {
        #region Global declaration

        private readonly IContactManager _contactManager;

        #endregion Global declaration

        #region Constructor

        public ContactController(IContactManager contactManager)
        {
            _contactManager = contactManager;
        }

        public ContactController()
        {

        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Get all Profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllProfiles()
        {
            var profiles = _contactManager.FindProfiles(0, 20).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Profile by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetProfileById(int id)
        {
            var profile = _contactManager.FindProfileById(id);
            return this.Json(profile, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create a new Profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpStatusCodeResult SaveProfileInformation(ProfileDTO profileDTO)
        {
            _contactManager.SaveProfileInformation(profileDTO);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update an existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPut]
        public HttpStatusCodeResult UpdateProfileInformation(int id, ProfileDTO profileDTO)
        {
            _contactManager.UpdateProfileInformation(id, profileDTO);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete an existing profile
        /// </summary>
        /// <param name="id"></param>
        [System.Web.Http.HttpDelete]
        public void DeleteProfile(int id)
        {
            try
            {
                if (id != 0)
                {
                    _contactManager.DeleteProfile(id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Get all initialization data for Contact page
        /// </summary>
        /// <returns></returns>
        public JsonResult InitializePageData()
        {
            var contactForm = _contactManager.InitializePageData();
            return this.Json(contactForm, JsonRequestBehavior.AllowGet);
        }

        #endregion Public Methods

        #region Views

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateEdit()
        {
            return View();
        }
        #endregion Views
    }
}
