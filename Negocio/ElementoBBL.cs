using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Dominio;

namespace Negocio
{
    public class ElementoBBL
    {
        public List<Elemento> GetElements()
        {
            List<Elemento> elements = new List<Elemento>();
            Datos data = new Datos();

            try
            {
                data.SetQuery("SELECT Id AS ID, " +
                              "       Descripcion AS Description " +
                              "FROM ELEMENTOS");
                data.ExecuteRead();
                while (data.Reader.Read())
                {
                    Elemento element = new Elemento
                    (
                        (int)data.Reader["ID"],
                        (string)data.Reader["Description"]
                    );
                    elements.Add(element);
                }

                return elements;
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
