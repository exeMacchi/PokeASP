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
    }
}
