using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pokemon
    {
        public int ID {  get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlImage {  get; set; }
        public Elemento Type { get; set; }
        public Elemento Weakness { get; set; }

        // public Pokemon Evolution { get; set; }
    }
}
