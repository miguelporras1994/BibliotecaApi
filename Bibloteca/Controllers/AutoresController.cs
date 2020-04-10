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
    public class AutoresController : ControllerBase

    {

        private readonly AplicationDbContext Db;

        public AutoresController(AplicationDbContext Db)
        {
            this.Db = Db;

        }

        [HttpGet]

        public ActionResult<IEnumerable<Autor>> GetAction()

        {

            return Db.Autores.ToList();

        }

        [HttpGet ("primero")]

        public ActionResult<Autor> GetPrimerAutor()

        {

            return Db.Autores.FirstOrDefault();
        }



        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<Autor>> GetAction(int id)
        {
            var autor = await Db.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;

        }

        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {


            Db.Autores.Add(autor);
            Db.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autor);



        }

        [HttpPut("{id}")]
        public ActionResult Put( int id , [FromBody] Autor value)
        {

            if(id != value.Id)
            {
                return BadRequest();
            }
            Db.Entry(value).State = EntityState.Modified;
            Db.SaveChanges();
            return Ok();



        }

        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = Db.Autores.FirstOrDefault(x => x.Id == id);


            if ( autor == null)
            {
                return BadRequest();
            }
            Db.Autores.Remove(autor);
            Db.SaveChanges();
            return Ok();



        }
    }






    }
