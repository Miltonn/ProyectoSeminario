using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;

namespace biblioteca2.Models
{
    public class perfilext
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string avatar { get; set; }
        public ImageFormat ImageFormat { get; set; }
        public string ubicacion { get; set; }
        public string interes { get; set; }
        public int infraccion { get; set; }
        public float kerma { get; set; }
        public string beneado { get; set; }
        public int iduser { get; set; }
    }
}