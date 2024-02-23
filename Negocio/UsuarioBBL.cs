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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        public static int GetUserIDByEmail(string email)
        {
            Datos data = new Datos();
            string query = $"SELECT Id FROM USUARIOS WHERE Email = @Email;";
            try
            {
                data.SetQuery(query);
                data.SetParam("@Email", email);
                data.ExecuteRead();

                if (data.Reader.Read())
                {
                    return (int)data.Reader["ID"];
                }
                else
                {
                    return 0;
                }
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

        public static void UpdateUserPass(int id, string newPass)
        {
            Datos data = new Datos();
            string query = $"UPDATE USUARIOS SET Clave = @Pass WHERE Id = @ID";
            try
            {
                data.SetQuery(query);
                data.SetParam("@Pass", newPass);
                data.SetParam("@ID", id);
                data.ExecuteNonQuery();
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
