using ApiWebApp.Data;
using ApiWebApp.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Adverts : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public Adverts(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(mapper.Map<List<AdvertDTO>>(context.Adverts));
        }

        [HttpPost]
        public IActionResult Create(CreateDTO advert)
        {
            var newAdvert = mapper.Map<Advert>(advert);

            context.Adverts.Add(newAdvert);

            context.SaveChanges();

            var advertDTO = mapper.Map<AdvertDTO>(newAdvert);

            return CreatedAtAction(nameof(GetOne), new { id = newAdvert.Id }, advertDTO);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            var advert = context.Adverts.FirstOrDefault(a => a.Id == id);
            if (advert == null)
                return NotFound();

            var returnAdvert = mapper.Map<AdvertDTO>(advert);

            return Ok(returnAdvert);
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Edit(int id, [FromBody]JsonPatchDocument<EditDTO> updatedAdvert)
        {
            var ad = context.Adverts.FirstOrDefault(a => a.Id == id);
            
            if(ad == null)
            {
                return NotFound();
            }
            var patchAdvert = mapper.Map<EditDTO>(ad);

            updatedAdvert.ApplyTo(patchAdvert, ModelState);
            if(!TryValidateModel(patchAdvert))
            {
                return ValidationProblem(ModelState);
            }

            mapper.Map(patchAdvert, ad);

            context.SaveChanges();

            return Ok(patchAdvert);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int id, EditDTO updatedAdvert)
        {
            var advert = context.Adverts.FirstOrDefault(e => e.Id == id);
            if (advert == null) return NotFound();

            //advert.Header = updatedAdvert.Header;
            //advert.Body = updatedAdvert.Body;
            //advert.Price = updatedAdvert.Price;
            mapper.Map(updatedAdvert, advert);

            var returnAdvert = mapper.Map<AdvertDTO>(advert);

            context.SaveChanges();
            return Ok(returnAdvert);
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var advert = context.Adverts.First(a => a.Id == id);

            if(advert == null) return NotFound();

            context.Adverts.Remove(advert);
            context.SaveChanges();

            return Ok();
        }

    }
}
