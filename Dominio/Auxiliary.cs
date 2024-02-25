using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public static class Auxiliary
    {
        /// <summary>
        /// Crear la condición de búsqueda avanzada que se pondrá en la sección WHERE de la
        /// query que se realizará a la base de datos.
        /// </summary>
        /// <param name="firstCriteria">Primer criterio de búsqueda (Nombre, Número, Tipo o Debilidad).</param>
        /// <param name="secondCriteria">Segundo criterio de búsqueda (Comienza con, Termina con...).</param>
        /// <param name="filterText">Texto de filtro de búsqueda que se inserta en el TextBox.</param>
        /// <returns>Condición completa para insertar como parámetro después de la cláusula WHERE.</returns>
        public static string CreateCondition(string firstCriteria, string secondCriteria, string filterText)
        {
            string condition = string.Empty;
            string criteria = CreateCriteria(firstCriteria, secondCriteria, filterText);

            if (firstCriteria == "Name")
                condition = $"P.Nombre {criteria} ";
            else if (firstCriteria == "Number")
                condition = $"P.Numero {criteria} ";
            else if (firstCriteria == "Type")
                condition = $"T.Descripcion {criteria} ";
            else if (firstCriteria == "Weakness")
                condition = $"D.Descripcion {criteria} ";

            return condition;
        }

        /// <summary>
        /// Crear el criterio de búsqueda utilizando el operador adecuado según el contexto
        /// (LIKE para búsqueda por texto, operadores de relacionales para los números).
        /// </summary>
        /// <param name="firstCriteria">Primer criterio de búsqueda (Nombre, Número, Tipo o Debilidad).</param>
        /// <param name="secondCriteria">Segundo criterio de búsqueda (Comienza con, Termina con...).</param>
        /// <param name="text">Texto de filtro de búsqueda que se inserta en el TextBox.</param>
        /// <returns>Criterio de búsqueda completo que se agrega luego del campo a buscar.</returns>
        private static string CreateCriteria(string firstCriteria, string secondCriteria, string text)
        {
            string criteria = string.Empty;
            if (firstCriteria == "Name" || firstCriteria == "Type" || firstCriteria == "Weakness")
            {
                if (secondCriteria == "Starts")
                    criteria = $"LIKE '{text}%' ";
                else if (secondCriteria == "Contains")
                    criteria = $"LIKE '%{text}%' ";
                else
                    criteria = $"LIKE '%{text}' ";
            }
            else
            {
                if (int.TryParse(text, out int number))
                {
                    if (secondCriteria == "Greater")
                        criteria = $"> {number} ";
                    else if (secondCriteria == "Equal")
                        criteria = $"= {number} ";
                    else
                        criteria = $"< {number} ";
                }
                else
                {
                    criteria = $"> 0 ";
                }
            }

            return criteria;
        }

        /// <summary>
        /// Crear el HTML Body del correo electrónico de bienvenida post-registro.
        /// </summary>
        /// <param name="nick"><see cref="Usuario.Nick"/> del nuevo usuario.</param>
        /// <returns>HTML Body formateado.</returns>
        public static string CreateRegisterHTMLBody(string nick)
        {
            string body =
                "<body style=\"font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;\">" +
                "<div style=\"max-width: 600px; margin: 20px auto; background-color: #fff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\">" +
                "<h1 style=\"color: #333;\">Bienvenido, entrenado Pokémon</h1>" +
                "<p style=\"color: #666; line-height: 1.6;\">¡Hola, " + nick + "!</p>" +
                "<p style=\"color: #666; line-height: 1.6;\">¡Gracias por registrarte en nuestra Pokédex Web! Estamos emocionados de tenerte como parte de nuestra comunidad.</p>" +
                "<p style=\"color: #666; line-height: 1.6;\">Con nuestra aplicación, podrás explorar información detallada sobre tus Pokémon favoritos, encontrar nuevas especies y descubrir secretos del mundo Pokémon.</p>" +
                "<p style=\"color: #666; line-height: 1.6;\">No dudes en comenzar tu aventura ahora mismo. ¡Haz clic en el botón de abajo para ingresar!</p>" +
                "<a href=\"https://localhost:44310/Default.aspx\" style=\"display: inline-block; padding: 10px 20px; background-color: #007bff; color: #fff; text-decoration: none; border-radius: 5px; transition: background-color 0.3s ease;\">¡Explora ahora!</a>" +
                "<p style=\"color: #666; line-height: 1.6;\">¡Que tu viaje para convertirte en el mejor entrenador Pokémon esté lleno de diversión y emoción!</p>" +
                "<p style=\"color: #666; line-height: 1.6;\">Equipo de la Pokédex Web</p>" +
                "</div>" +
                "</body>";

            return body;
        }
    }
}
