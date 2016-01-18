using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                    ID = role.Id,
                    Nombre = role.Name
                });
            }
            return ListaRoles;
        }


    }
    public class Role
    {
        public string Nombre { get; set; }
        public string ID { get; set; }
    }
}