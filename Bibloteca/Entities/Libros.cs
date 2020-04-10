using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bibloteca.Entities
{
    public class Libros
    {
        [Key]
        public int Idlibro { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public int AutorId { get; set; }

        public Autor Autor { get; set; }


    }
}
