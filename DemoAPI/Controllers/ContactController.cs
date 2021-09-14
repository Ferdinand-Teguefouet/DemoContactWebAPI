using BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Tools;
using BLL.Models;
using DemoAPI.Models;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        //private List<string> maListe = new List<string>();
        private IContactBusiness<BusinessContact> _contactService;

        public ContactController(IContactBusiness<BusinessContact> contactBusiness)
        {
            _contactService = contactBusiness;

            //maListe.Add("test");
            //maListe.Add("coucou");
            //maListe.Add("pizza");
            //maListe.Add("couscous");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_contactService.GetAll().Select(c => c.ToApi()));
            //return Ok(maListe);
                     
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_contactService.GetById(id).ToApi());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _contactService.Delete(id);
                return Ok("Tout s'est bien passé");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

        [HttpPost]
        public IActionResult Insert(ContactForm c)
        {
            if (!ModelState.IsValid)
                return BadRequest("Ca a foiré");
            _contactService.Insert(c.ToBLL());
            return Ok("Enregistrement effectué");
        }

        [HttpPut]
        public IActionResult Update(ContactForm c)
        {
            if (!ModelState.IsValid)
                return BadRequest("Ca a foiré");
            if(c.Id == 0)
            {
                return BadRequest("L'id doit être renseigné");
            }
            _contactService.Update(c.ToBLL());
            return Ok("Modification effectuée");
        }
    }
}
