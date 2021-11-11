using CoreAPI;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UserProfileController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        public IHttpActionResult Post(UserProfile user)
        {
            var mng = new UserProfileManager();
            user = mng.ValidateUser(user);
            apiResp = new ApiResponse();
            apiResp.Data = user;
            return Ok(apiResp);

            /*try
            {
                var mng = new UserProfileManager();
                user = mng.ValidateUser(user);
                apiResp = new ApiResponse();
                apiResp.Data = user;
                return Ok(apiResp);
                
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }*/
        }
    }
}