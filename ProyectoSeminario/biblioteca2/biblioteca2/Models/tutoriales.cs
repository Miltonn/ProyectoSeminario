﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace biblioteca2.Models
{
    public class tutoriales
    {
        public int idPublicacion { set; get; }
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
        //[UIHint("tinymce_jquery_full"), AllowHtml]
        public string Contenido { get; set; }
        //        public string Contenido { get; set; }

        public Guid idusers { get; set; }

        DataClasses1DataContext db = new DataClasses1DataContext();
        public void regpublit(tutoriales model)
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


        public void regtutorial(int idUs)
        {
            tutorial libro = new tutorial()
            {
                idPublicacion = idUs
            };
            db.tutorial.InsertOnSubmit(libro);
            db.SubmitChanges();
        }
        public void regcategoriat(string s)
        {
            categoria categoria = new categoria()
            {
                tipo = s
            };
            db.categoria.InsertOnSubmit(categoria);
            db.SubmitChanges();
        }
        public void regcategorizaciont(int idUs, int cat)
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