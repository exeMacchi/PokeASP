using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vista.Pages.Auth
{
    public partial class ForgottenPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            try
            {
                int id = UsuarioBBL.GetUserIDByEmail(email);
                if (id != 0)
                {
                    string body = $"<h1>Recuperar cuenta Pokédex Web</h1>" +
                                  $"<p>Recibimos una solicitud para restablecer la contraseña " + 
                                  "asociada a su cuenta.</p>" + 
                                  "<p>Por favor, haga clic en el siguiente enlace para proceder " + 
                                  "con el restablecimiento de su contraseña: </p>" +
                                  $"<a href=\"https://localhost:44310/Pages/Auth/ForgottenPass.aspx?id={id}\">Cambiar contraseña</a>" +
                                   "<p>Si no solicitó este cambio, puede ignorar este mensaje.</p>" +
                                   "<p>Atentamente, equipo de soporte Pokédex Web.</p>";
                    EmailService emailService = new EmailService();
                    emailService.CreateMail("no-reply@pokedex.com", email, "Cambiar contraseña", body);
                    emailService.SendMail();
                    forgottenAlert.Visible = true;
                }
                else 
                {
                    emailErrorAlert.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtFirstPass.Text != txtSecondPass.Text)
            {
                passErrorAlert.Visible = true;
                return;
            }

            string newPass = txtFirstPass.Text;
            int id = int.Parse(Request.QueryString["id"]);
            UsuarioBBL.UpdateUserPass(id, newPass);
            Session["AlertMessage"] = "¡La contraseña se ha actualizado de forma exitosa!";
            Response.Redirect("/Pages/Auth/Login.aspx?alert=success");
        }
    }
}