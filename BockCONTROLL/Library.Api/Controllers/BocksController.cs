using Library.Core.Domain.Bocks.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BocksController : ControllerBase
    {
        private static readonly List<Bock> _bocks = new List<Bock>();
        
        [HttpGet]
        public IEnumerable<Bock> Get()
        {
            return _bocks;
        }

        [HttpGet("{id}")]
        public ActionResult<Bock> Get(Guid id)
        {
            var bock = _bocks.FirstOrDefault(b => b.Id == id);
            if (bock == null)
                return NotFound();

            return bock;
        }

        [HttpPost]
        public ActionResult<Bock> Post([FromBody] Bock bock)
        {
            _bocks.Add(bock);
            return CreatedAtAction(nameof(Get), new { id = bock.Id }, bock);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Bock updatedBock)
        {
            var existingBock = _bocks.FirstOrDefault(b => b.Id == id);
            if (existingBock == null)
                return NotFound();

            existingBock.Title = updatedBock.Title;
            existingBock.Description = updatedBock.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var bockToRemove = _bocks.FirstOrDefault(b => b.Id == id);
            if (bockToRemove == null)
                return NotFound();

            _bocks.Remove(bockToRemove);

            return NoContent();
        }
    }
}
