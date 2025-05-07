using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintBackend.Data;
using PaintBackend.Models;
namespace PaintBackend.Controllers
{
    [ApiController]
    [Route("paint")]
    public class PaintController : ControllerBase
    {
        private readonly PaintDbContext _context;

        public PaintController(PaintDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetPainting")]
        public IActionResult GetPainting()
        {
            return Ok(_context.Paintings.ToList());
        }

        [HttpPost("PostPainting")]
        public void PostPainting(Paint paint)
        {
            _context.Paintings.Add(paint);
            _context.SaveChanges();
        }

        [HttpDelete("DeletePainting")]
        public void DeletePainting(long id)
        {
            _context.Paintings.Where(x => x.PaintId == id).ExecuteDelete();
            _context.SaveChanges();
        }
    }
}