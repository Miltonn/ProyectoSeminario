using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace biblioteca2.Models
{
    public class articulomodelo
    {
        public int idPublicacion { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Portada { get; set; }
        public string Corecciones { get; set; }
        public string Puntaje { get; set; }
        [Required]
        public string Categoria { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string Fecha_Publicacion { get; set; }

        [Required]
        
        public string Contenido { get; set; }
        //        public string Contenido { get; set; }

        public Guid idusers { get; set; }

        DataClasses1DataContext db = new DataClasses1DataContext();
        public void regpubli(articulomodelo model)
        {

            publicacion publicacion = new publicacion()
            {
                titulo = model.Titulo,
                portada = model.Portada,
                contenido = model.Contenido,
                correcciones = "false",
                puntaje = 0,
                fecha_publicacion = DateTime.Today,
                descripcion = model.Descripcion,
                UserId = model.idusers
            };
            db.publicacion.InsertOnSubmit(publicacion);
            db.SubmitChanges();

        }


        public void regarticulo(int idUs)
        {
            articulo libro = new articulo()
            {
                idPublicacion = idUs
            };
            db.articulo.InsertOnSubmit(libro);
            db.SubmitChanges();
        }
        public void regcategoria(string s)
        {
            categoria categoria = new categoria()
            {
                tipo = s
            };
            db.categoria.InsertOnSubmit(categoria);
            db.SubmitChanges();
        }
        public void regcategorizacion(int idUs, int cat)
        {
            categorizacion categorizacion = new categorizacion()
            {
                idCategoria = cat,
                idPublicacion = idUs
            };
            db.categorizacion.InsertOnSubmit(categorizacion);
            db.SubmitChanges();
        }

       
    }
}