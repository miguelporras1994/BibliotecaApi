using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bibloteca.Context;
using Bibloteca.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Bibloteca.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Bibloteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase

    {

        private readonly AplicationDbContext Db;
        private readonly IMapper mapper;

        public AutoresController(AplicationDbContext Db , IMapper mapper)
        {
            this.Db = Db;
            this.mapper = mapper;

        }

        [HttpGet]

        public async Task <ActionResult<IEnumerable<AutorDTO>>> GetAction()

        {
            var autores = await Db.Autores.ToListAsync();
            var autoresDTO = mapper.Map<List<AutorDTO>>(autores);

            return autoresDTO;

        }

        [HttpGet ("primero")]

        public ActionResult<Autor> GetPrimerAutor()

        {

            return Db.Autores.FirstOrDefault();
        }



        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> GetAction(int id)
        {
            var autor = await Db.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            var autordto = mapper.Map<AutorDTO>(autor);

            return  autordto;

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreateDTO   autocreateDTO)

        {
            var autor = mapper.Map<Autor>(autocreateDTO);

          
            Db.Autores.Add(autor);
             await Db.SaveChangesAsync();
            var autorDTO = mapper.Map<AutorDTO>(autor);
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autorDTO);



        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id , [FromBody]  AutorCreateDTO UpdateAutor)
        {

            var autor = mapper.Map<Autor>(UpdateAutor);
            autor.Id = id;

            
            Db.Entry(autor).State = EntityState.Modified;
            await Db.SaveChangesAsync();
            return Ok();



        }
        
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<AutorCreateDTO> jsonPatchDocument)
        { 
        
            if (jsonPatchDocument == null)
            {
                return BadRequest();

            }

            var autorbase = await Db.Autores.FirstOrDefaultAsync(x => x.Id == id);


            if(autorbase == null)
            {
                return NotFound();
            }

            var autorDto = mapper.Map<AutorCreateDTO>(autorbase);

            jsonPatchDocument.ApplyTo(autorDto, ModelState);

            var Isvalid = TryValidateModel(autorbase);
            if (!Isvalid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(autorDto, autorbase);

            await Db.SaveChangesAsync();
            return NoContent();
      
        }



            
        

        [HttpDelete("{id}")]
        public  async Task<ActionResult<Autor>> Delete(int id)
        {
            var autorid = await Db.Autores.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);


            if ( autorid ==  default(int) )
            {
                return BadRequest();
            }
            Db.Autores.Remove( new Autor {Id = autorid });
             await  Db.SaveChangesAsync();
            return NoContent();



        }
    }






    }
