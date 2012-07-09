using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace biblioteca2.Models
{
    public class GridDemo
    {
        public GridDemo() 
        {
            grid = new JQGrid()
            {
                Columns = new List<JQGridColumn>() 
                { 
                    new JQGridColumn()
                    {
                        DataField="idPerfil",
                        PrimaryKey=true,
                        Editable=false,
                        Width=(100)
                    },
                    new JQGridColumn()
                    {
                        DataField="infraccion",
                        Editable=true,
                        Width=150
                    },
                    new JQGridColumn()
                    {
                        DataField="karma",
                        Editable=true,
                        Width=150
                    },
                    new JQGridColumn()
                    {
                        DataField="beneado",
                        Editable=true,
                        Width=150
                    }
                    
                },
                Width=Unit.Pixel(600)
            };
            grid.ToolBarSettings.ShowRefreshButton = true;
        }
        public JQGrid grid { set; get; }
    }
    class perfilgrid 
    {
        public perfilgrid() 
        {
            perfil = new JQGrid()
            {
                Columns = new List<JQGridColumn>() 
                { 
                    new JQGridColumn()
                    {
                        DataField="nombre",
                        PrimaryKey=true,
                        Editable=false,
                        Width=(20)
                    },
                    new JQGridColumn()
                    {
                        DataField="apellido",
                        Editable=true,
                        Width=150
                    }
                },
                Width = Unit.Pixel(600)
            };
        }public JQGrid perfil { set; get; } 
    }
}