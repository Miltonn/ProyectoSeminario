using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biblioteca2.Models
{
    public class soliadmin
    {
        public string solisitud { set; get; }
        public DateTime fecha { set; get; }
        public Guid UserId { set; get; }
        
        public Guid RoleId { set;get; }

        DataClasses1DataContext db = new DataClasses1DataContext();
        public void regsolisitud(soliadmin model)
        {
            solisitud solisitud=new solisitud()
            {
                solisitudes="solisita ser Administrador",
                fecha = DateTime.Today,
                UserId = model.UserId
            };
            db.solisitud.InsertOnSubmit(solisitud);
            db.SubmitChanges();
        }
        public void regroll(Guid idu,Guid idr)
        {
            aspnet_UsersInRoles rol = new aspnet_UsersInRoles()
            {
                UserId = idu,
                RoleId=idr
            };
            db.aspnet_UsersInRoles.InsertOnSubmit(rol);
            db.SubmitChanges();

        }
    }
}