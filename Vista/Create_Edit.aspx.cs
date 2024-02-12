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
    public partial class Create_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // Cargar ddlType (tanto para agregar como modificar)
                    Session["Elements"] = ElementoBBL.GetElements();
                    ddlType.DataSource = (List<Elemento>)Session["Elements"];
                    ddlType.DataTextField = "Description";
                    ddlType.DataValueField = "ID";
                    ddlType.DataBind();

                    // Si se está creando un nuevo Pokemon
                    if (Request.QueryString["id"] == null)
                    {
                        btnAdd.Enabled = false;

                        // Se agrega un placeholder al DDL
                        ListItem typePlaceholder = new ListItem("Seleccione un tipo...", "0");
                        typePlaceholder.Attributes["disabled"] = "true";
                        typePlaceholder.Attributes["selected"] = "true";
                        typePlaceholder.Attributes["class"] = "form-option--placeholder";
                        ddlType.Items.Insert(0, typePlaceholder);
                    }
                    // Si se está modificando un Pokemon
                    else
                    {
                        // Se cargan todos los campos según la información del pokemon seleccionado
                        Pokemon pokeMOD = PokemonBBL.GetPokemon(int.Parse(Request.QueryString["id"]));
                        txbxName.Text = pokeMOD.Name;
                        txbxNumber.Text = pokeMOD.Number.ToString();
                        // Objeto en sesión que me ayuda en la verificación del cambio de número
                        Session["NumberPokemon"] = pokeMOD.Number;
                        txbxDescription.Text = pokeMOD.Description;

                        ddlType.SelectedValue = pokeMOD.Type.ID.ToString();
                        ddlType_SelectedIndexChanged(sender, e);
                        // Se selecciona el ddlWeakness según el ID del elemento de debilidad
                        // del pokemon seleccionado
                        ddlWeakness.SelectedIndex = ddlWeakness.Items.IndexOf(ddlWeakness.Items.FindByValue(pokeMOD.Weakness.ID.ToString()));

                        txbxUrl.Text = pokeMOD.UrlImage;
                        txbxUrl_TextChanged(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        /// <summary>
        /// Método que sirve para verificar si se debe o no habilitar el botón de agregar
        /// o modificar dependiendo de si se cargan o no todos los campos requeridos del
        /// formulario.
        /// </summary>
        protected void VerifyInformation(object sender, EventArgs e)
        {
            if (txbxName.Text != "" && txbxNumber.Text != "" && txbxDescription.Text != "" &&
                ddlType.Text != "" && ddlWeakness.Text != "" && txbxUrl.Text != "")
            {
                btnAdd.Enabled = true;
                btnModify.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
                btnModify.Enabled = false;
            }
        }

        /// <summary>
        /// Evento que ocurre cuando se selecciona una opción en el DropDownList de tipo.
        /// </summary>
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cargar ddlWeakness dependiendo del tipo seleccionado en ddlType.
            ddlWeakness.Enabled = true;
            ddlWeakness.DataSource = ((List<Elemento>)Session["Elements"]).FindAll(el => el.ID != int.Parse(ddlType.SelectedValue));
            ddlWeakness.DataTextField = "Description";
            ddlWeakness.DataValueField = "ID";
            ddlWeakness.DataBind();

            // Si se está creando un pokemon
            if (Request.QueryString["id"] == null)
            {
                // Se elimina el Placeholder luego de que se seleccione un tipo.
                ddlType.Items.Remove(ddlType.Items.FindByValue("0"));
                VerifyInformation(sender, e);
            }
        }

        /// <summary>
        /// Evento que ocurre cuando se quiere cargar una imagen a través de una URL
        /// </summary>
        protected void txbxUrl_TextChanged(object sender, EventArgs e)
        {
            if (txbxUrl.Text != "")
            {
                imgPokemon.ImageUrl = txbxUrl.Text;
            }
            else
            {
                imgPokemon.ImageUrl = "https://placehold.co/475.png?text=Pokemon";
            }
            VerifyInformation(sender, e);
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            int number = int.Parse(txbxNumber.Text);

            // Si se modificó el número del Pokemon y este ya existe en la base de datos
            if ((int)Session["NumberPokemon"] != number && PokemonBBL.NumberExistsInDB(number))
            {
                errorText.Text = "El número introducido ya existe en la base de datos. Por favor, introduzca uno nuevo.";
                errorAlert.Visible = true;
                txbxNumber.Focus();
                return;
            }

            string name = txbxName.Text;
            string description = txbxDescription.Text;
            string urlImage = txbxUrl.Text;
            string type = ddlType.SelectedItem.Text;
            int idType = int.Parse(ddlType.SelectedValue);
            string weakness = ddlWeakness.SelectedItem.Text;
            int idWeakness = int.Parse(ddlWeakness.SelectedValue);
            int id = int.Parse(Request.QueryString["id"]);

            Pokemon uPoke = new Pokemon();
            uPoke.ID = id;
            uPoke.Number = number;
            uPoke.Name = name;
            uPoke.Description = description;
            uPoke.UrlImage = urlImage;
            uPoke.Type = new Elemento(idType, type);
            uPoke.Weakness = new Elemento(idWeakness, weakness);

            PokemonBBL.UpdatePokemon(uPoke);
            Session["AlertMessage"] = "El Pokemon fue modificado en la base de datos de forma exitosa.";
            Response.Redirect("Admin.aspx?alert=success", false);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int number = int.Parse(txbxNumber.Text);
            if (!PokemonBBL.NumberExistsInDB(number))
            {
                string name = txbxName.Text;
                string description = txbxDescription.Text;
                string urlImage = txbxUrl.Text;
                string type = ddlType.SelectedItem.Text;
                int idType = int.Parse(ddlType.SelectedValue);
                string weakness = ddlWeakness.SelectedItem.Text;
                int idWeakness = int.Parse(ddlWeakness.SelectedValue);

                Pokemon newPoke = new Pokemon();
                newPoke.Number = number;
                newPoke.Name = name;
                newPoke.Description = description;
                newPoke.UrlImage = urlImage;
                newPoke.Type = new Elemento(idType, type);
                newPoke.Weakness = new Elemento(idWeakness, weakness);

                PokemonBBL.CreatePokemon(newPoke);
                Session["AlertMessage"] = "El Pokemon fue creado en la base de datos de forma exitosa!";
                Response.Redirect("Admin.aspx?alert=success", false);
            }
            else
            {
                errorText.Text = "El número introducido ya existe en la base de datos. Por favor, introduzca uno nuevo.";
                errorAlert.Visible = true;
                txbxNumber.Focus();
            }
        }
    }
}