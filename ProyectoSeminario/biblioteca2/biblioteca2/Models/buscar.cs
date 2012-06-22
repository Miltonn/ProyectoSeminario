using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace biblioteca2.Models
{
    public class buscar
    {
        public string buscarr { get; set; }
        
        public string Email { get; set; }
        
        public string Comentario { get; set; }
        public string VistoBueno { get; set; }
        public string fecha { get; set; }
        public string bus { get; set; }
        
        public int idPublicacion { get; set; }
        public Guid UserId { get; set; }
        public void comentario(buscar model) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            comentarios c = new comentarios()
            {
                contenido=model.Comentario,
                vistobueno="false",
                fecha_publicacion=DateTime.Now,
                idPublicacion = model.idPublicacion,
                UserId=model.UserId
                
            };
            db.comentarios.InsertOnSubmit(c);
            db.SubmitChanges();
        }

       /* public void perfil(buscar model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            perfil p = new perfil()
            {
                karma=karma+0.5,
                //karma=(float)System.Data.EntityState.Modified
            };
            db.SubmitChanges();
        }*/
    
    }
    
}