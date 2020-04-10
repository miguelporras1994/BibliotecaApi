using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bibloteca.Entities;

namespace Bibloteca.Context
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> opciones)
            :base(opciones)
        {

        }

        public DbSet<Autor>Autores { get; set; }
        public DbSet<Libros> Libros { get; set; }

    }
}
