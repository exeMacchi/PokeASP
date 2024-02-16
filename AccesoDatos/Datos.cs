﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AccesoDatos
{
    public class Datos
    {
        // Atributos
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;
        
        // Propiedades
        public SqlDataReader Reader { get { return reader; } }

        // Constructor
        public Datos()
        {
            connection = new SqlConnection($"server={ConfigurationManager.AppSettings["DB_SERVER"]};" +
                                           $"database={ConfigurationManager.AppSettings["DB_NAME"]};" + 
                                           "integrated security=true;");
            command = connection.CreateCommand();
        }

        // Métodos
        /// <summary>
        /// Establecer la consulta SQL al SqlCommand
        /// </summary>
        /// <param name="query">Consulta SQL.</param>
        public void SetQuery(string query)
        {
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
        }
		
		/// <summary>
        /// Establecer el nombre del procedimiento almacenado dentro de la base
        /// de datos que se quiere ejectuar.
        /// </summary>
        /// <param name="storedProcedure">Nombre del procedimiento almacenado</param>
		public void SetProcedure(string storedProcedure)
		{
			command.CommandType = System.Data.CommandType.StoredProcedure;
			command.CommandText = storedProcedure;
		}


        /// <summary>
        /// Configurar los parámetros de comando.
        /// </summary>
        /// <param name="param">@Parámetro.</param>
        /// <param name="value">Valor del parámetro.</param>
        public void SetParam(string param, object value)
        {
            command.Parameters.AddWithValue(param, value);
        }

        /// <summary>
        /// Configurar el parámetro de salida booleano de una consulta SQL.
        /// </summary>
        /// <param name="param"></param>
        /// <returns>SqlParameter</returns>
        public SqlParameter SetOutputParam(string param)
        {
            SqlParameter output = new SqlParameter(param, SqlDbType.Bit);
            output.Direction = ParameterDirection.Output;
            command.Parameters.Add(output);
            return output;
        }


        /// <summary>
        /// Ejecutar la consulta SQL para una operación SELECT.
        /// </summary>
        public void ExecuteRead()
        {
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Ejecutar la consulta SQL para una operación INSERT, UPDATE o DELETE. 
        /// </summary>
        public void ExecuteNonQuery()
        {
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Cerrar la conexión a la base de datos y posible DataReader.
        /// </summary>
        public void CloseConnection()
        {
            if (reader != null)
            {
                reader.Close();
            }
            connection.Close();
        }
    }
}
