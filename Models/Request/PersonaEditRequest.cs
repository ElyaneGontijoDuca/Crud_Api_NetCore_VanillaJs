using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreVanillaJs.Models.Request
{
    public class PersonaEditRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
    }
}
