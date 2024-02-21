using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int ID { get; set; }

        public string Nick { get; set; }

        public string Email { get; set; }

        public string Pass { get; set; }

        public string ProfileImage { get; set; }

        public DateTime Birth { get; set; }

        public bool Admin { get; set; }
    }
}
