using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using writeMeEverythingASP.Data;

namespace writeMeEverythingASP.Helper
{
    public class Auth : ActionFilterAttribute
    {
        private readonly ChatContext _context;

        public Auth()
        {
            _context = new ChatContext();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Token Missing
            if (HttpContext.Current.Request.Headers["token"] == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                 HttpStatusCode.BadRequest,
                 new { message = "Token is missing" },
                 actionContext.ControllerContext.Configuration.Formatters.JsonFormatter
                );
                return;
            }

            string token = HttpContext.Current.Request.Headers["token"].ToString();

            var user = _context.Users.FirstOrDefault(u => u.Token == token);

            // Token incorrect
            if (user == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                 HttpStatusCode.Unauthorized,
                 new { message = "Token is incorrect" },
                 actionContext.ControllerContext.Configuration.Formatters.JsonFormatter
                );
                return;
            }

            actionContext.Request.Properties.Add("UserId", user.Id);
        }
    }
}