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

        public UserRole userRoles(string user)
        {
            // Context de base de datos
            ApplicationDbContext db = new ApplicationDbContext();

            var rolesUsuario = (from x in db.Users
                                where x.UserName == user
                                select x.Roles).FirstOrDefault();

            // Obtener lista de id de roles
          
            List<Role> lstRolesId = new List<Role>();
            foreach (IdentityUserRole role in rolesUsuario)
            {
               
                lstRolesId.Add(new Role
                {
                    id = role.RoleId
                });
            }

            // Obtener lista de nombres correspondientes a los roles id
            foreach (Role rolId in lstRolesId)
            {
                string queryRolName = (from x in db.Roles
                                    where x.Id == rolId.id
                                    select x.Name).FirstOrDefault();

                rolId.Rol = queryRolName;
            }


            return new UserRole(user,lstRolesId);

        }
        
    }
    public class Role
    {
        public string id { get; set; }
        public string Rol { get; set; }
    }

    public class UserRole
    {
        public string User { get; set; }
        public DateTime DateOfQuery { get; }
        public List<Role> Roles { get; set; }

        // Constructor
        public UserRole(string User, List<Role> Roles)
        {
            this.DateOfQuery = DateTime.Now;
            this.User = User;
            this.Roles = Roles;
        }
    }

    }