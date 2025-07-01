using Business;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Threading.Tasks;

namespace NotDefteriAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotDefteriController : ControllerBase
    {
        private readonly INotDefteriBusiness _notDefteriBusiness;
        public NotDefteriController(INotDefteriBusiness notDefteriBusiness)
        {
            _notDefteriBusiness = notDefteriBusiness;
        }
        //Not ekleme metodu
        [HttpPost]
        public async Task<ActionResult> NotEkle([FromBody] NotModel notModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _notDefteriBusiness.NotEkle(notModel);
            if (result.Success)
            {
                return Ok(notModel);
            }
            return StatusCode(500, result.mesaj);
        }
        //notları listele metodu
        [HttpGet]
        public async Task<ActionResult<List<NotModel>>> NotListele()
        {
            var notlar = await _notDefteriBusiness.NotlariListele();
            if (notlar == null)
            {
                return NotFound();
            }
            return Ok(notlar);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> NotGuncelle(int id,NotModel notModel)
        {
            var (Success, message) = await _notDefteriBusiness.NotGuncelle(id, notModel);
            if (!Success)
            {
                return BadRequest(new {mesaj= message });
            }
            return Ok(notModel);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Sil(int id) { 
            await _notDefteriBusiness.Sil(id);
            return Ok(new {mesaj="Not başarıyla silindi!"});
        }
    }
}
