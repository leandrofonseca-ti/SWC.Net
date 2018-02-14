using SWC.Data.Entity;
using SWC.Data.Helper;
using SWC.Data.Interface;
using SWC.Data.Service;
using SWC.REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SWC.REST.Controllers
{
    public class ServiceController : ApiController
    {
        private IScheduleRepository _scheduleService;
        private IUserRepository _userService;


        public ServiceController(IScheduleRepository scheduleService, IUserRepository userService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
        }


        #region USER

        [HttpGet]
        [Route("api/service/Login/{phone}/{password}")]
        public JsonResponse Login(string phone, string password)
        {
            JsonResponse data = new JsonResponse();
            data.STATUS = false;
            var result = _userService.ValidateUserPhone(phone, password);
            if (result.USERID > 0 && result.ACTIVE)
            {
                data.DATA = GetResultLogin(result);
                data.STATUS = true;
            }
            else
            {
                if (result.USERID > 0 && result.ACTIVE == false)
                {
                    data.MESSAGE = "User disabled.";
                }
                else if (String.IsNullOrEmpty(result.PHONE))
                {
                    data.MESSAGE = "User not found!";
                }
                else
                {
                    data.MESSAGE = "User/Password incorrect!";
                }
            }
            return data;
        }

        [HttpGet]
        [Route("api/service/ForgotPassword/{phone}")]
        public JsonResponse ForgotPassword(string phone)
        {
            JsonResponse data = new JsonResponse();
            data.STATUS = false;
            //HttpContext.Current.Request.Form
            var result = _userService.ExistUserPhone(phone);

            if (result.USERID > 0 && result.ACTIVE)
            {

                bool sentEmail = new Util().SendMailEsqueciSenha(result.USERID.Value, result.NAME, result.EMAIL);
                if (sentEmail)
                {
                    data.DATA = GetResultLogin(result);
                    data.STATUS = true;
                }
            }
            else
            {

                if (result.USERID > 0 && result.ACTIVE == false)
                {
                    data.MESSAGE = "User disabled.";
                }
                else if (String.IsNullOrEmpty(result.PHONE))
                {
                    data.MESSAGE = "User not found!";
                }
                else
                {
                    data.MESSAGE = "User/Password incorrect!";
                }
            }

            return new JsonResponse() { STATUS = true, MESSAGE = "", DATA = "" };
        }

        #endregion USER


        #region SCHEDULE

        [HttpGet]
        [Route("api/service/UpdateScheduleStaff/{userid}/{scheduleitemid}/{description}")]
        public JsonResponse UpdateScheduleStaff(int userid, int scheduleitemid, string description)
        {
            JsonResponse data = new JsonResponse();
            data.STATUS = false;
            try
            {

                var result = _scheduleService.UpdateScheduleStaff(userid, scheduleitemid, description);

                if (result)
                {
                    data.STATUS = true;
                }
                else
                {
                    data.MESSAGE = "Operation not completed.";
                }

            }
            catch (Exception ex)
            {
                data.MESSAGE = ex.Message;
            }
            return data;
        }

        [HttpGet]
        [Route("api/service/GetAllExtraTasks")]
        public JsonResponse GetAllExtraTasks()
        {
            JsonResponse data = new JsonResponse();
            data.STATUS = false;
            try
            {
                var results = _scheduleService.LoadExtraTasks();
                data.DATA = results;
                data.STATUS = true;
            }
            catch (Exception ex)
            {
                data.MESSAGE = ex.Message;
            }
            return data;
        }

        [HttpGet]
        [Route("api/service/GetAllTasksByPlan/{plantaskid}")]
        public JsonResponse GetAllTasksByPlan(int plantaskid)
        {
            JsonResponse data = new JsonResponse();
            data.STATUS = false;
            try
            {
                var results = _scheduleService.LoadTasksByPlan(plantaskid);
                data.DATA = results;
                data.STATUS = true;

            }
            catch (Exception ex)
            {
                data.MESSAGE = ex.Message;
            }
            return data;
        }

        [HttpGet]
        [Route("api/service/LoadScheduleByUser/{userid}")]
        public JsonResponse LoadScheduleByUser(int userid)
        {
            JsonResponse data = new JsonResponse();
            data.STATUS = false;
            try
            {
                var result = _scheduleService.LoadScheduleByUser(userid);

                if (result.Any())
                {
                    data.DATA = FormatJsonSchedule(result);
                    data.STATUS = true;
                }

            }
            catch (Exception ex)
            {
                data.MESSAGE = ex.Message;
            }
            return data;
        }

        [HttpGet]
        [Route("api/service/LoadScheduleItem/{id}")]
        public JsonResponse LoadScheduleItem(int id)
        {
            JsonResponse data = new JsonResponse();
            data.STATUS = false;
            try
            {

                var result = _scheduleService.GetScheduleItem(id);

                if (result.ID > 0)
                {
                    data.DATA = result;
                    data.STATUS = true;
                }

            }
            catch (Exception ex)
            {
                data.MESSAGE = ex.Message;
            }
            return data;
        }

        [HttpGet]
        [Route("api/service/LoadPlanTask")]
        public JsonResponse LoadPlanTask()
        {
            JsonResponse data = new JsonResponse();
            data.STATUS = false;
            try
            {
                var results = _scheduleService.LoadPlan();
                data.DATA = results;
                data.STATUS = true;

            }
            catch (Exception ex)
            {
                data.MESSAGE = ex.Message;
            }
            return data;
        }




        [HttpPut]
        [Route("api/service/SaveScheduleItem/{JsonScheduleItem}")]
        public JsonResponse SaveScheduleItem(JsonScheduleItem entity)
        {
            JsonResponse data = new JsonResponse();
            data.STATUS = false;
            try
            {
                //var allday = 1;
                var result = _scheduleService.SaveScheduleItem(entity.ID, entity.CUSTOMERID, entity.PERIODTYPEID, entity.TIME, entity.DATE);

                if (result)
                {
                    data.STATUS = true;
                }
                else
                {
                    data.MESSAGE = "Operation not completed.";
                }

            }
            catch (Exception ex)
            {
                data.MESSAGE = ex.Message;
            }
            return data;
        }
        #endregion SCHEDULE


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region PRIVATE METHODS

        private clsUserJson GetResultLogin(clsUser response)
        {
            return new clsUserJson()
            {
                ID = response.USERID.Value,
                PHONE = response.PHONE,
                NAME = response.NAME,
                LASTNAME = response.NAME,
                OCCUPATION = response.PROFILENAME,
                PROFILEID = response.PROFILEID
            };
        }

        private IEnumerable<object> FormatJsonSchedule(IList<clsSchedule> items)
        {
            foreach (var item in items)
            {
                yield return new
                {
                    ID = item.ID,
                    NAME = item.NAME,
                    PERIOD_NAME = item.PERIOD_NAME,
                };
            }
        }
        #endregion PRIVATE METHODS
    }
}
