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
    public partial class Default : System.Web.UI.Page
    {
        public List<Pokemon> Pokemons { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Esto verifica si ya existe una lista de Pokemons en sesión
                if (!(Session["Pokemons"] != null))
                {
                    Session["Pokemons"] = PokemonBBL.GetPokemons();
                }
            }

            Pokemons = (List<Pokemon>)Session["Pokemons"];
            if (Pokemons.Count > 0)
            {
                PokeCards.DataSource = Pokemons;
                PokeCards.DataBind();
            }
        }
    }
}