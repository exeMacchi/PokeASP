using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Vista.Pages.User
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string subject = txtSubject.Text;
            string message = txtMessage.Text;
            string body = $"<h1>Consulta de <i>{(name != "" ? name : "Anonimo")}</i></h1>" + 
                          $"<p>{message}</p>";

            try
            {
                EmailService emailService = new EmailService();
                emailService.CreateMail(email, "contact@pokeweb.com", subject, body);
                emailService.SendMail();
                contactAlert.Visible = true;
                contactAlert.Focus();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }
    }
}