using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Elemento
    {
        public int ID {  get; set; }
        public string Description { get; set; }

        public Elemento(int id, string description)
        {
            ID = id;
            Description = description;
        }
    }
}
