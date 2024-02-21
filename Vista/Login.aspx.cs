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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string pass = txtPass.Text;

            try
            {
                Usuario user = UsuarioBBL.GetUser(email, pass);
                if (user.ID != 0)
                {
                    Session["UserSession"] = user;
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    panelErrorAlert.Visible = true;
                    txtEmail.Focus();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }
    }
}