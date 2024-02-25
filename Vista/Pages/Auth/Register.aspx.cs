using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vista
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // No puedo utilizar btnSubmit.Enabled = false porque la habilitación del
                // botón también se maneja desde el cliente; por ende, es más fácil
                // gestionando el atributo en sí.
                btnSubmit.Attributes["disabled"] = "true";
            }
        }

        protected void txtNick_TextChanged(object sender, EventArgs e)
        {
            if (txtNick.Text != "")
            {
                // Si ya existe una cuenta con el nickname especificado...
                if (UsuarioBBL.VerifyNickname(txtNick.Text))
                {
                    txtNick.CssClass = "form-control border border-danger border-3";
                    lbNickAlert.Visible = true;
                }
                // Si no existe...
                else
                {
                    txtNick.CssClass = "form-control border border-success border-3";
                    lbNickAlert.Visible = false;
                    VerifyInformation(sender, e);
                }
            }
            else
            {
                txtNick.CssClass = "form-control border border-danger border-3";
                lbNickAlert.Visible = false;
                btnSubmit.Attributes["disabled"] = "true";
            }
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text != "")
            {
                // Si ya existe una cuenta con el email especificado...
                if (UsuarioBBL.VerifyEmail(txtEmail.Text))
                {
                    txtEmail.CssClass = "form-control border border-danger border-3";
                    lbEmailAlert.Visible = true;
                }
                // Si no existe...
                else
                {
                    txtEmail.CssClass = "form-control border border-success border-3";
                    lbEmailAlert.Visible = false;
                    VerifyInformation(sender, e);
                }
            }
            else
            {
                txtEmail.CssClass = "form-control border border-danger border-3";
                lbEmailAlert.Visible = false;
                btnSubmit.Attributes["disabled"] = "true";
            }
        }

        protected void VerifyInformation(object sender, EventArgs e)
        {
            if (txtNick.Text != "" && txtEmail.Text != "" && txtFirstPass.Text != "" && txtSecondPass.Text != "")
            {
                btnSubmit.Attributes.Remove("disabled");
            }
            else
            {
                btnSubmit.Attributes["disabled"] = "true";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Verificación adicional
            if (txtNick.Text == "" || txtEmail.Text == "" || txtFirstPass.Text == "")
            {
                registerErrorAlert.Visible = true;
                return;
            }

            Usuario newUser = new Usuario
            {
                Nick = txtNick.Text,
                Email = txtEmail.Text,
                Pass = txtFirstPass.Text
            };

            UsuarioBBL.CreateUser(newUser);

            EmailService emailService = new EmailService();
            string body = Auxiliary.CreateRegisterHTMLBody(newUser.Nick);
            emailService.CreateMail("no-replay@pokedex.com", txtEmail.Text, "Bienvenido, entrenador Pokémon", body);
            emailService.SendMail();
            Session["AlertMessage"] = "¡Usuario registrado de forma exitosa!";
            Response.Redirect("/Pages/Auth/Login.aspx?alert=success");
        }
    }
}