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
                // Middleware admin
                if (!((Usuario)Session["UserSession"]).Admin)
                {
                    Response.Redirect("Login.aspx", false);
                }

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
        /// Filtrar la lista de pokemons activos en sesión según un criterio (dependiendo
        /// qué tipo de filtro esté activado) y actualizar el <see cref="gvPokemons"/> 
        /// con los resultados obtenidos.
        /// </summary>
        protected void btnFind_Click(object sender, EventArgs e)
        {
            string filterText = txtFilter.Text;

            // Filtro avanzado
            if (advancedPanel.Visible)
            {
                string firstCriteria = ddlFirstCriteria.SelectedValue;
                string secondCriteria = ddlSecondCriteria.SelectedValue;
                string condition = Auxiliary.CreateCondition(firstCriteria, secondCriteria, filterText);

                List<Pokemon> pokeFiltered = PokemonBBL.SearchPokemons(condition);
                gvPokemons.DataSource = pokeFiltered;
                gvPokemons.DataBind();

                if (pokeFiltered.Count == 0)
                {
                    alertEmptyGV.Visible = false; // Por las dudas
                    alertPokemonNotFound.Visible = true;
                }
            }
            // Filtro básico
            else
            {
                List<Pokemon> pokeFiltered = ((List<Pokemon>)Session["Pokemons"]).FindAll(p => p.Name.ToUpper().Contains(filterText.ToUpper()));

                gvPokemons.DataSource = pokeFiltered;
                gvPokemons.DataBind();

                if (pokeFiltered.Count == 0)
                {
                    alertEmptyGV.Visible = false; // Por las dudas
                    alertPokemonNotFound.Visible = true;
                }
            }
        }

        /// <summary>
        /// Activar o desactivar el filtro avanzado de búsqueda.
        /// </summary>
        protected void btnAdvanced_Click(object sender, EventArgs e)
        {
            if (advancedPanel.Visible)
            {
                txtFilter.Attributes["placeholder"] = "Buscar por nombre...";
                advancedPanel.Visible = false;
            }
            else
            {
                // Primer criterio
                ddlFirstCriteria.Items.Clear();
                ddlFirstCriteria.Items.Add(new ListItem("Nombre", "Name"));
                ddlFirstCriteria.Items.Add(new ListItem("Número", "Number"));
                ddlFirstCriteria.Items.Add(new ListItem("Tipo", "Type"));
                ddlFirstCriteria.Items.Add(new ListItem("Debilidad", "Weakness"));

                // Segundo criterio (las opciones de nombre son por defecto)
                ddlSecondCriteria.Items.Clear();
                ddlSecondCriteria.Items.Add(new ListItem("Comienza con...", "Starts"));
                ddlSecondCriteria.Items.Add(new ListItem("Contiene...", "Contains"));
                ddlSecondCriteria.Items.Add(new ListItem("Termina con...", "Ends"));


                txtFilter.Attributes["placeholder"] = "Buscar por el criterio...";
                advancedPanel.Visible = true;
            }

            // Reinicio
            txtFilter.Text = string.Empty;
            gvPokemons.DataSource = (List<Pokemon>)Session["Pokemons"];
            gvPokemons.DataBind();
            alertPokemonNotFound.Visible = false;
        }

        /// <summary>
        /// Cargar el <see cref="ddlSecondCriteria"/> según la selección del primer
        /// criterio en <see cref="ddlFirstCriteria"/>.
        /// </summary>
        protected void ddlFirstCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            if (ddlFirstCriteria.SelectedValue == "Number")
            {
                ddlSecondCriteria.Items.Clear();
                ddlSecondCriteria.Items.Add(new ListItem("Mayor a...", "Greater"));
                ddlSecondCriteria.Items.Add(new ListItem("Igual a...", "Equal"));
                ddlSecondCriteria.Items.Add(new ListItem("Menor a...", "Smaller"));
            }
            else
            {
                ddlSecondCriteria.Items.Clear();
                ddlSecondCriteria.Items.Add(new ListItem("Comienza con...", "Starts"));
                ddlSecondCriteria.Items.Add(new ListItem("Contiene...", "Contains"));
                ddlSecondCriteria.Items.Add(new ListItem("Termina con...", "Ends"));
            }
        }
    }
}