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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Session["Pokemons"] = PokemonBBL.GetPokemons();

                    if (((List<Pokemon>)Session["Pokemons"]).Count > 0)
                    {
                        PokeCards.DataSource = (List<Pokemon>)Session["Pokemons"];
                        PokeCards.DataBind();
                    }
                    else
                    {
                        // Alerta de no registros
                    }
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex);
                    throw ex;
                }
            }

        }
    }
}