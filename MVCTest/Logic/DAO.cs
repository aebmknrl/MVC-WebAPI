using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTest.Logic
{
    public class DAO
    {
        public static void addRoleToUser(ApplicationUser user, string role)
        {
            // EL SIGUIENTE CODIGO AGREGA AL USUARIO UN ROL
            ApplicationDbContext context = new ApplicationDbContext(); 
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRole(user.Id,role);
        }
    }
}