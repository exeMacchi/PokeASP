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
                Pokemons = PokemonBBL.GetPokemons();
            }

            if (Pokemons.Count > 0)
            {
                gvPokemons.DataSource = Pokemons;
                gvPokemons.DataBind();
            }
        }
    }
}