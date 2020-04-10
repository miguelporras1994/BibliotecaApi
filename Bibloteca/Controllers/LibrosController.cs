using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bibloteca.Context;
using Bibloteca.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bibloteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LibrosController : ControllerBase
    {




        private readonly AplicationDbContext Db;

        public LibrosController(AplicationDbContext Db)
        {
            this.Db = Db;

        }

        [HttpGet]

        public ActionResult<IEnumerable<Libros>> GetAction()

        {

            return Db.Libros.Include(x => x.Autor).ToList();
        }


        [HttpGet("{id}", Name = "ObtenerLibros")]
        public ActionResult<Autor> GetAction(int id)
        {
            var autor = Db.Autores.FirstOrDefault(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;

        }

        [HttpPost]
        public ActionResult Post([FromBody] Libros libros)
        {


            Db.Libros.Add(libros);
            Db.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibros", new { id = libros.Idlibro }, libros);



        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libros value)
        {

            if (id != value.Idlibro)
            {
                return BadRequest();
            }
            Db.Entry(value).State = EntityState.Modified;
            Db.SaveChanges();
            return Ok();



        }

        [HttpDelete("{id}")]
        public ActionResult<Libros> Delete(int id)
        {
            var libro = Db.Libros.FirstOrDefault(x => x.Idlibro == id);


            if (libro == null)
            {
                return BadRequest();
            }
            Db.Libros.Remove(libro);
            Db.SaveChanges();
            return Ok();



        }



    }
}




