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

                    if (((List<Pokemon>)Session["Pokemons"]).Count > 0)
                    {
                        alertPokemonNotFound.Visible = false;
                        alertEmptyGV.Visible = false;

                        gvPokemons.DataSource = (List<Pokemon>)Session["Pokemons"];
                        gvPokemons.DataBind();
                    }
                    else
                    {
                        alertPokemonNotFound.Visible = false; // Por las dudas
                        alertEmptyGV.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex);
                    throw;
                }
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
            string id = gvPokemons.SelectedDataKey.Value.ToString();
            Response.Redirect($"Create_Edit.aspx?id={id}", false);
        }

        /// <summary>
        /// Lanzar el modal de advertencia sobre la eliminación lógica del registro
        /// seleccionado.
        /// </summary>
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

        /// <summary>
        /// Eliminar de forma lógica un registro en la base de datos (Activo = !Activo).
        /// </summary>
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

        /// <summary>
        /// Filtrar por nombre la lista de pokemons activos en sesión y actualizar
        /// el GridView con los resultados obtenidos.
        /// </summary>
        protected void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = txtFilter.Text;
            
            List<Pokemon> pokeFiltered = ((List<Pokemon>)Session["Pokemons"]).FindAll(p => p.Name.ToUpper().Contains(filter.ToUpper()));

            gvPokemons.DataSource = pokeFiltered;
            gvPokemons.DataBind();

            if (pokeFiltered.Count == 0)
            {
                alertEmptyGV.Visible = false; // Por las dudas
                alertPokemonNotFound.Visible = true;
            }
        }
    }
}