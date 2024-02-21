using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Vista
{
    public partial class PokeDetail : System.Web.UI.Page
    {
        public Pokemon Poke { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Middleware necesario por si se entra a esta página desde la URL
                if (Request.QueryString["id"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                int id = int.Parse(Request.QueryString["id"]);
                Poke = PokemonBBL.GetPokemon(id);
            }
        }
    }
}