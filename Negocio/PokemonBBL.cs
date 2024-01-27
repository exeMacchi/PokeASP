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
    }
}
