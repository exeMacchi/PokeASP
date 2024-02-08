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
    public partial class Admin : System.Web.UI.Page
    {
        public List<Pokemon> Pokemons { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Pokemons"] = PokemonBBL.GetPokemons();
            }

            Pokemons = (List<Pokemon>)Session["Pokemons"];
            if (Pokemons.Count > 0)
            {
                gvPokemons.DataSource = Pokemons;
                gvPokemons.DataBind();
            }
        }

        /// <summary>
        /// Evento que ocurre cuando se cambia de página (paginación activada)
        /// </summary>
        protected void gvPokemons_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPokemons.PageIndex = e.NewPageIndex;
            gvPokemons.DataBind();
        }

        /// <summary>
        /// Evento que se lanza cuando se quiere modificar un pokemon.
        /// </summary>
        protected void gvPokemons_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Acceder al DataKeyNames del GridView
            string id = gvPokemons.SelectedDataKey.Value.ToString();
            Response.Redirect($"Create_Edit.aspx?id={id}", false);
        }
    }
}