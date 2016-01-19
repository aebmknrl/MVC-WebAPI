using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCTest.Models
{
    public class RolesTasks
    {
       public List<Role> obtainRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var roles = roleManager.Roles.ToList();
            List<Role> ListaRoles = new List<Role>();
            foreach (var role in roles)
            {
                ListaRoles.Add(new Role() {
                    id = role.Id,
                    Rol = role.Name
                });
            }
            return ListaRoles;
        }

        public List<UserRole> userRoles(string user)
        {
            List<UserRole> UserRolesCollection = new List<UserRole>();

            

            List<string> userRoles = Roles.GetRolesForUser(user).ToList();

            List<Role> listOfRoles = obtainRoles();

            foreach (string rol in userRoles)
            {
                string _id = (from x in listOfRoles
                              where x.Rol == rol
                              select x.id).FirstOrDefault();
                UserRolesCollection.Add(new UserRole
                {
                    id = _id,
                    Rol = rol,
                    User = user,
                    DateOfQuery = DateTime.Now.ToString()
                });
            }

            return UserRolesCollection;

        }

        public static void setOnRoles()
        {
            Roles.Enabled = true;
        }
    }
    public class Role
    {
        public string id { get; set; }
        public string Rol { get; set; }
    }

    public class UserRole : Role
    {
        public string User { get; set; }
        public string DateOfQuery { get; set; }
    }


}