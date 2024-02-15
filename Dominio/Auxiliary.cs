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
        /// <param name="firstCriteria">Primer criterio de búsqueda (Nombre, Número, Tipo o Debilidad)</param>
        /// <param name="secondCriteria">Segundo criterio de búsqueda (Comienza con, Termina con...)</param>
        /// <param name="filterText">Texto de filtro de búsqueda que se inserta en el TextBox</param>
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
    }
}
