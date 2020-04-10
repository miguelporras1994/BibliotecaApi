using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibloteca.Entities
{
    public class Autor
    {
        public int  Id { get; set; }

        public string Identificacion { get; set; }
        public string Nombre { get; set; }

        public List<Libros> libros { get; set; }
    }



}
