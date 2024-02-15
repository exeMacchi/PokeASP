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
                try
                {
                    Session["Pokemons"] = PokemonBBL.GetPokemons();
                    if (PokemonBBL.VerifyInactives())
                    {
                        inactivePokemons.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex);
                    throw;
                }
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

        protected void gvPokemons_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvPokemons.DataKeys[e.RowIndex].Value.ToString();
            string nombre = gvPokemons.Rows[e.RowIndex].Cells[1].Text;

            lbDeletePokemon.Text = nombre;
            btnDeleteConfirm.CommandArgument = id;

            string script = @"<script type='text/javascript'>
                                document.addEventListener('DOMContentLoaded', function() {
                                    document.getElementById('btnDeletePokemon').click();
                                });
                              </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", script);
        }

        protected void btnDeleteConfirm_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);

            try
            {
                PokemonBBL.DisableOrActivePokemon(id);
                Session["AlertMessage"] = "El Pokemon fue eliminado de forma exitosa.";
                Response.Redirect("Admin.aspx?alert=success", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
    }
}