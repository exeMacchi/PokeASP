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
    public partial class InactivePoke : System.Web.UI.Page
    {
        public List<Pokemon> InactivePokemons { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Session["InactivePokemons"] = PokemonBBL.GetInactivePokemons();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex);
                    throw;
                }
            }

            InactivePokemons = (List<Pokemon>)Session["InactivePokemons"];
            if (InactivePokemons.Count > 0)
            {
                gvInactivePokemons.DataSource = InactivePokemons;
                gvInactivePokemons.DataBind();
            }
        }

        protected void gvInactivePokemons_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInactivePokemons.PageIndex = e.NewPageIndex;
            gvInactivePokemons.DataBind();
        }

        protected void gvInactivePokemons_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvInactivePokemons.SelectedDataKey.Value.ToString();
            string nombre = gvInactivePokemons.SelectedRow.Cells[1].Text;

            lbReactivePokemon.Text = nombre;
            btnReactiveConfirm.CommandArgument = id;

            string script = @"<script type='text/javascript'>
                                document.addEventListener('DOMContentLoaded', function() {
                                    document.getElementById('btnReactivePokemon').click();
                                });
                              </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", script);
        }

        protected void gvInactivePokemons_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvInactivePokemons.DataKeys[e.RowIndex].Value.ToString();
            string nombre = gvInactivePokemons.Rows[e.RowIndex].Cells[1].Text;

            lbDeletePokemon.Text = nombre;
            btnDeleteConfirm.CommandArgument = id;

            string script = @"<script type='text/javascript'>
                                document.addEventListener('DOMContentLoaded', function() {
                                    document.getElementById('btnDeletePokemon').click();
                                });
                              </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", script);
        }


        protected void btnReactiveConfirm_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);

            try
            {
                PokemonBBL.DisableOrActivePokemon(id);
                Session["AlertMessage"] = "El Pokémon fue reactivado en la base de datos de forma exitosa.";
                Response.Redirect("Admin.aspx?alert=success", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnDeleteConfirm_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);

            try
            {
                PokemonBBL.DeletePokemon(id);
                Session["AlertMessage"] = "El Pokemon fue eliminado de la base de datos de forma exitosa.";
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