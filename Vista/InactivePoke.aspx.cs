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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Middleware admin
                if (!((Usuario)Session["UserSession"]).Admin)
                {
                    Response.Redirect("Login.aspx", false);
                }

                try
                {
                    Session["InactivePokemons"] = PokemonBBL.GetInactivePokemons();

                    if (((List<Pokemon>)Session["InactivePokemons"]).Count > 0)
                    {
                        alertPokemonNotFound.Visible = false;
                        alertEmptyGV.Visible = false;

                        gvInactivePokemons.DataSource = (List<Pokemon>)Session["InactivePokemons"];
                        gvInactivePokemons.DataBind();
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
        /// Evento que ocurre cuando se cambia de página (paginación activada).
        /// </summary>
        protected void gvInactivePokemons_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInactivePokemons.PageIndex = e.NewPageIndex;
            gvInactivePokemons.DataBind();
        }

        /// <summary>
        /// Lanzar un modal de notificación para la reactivación de un Pokémon
        /// seleccionado.
        /// </summary>
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

        /// <summary>
        /// Lanza un modal de advertencia para la eliminación física del 
        /// Pokémon seleccionado.
        /// </summary>
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


        /// <summary>
        /// Reactivar el Pokemon en la base de datos.
        /// </summary>
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

        /// <summary>
        /// Eliminar de forma física un Pokemon en la base de datos.
        /// </summary>
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

        /// <summary>
        /// Filtrar por nombre la lista de pokemons inactivos y actualizar el GridView
        /// con los resultados obtenidos.
        /// </summary>
        protected void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = txtFilter.Text;
            List<Pokemon> inactivesFiltered = ((List<Pokemon>)Session["InactivePokemons"]).FindAll(p => p.Name.ToUpper().Contains(filter.ToUpper()));

            gvInactivePokemons.DataSource = inactivesFiltered;
            gvInactivePokemons.DataBind();

            if (inactivesFiltered.Count == 0)
            {
                alertEmptyGV.Visible = false; // Por las dudas que esté activo.
                alertPokemonNotFound.Visible = true;
            }
        }
    }
}