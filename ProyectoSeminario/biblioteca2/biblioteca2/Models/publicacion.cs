using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using biblioteca2.Models;

namespace biblioteca2.Models
{
    public class publicmodel
    {
        public int idPublicacion { get; set; }
        
        public string Titulo { get; set; }
        
        public string Portada { get; set; }
        
        public string Corecciones { get; set; }
        
        public string Puntaje { get; set; }

        public string Fecha_Publicacion { get; set; }
        
        public string Descripcion { get; set; }
        
        public string Autor { get; set; }
        
        public string Año_Publicacion { get; set; }
        
        public string Idioma { get; set; }
        
        public string Tamaño { get; set; }

        public string Tipo { get; set; }
        
        public string Nombre_Indice { get; set; }
        public string Url { get; set; }
        public string Correcciones { get; set; }
        //[UIHint("tinymce_jquery_full"), AllowHtml]
        public string Contenido { get; set; }
        public Guid idusers { get; set; }
        DataClasses1DataContext db = new DataClasses1DataContext();
        public void regpubli(publicmodel model)
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
        
        public void reglibro(publicmodel model,int idUs) {
            libro libro = new libro()
            {
                
                autor = model.Autor,
                año_publicacion = model.Año_Publicacion,
                idioma = model.Idioma,
                tamaño = model.Tamaño+" bytes",
                idPublicacion = idUs
            };
            db.libro.InsertOnSubmit(libro);
            db.SubmitChanges();
        }
        public void regcategoria(publicmodel model) {
            categoria categoria = new categoria() { 
                tipo = model.Tipo
            };
            db.categoria.InsertOnSubmit(categoria);
            db.SubmitChanges();
        }
        public void regcategorizacion(int idUs,int idcat) {
            categorizacion categorizacion = new categorizacion() { 
                idCategoria=idcat,
                idPublicacion=idUs
            };
            db.categorizacion.InsertOnSubmit(categorizacion);
            db.SubmitChanges();
        }
    }
}