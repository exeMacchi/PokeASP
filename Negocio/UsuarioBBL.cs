using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Dominio;

namespace Negocio
{
    public class UsuarioBBL
    {
        public static Usuario GetUser(string email, string password)
        {
            Datos data = new Datos();
            try
            {
                data.SetProcedure("spGetUser");
                data.SetParam("@Email", email);
                data.SetParam("@Pass", password);
                data.ExecuteRead();
                Usuario user = new Usuario();

                if (data.Reader.Read())
                {
                    user.ID = (int)data.Reader["ID"];
                    user.Nick = (string)data.Reader["Nick"];
                    user.Pass = (string)data.Reader["Pass"];
                    if (!(data.Reader["Birth"] is DBNull))
                        user.Birth = DateTime.Parse(data.Reader["birth"].ToString());
                    if (!(data.Reader["ProfileImage"] is DBNull))
                        user.ProfileImage = (string)data.Reader["ProfileImage"];
                    user.Admin = (bool)data.Reader["Role"];
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.CloseConnection();
            }
        }
    }
}
