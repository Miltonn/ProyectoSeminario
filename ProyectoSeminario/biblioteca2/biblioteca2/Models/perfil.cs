using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.ComponentModel.DataAnnotations;

namespace biblioteca2.Models
{
    public class perfilext
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        
        public string Avatar { get; set; }
        [Required]
        public string Ubicacion { get; set; }
        [Required]
        public string Interes { get; set; }
        
        public int infraccion { get; set; }
        public float kerma { get; set; }
        public string beneado { get; set; }
        public Guid iduser { get; set; }
        public Guid idrol { get; set; }
        
        public void regperfilusers(perfilext model,Guid idUsers)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            perfilusers users = new perfilusers()
            {
                nombre=model.Nombre,
                apellido=model.Apellido,
                avatar=model.Avatar,
                ubicacion=model.Ubicacion,
                interes=model.Interes,
                UserId=idUsers
            };
            db.perfilusers.InsertOnSubmit(users);
            db.SubmitChanges();
        }
        public void regperfil(Guid idus) 
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            perfil perfl = new perfil()
            {
                infraccion = 0,
                karma = 0,
                beneado = "false",
                UserId = idus
            };
            db.perfil.InsertOnSubmit(perfl);
            db.SubmitChanges();
        }
        public void regroles(Guid idus,Guid idrol) 
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            aspnet_UsersInRoles roles = new aspnet_UsersInRoles()
            {
                UserId=idus,
                RoleId=idrol
            };
            db.aspnet_UsersInRoles.InsertOnSubmit(roles);
            db.SubmitChanges();
        }
        
    }
    
}