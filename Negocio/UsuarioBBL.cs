using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        /// Obtener la información de un usuario a partir de un correo electrónico
        /// y una contraseña especificada.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña de la cuenta.</param>
        /// <returns><see cref="Usuario"/> con la información cargada.</returns>
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

        /// <summary>
        /// Devolver desde la base de datos el ID de un usuario según su email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        /// En caso de que haya un usuario con el email especificado, devuelve el
        /// ID de este; en caso contrario, devuelve 0.
        /// </returns>
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

        /// <summary>
        /// Actualizar en la base de datos la contraseña de un usuario por una nueva.
        /// </summary>
        /// <param name="id">ID del usuario que quiere actualizar su contraseña.</param>
        /// <param name="newPass">Nueva contraseña.</param>
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
        
        /// <summary>
        /// Verificar en la base de datos si ya existe una cuenta con el nickname especificado.
        /// Si este ya está ocupado, devuelve <c>true</c>, en caso contrario, devuelve <c>false</c>.
        /// </summary>
        /// <param name="nick">Nickname que se quiere verificar en la base de datos.</param>
        /// <returns>Booleano que representa si ya existe una cuenta con el nickname que el usuario quiere utilizar.</returns>
        public static bool VerifyNickname(string nick)
        {
            Datos data = new Datos();
            bool exists = false;
            try
            {
                data.SetProcedure("spVerifyNickname");
                data.SetParam("@Nick", nick);
                SqlParameter output = data.SetOutputParam("@Exists");
                data.ExecuteNonQuery();
                exists = (bool)output.Value;
                return exists;
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

        /// <summary>
        /// Verificar en la base de datos si ya existe una cuenta con un email especificado.
        /// Si ya existe, el método devuelve <c>true</c>; en caso contrario, devuelve <c>false</c>.
        /// </summary>
        /// <param name="email">Correo electrónico que se quiere verificar en la base de datos.</param>
        /// <returns>Booleano que representa si ya existe una cuenta con el email que el usuario quiere utilizar.</returns>
        public static bool VerifyEmail(string email)
        {
            Datos data = new Datos();
            bool exists = false;
            try
            {
                data.SetProcedure("spVerifyEmail");
                data.SetParam("@Email", email);
                SqlParameter output = data.SetOutputParam("@Exists");
                data.ExecuteNonQuery();
                exists = (bool)output.Value;
                return exists;
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

        /// <summary>
        /// Crear un nuevo <see cref="Usuario"/> en la base de datos.
        /// </summary>
        /// <param name="user"><see cref="Usuario"/> con información esencial.</param>
        public static void CreateUser(Usuario user)
        {
            Datos data = new Datos();

            try
            {
                data.SetProcedure("spCreateUser");
                data.SetParam("@Nick", user.Nick);
                data.SetParam("@Email", user.Email);
                data.SetParam("@Pass", user.Pass);
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
