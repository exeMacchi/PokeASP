using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Dominio;

namespace Negocio
{
    public class PokemonBBL
    {
        /// <summary>
        /// Obtener todos los Pokemons activos desde la base de datos.
        /// </summary>
        /// <returns>Lista con todos los Pokemon activos en la base de datos.</returns>
        public static List<Pokemon> GetPokemons()
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            Datos data = new Datos();

            try
            {
                data.SetProcedure("spGetPokemons");
                data.ExecuteRead();
                while (data.Reader.Read())
                {
                    Pokemon pokemon = new Pokemon();
                    pokemon.ID = (int)data.Reader["ID"];
                    pokemon.Number = (int)data.Reader["Number"];
                    pokemon.Name = (string)data.Reader["Name"];
                    pokemon.Description = (string)data.Reader["Description"];
                    pokemon.UrlImage = (string)data.Reader["Image"];

                    pokemon.Type = new Elemento((int)data.Reader["TypeID"], 
                                                (string)data.Reader["Type"]);

                    pokemon.Weakness = new Elemento((int)data.Reader["WeeknessID"], 
                                                    (string)data.Reader["Weekness"]);

                    pokemons.Add(pokemon);
                }

                return pokemons;
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
        /// Obtener todos los <see cref="Pokemon"/> inactivos desde la base de datos.
        /// </summary>
        /// <returns>Lista con todos los <see cref="Pokemon"/> inactivos.</returns>
        public static List<Pokemon> GetInactivePokemons()
        {
            List<Pokemon> inactivePokemons = new List<Pokemon>();
            Datos data = new Datos();

            try
            {
                data.SetProcedure("spGetInactivePokemons");
                data.ExecuteRead();
                while (data.Reader.Read())
                {
                    Pokemon inactivePoke = new Pokemon();
                    inactivePoke.ID = (int)data.Reader["ID"];
                    inactivePoke.Number = (int)data.Reader["Number"];
                    inactivePoke.Name = (string)data.Reader["Name"];
                    inactivePoke.Description = (string)data.Reader["Description"];
                    inactivePoke.UrlImage = (string)data.Reader["Image"];

                    inactivePoke.Type = new Elemento((int)data.Reader["TypeID"], 
                                                (string)data.Reader["Type"]);

                    inactivePoke.Weakness = new Elemento((int)data.Reader["WeeknessID"], 
                                                    (string)data.Reader["Weekness"]);

                    inactivePokemons.Add(inactivePoke);
                }
                return inactivePokemons;
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
        /// Obtener un <see cref="Pokemon"/> desde la base de datos según un ID.
        /// </summary>
        /// <param name="id">ID del Pokemon que se desea obtener en la base de datos.</param>
        /// <returns>Pokemon seleccionado.</returns>
        public static Pokemon GetPokemon(int id)
        {
            Datos data = new Datos();

            try
            {
                data.SetProcedure("spGetPokemon");
                data.SetParam("@ID", id);
                data.ExecuteRead();
                Pokemon pokemon = new Pokemon();
                if (data.Reader.Read())
                {
                    pokemon.ID = (int)data.Reader["ID"];
                    pokemon.Number = (int)data.Reader["Number"];
                    pokemon.Name = (string)data.Reader["Name"];
                    pokemon.Description = (string)data.Reader["Description"];
                    pokemon.UrlImage = (string)data.Reader["Image"];

                    pokemon.Type = new Elemento((int)data.Reader["TypeID"], 
                                                (string)data.Reader["Type"]);

                    pokemon.Weakness = new Elemento((int)data.Reader["WeeknessID"], 
                                                    (string)data.Reader["Weekness"]);
                }
                return pokemon;
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
        /// Verificar si en la base de datos el número de Pokemon pasado como argumento 
        /// ya existe en la base de datos.
        /// </summary>
        /// <param name="number">Número de Pokemon.</param>
        /// <returns>Booleano que determina si existe o no el número en la base de datos.</returns>
        public static bool NumberExistsInDB(int number)
        {
            Datos data = new Datos();
            bool existe = false;
            try
            {
                data.SetProcedure("spValidateNumber");
                data.SetParam("@Number", number);
                SqlParameter output = data.SetOutputParam("@Exists");
                data.ExecuteNonQuery();
                existe = (bool)output.Value;
                return existe;
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
        /// Verificar si en la base de datos hay <see cref="Pokemon"/> inactivos.
        /// Si los hay, este método devuelve <c>true</c>; en caso contrario
        /// devuelve <c>false</c>.
        /// </summary>
        /// <returns>Valor booleano que indica si hay o no <see cref="Pokemon"/> inactivos.</returns>
        public static bool VerifyInactives()
        {
            Datos data = new Datos();
            bool inactives = false;
            try
            {
                data.SetProcedure("spVerifyInactives");
                SqlParameter output = data.SetOutputParam("@Result");
                data.ExecuteNonQuery();
                inactives = (bool)output.Value;
                return inactives;
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
        /// Crear un nuevo <see cref="Pokemon"/> en la base de datos.
        /// </summary>
        /// <param name="newPoke"><see cref="Pokemon"/> con toda la información cargada.</param>
        public static void CreatePokemon(Pokemon newPoke)
        {
            Datos data = new Datos();
            try
            {
                data.SetProcedure("spCreatePokemon");
                data.SetParam("@Number", newPoke.Number);
                data.SetParam("@Name", newPoke.Name);
                data.SetParam("@Description", newPoke.Description);
                data.SetParam("@Image", newPoke.UrlImage);
                data.SetParam("@IDType", newPoke.Type.ID);
                data.SetParam("@IDWeakness", newPoke.Weakness.ID);
                data.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.CloseConnection();
            }
        }

        /// <summary>
        /// Modificar un <see cref="Pokemon"/> seleccionado en la base de datos.
        /// </summary>
        /// <param name="poke"><see cref="Pokemon"/> con la información cargada.</param>
        public static void UpdatePokemon(Pokemon poke)
        {
            Datos data = new Datos();
            try
            {
                data.SetProcedure("spUpdatePokemon");
                data.SetParam("@Number", poke.Number);
                data.SetParam("@Name", poke.Name);
                data.SetParam("@Description", poke.Description);
                data.SetParam("@Image", poke.UrlImage);
                data.SetParam("@IDType", poke.Type.ID);
                data.SetParam("@IDWeakness", poke.Weakness.ID);
                data.SetParam("@ID", poke.ID);
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
        /// Desactivar o activar un <see cref="Pokemon"/> en la base de datos.
        /// Este método llama a un procedimiento almacenado que realiza la lógica
        /// para activar o desactivar automáticamente.
        /// </summary>
        /// <param name="id">
        /// ID del <see cref="Pokemon"/> que se desea activar o desactivar.
        /// </param>
        public static void DisableOrActivePokemon(int id)
        {
            Datos data = new Datos();
            try
            {
                data.SetProcedure("spDisableOrActivePokemon");
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

        public static void DeletePokemon(int id)
        {
            Datos data = new Datos();
            try
            {
                data.SetProcedure("spDeletePokemon");
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
