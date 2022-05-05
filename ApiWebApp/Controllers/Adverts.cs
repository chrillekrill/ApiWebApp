using ApiWebApp.Data;
using ApiWebApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Adverts : Controller
    {
        private readonly ApplicationDbContext _context;

        public Adverts(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(
                _context.Adverts.Select(c => new AdvertDTO
                {
                    Id = c.Id,
                    Header = c.Header,
                    Body = c.Body,
                    Price = c.Price
                }).ToList());
        }

        [HttpPost]
        public IActionResult Create(CreateDTO advert)
        {
            var newAdvert = new Advert
            {
                Header = advert.Header,
                Body = advert.Body,
                Price = advert.Price
            };
            _context.Adverts.Add(newAdvert);
            _context.SaveChanges();

            var advertDTO = new AdvertDTO
            {
                Id = newAdvert.Id,
                Header = newAdvert.Header,
                Body = newAdvert.Body,
                Price = newAdvert.Price
            };

            return CreatedAtAction(nameof(GetOne), new { id = newAdvert.Id }, advertDTO);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            var advert = _context.Adverts.FirstOrDefault(a => a.Id == id);
            if (advert == null)
                return NotFound();

            var returnAdvert = new AdvertDTO
            {
                Id = advert.Id,
                Header = advert.Header,
                Body = advert.Body,
                Price = advert.Price
            };

            return Ok(returnAdvert);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int id, UpdateDTO updatedAdvert)
        {
            var advert = _context.Adverts.FirstOrDefault(e => e.Id == id);
            if (advert == null) return NotFound();

            advert.Header = updatedAdvert.Header;
            advert.Body = updatedAdvert.Body;
            advert.Price = updatedAdvert.Price;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var advert = _context.Adverts.First(a => a.Id == id);

            if(advert == null) return NotFound();

            _context.Adverts.Remove(advert);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
