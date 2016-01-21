using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCTest.Logic;
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

        public object addUserRoles(string userID, List<Role> newUserRoles)
        {
            // Context de bd
            ApplicationDbContext db = new ApplicationDbContext();

            try
            { 
                // Obtengo los roles del usuario
                ICollection<IdentityUserRole> queryRolesUser = (from x in db.Users
                                  where x.Id == userID
                                  select x.Roles).FirstOrDefault();


                // Agrego a los nombres de roles sus respectivos ID de rol
                foreach (Role role in newUserRoles)
                {
                    string queryIdOfRoles = (from x in db.Roles
                                            where x.Name == role.Rol
                                            select x.Id).FirstOrDefault();
                    newUserRoles[newUserRoles.IndexOf(role)].id = queryIdOfRoles;

                }   
                
                // Agregar roles                   
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);
           
                foreach (Role role in newUserRoles)
                {
                    var query = (from y in queryRolesUser
                                 where y.RoleId == role.id
                                 select y).Count();
                    // Si el usuario no posee el rol => agregarlo, de lo contrario lo ignoro
                    if (query <= 0)
                    {
                        userManager.AddToRole(userID, role.Rol);
                    }
                }

                return new Info("Ok", "Roles actualizados correctamente al Usuario: " + userID);
            }
            catch (Exception e)
            {
                return new InfoError(e.StackTrace,"Error",e.Message);
            }
            
            
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
        public UserRole()
        {
            this.Roles = null;
            this.User = "";
        }
        public UserRole(string User, List<Role> Roles)
        {
            this.DateOfQuery = DateTime.Now;
            this.User = User;
            this.Roles = Roles;
        }
    }

    }