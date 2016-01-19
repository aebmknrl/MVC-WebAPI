using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVCTest.Models;
using System.Web;

namespace MVCTest.Controllers
{
    [RoutePrefix("api/Content")]

    public class ContentController : ApiController
    {
        [HttpGet]
        [Route("InfoTodos")]
        [Authorize(Roles = "Admin,User")]
        public string InfoTodos()
        {
            return "Hola a todos";
        }

        [Route("InfoAdmin")]
        [Authorize(Roles = "Admin")]
        public string InfoAdmin()
        {
            return "Hola admin";
        }

        [HttpGet]
        [Route("InfoUser")]
        [Authorize]
        public string InfoUser()
        {
            return HttpContext.Current.User.IsInRole("User").ToString();
        }

        [Route("GetRoles")]
        [Authorize]
        public List<Role> GetRoles()
        {
            RolesTasks objRoles = new RolesTasks();
            List<Role> ListaRoles = objRoles.obtainRoles();
            return ListaRoles;
        }
    }
}