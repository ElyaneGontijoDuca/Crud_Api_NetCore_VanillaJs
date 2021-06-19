using CoreVanillaJs.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreVanillaJs.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
   // [EnableCors("permitir")] //Isso habilita que o controlador seja acessado por outras solicitudes atravez de outras origens 
    //no nosso caso atravez de javascript que nao esta nesse projecto
    public class PersonaController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            using (CrudVanillaJsContext db = new Models.CrudVanillaJsContext())
            {
                var lst = (from d in db.Persona
                           select d).ToList();
                return Ok(lst);
            }
        }
       
        [HttpPost]
        public ActionResult Post([FromBody]Models.Request.PersonaRequest model)
        {
            using (CrudVanillaJsContext db = new Models.CrudVanillaJsContext())
            {
                Models.Persona oPersona = new Models.Persona();
                oPersona.Nombre = model.Nombre;
                oPersona.Edad = model.Edad;
                //Para agregar a base de dados

                db.Persona.Add(oPersona);
                db.SaveChanges();
            }
            return Ok();

        }

        [HttpPut]
        public ActionResult Edit([FromBody]Models.Request.PersonaEditRequest model)
        {
            using (CrudVanillaJsContext db = new Models.CrudVanillaJsContext())
            {
                Models.Persona oPersona = db.Persona.Find(model.Id);
                oPersona.Nombre = model.Nombre;
                oPersona.Edad = model.Edad;

                //Propriedade que define a edicao no banco
                db.Entry(oPersona).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            return Ok();
        }


        [HttpDelete]
       public ActionResult Delete([FromBody] Models.Request.PersonaEditRequest model) 
        {
            using(CrudVanillaJsContext db = new Models.CrudVanillaJsContext())
            {
                Models.Persona oPersona = db.Persona.Find(model.Id);
                db.Persona.Remove(oPersona);
                db.SaveChanges();
            }
            return Ok();
        }
    }
}
