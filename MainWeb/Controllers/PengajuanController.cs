using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MainWeb.Data;
using MainWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace MainWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PengajuanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PengajuanController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{pesertaid}/{id}")]
        public async Task<ActionResult<Pelayanan>> GetPelayanan(int pesertaid, int id)
        {
          if (_context.Peserta == null)
          {
              return NotFound();
          }
            var peserta = await _context.Peserta.Include(x => x.Pelayanan)
                .ThenInclude(x=>x.AlatKontrasepsiPilihan).FirstOrDefaultAsync(x=>x.Id==id);

            if (peserta == null)
            {
                return NotFound();
            }

            return peserta.Pelayanan.Where(x=>x.Id==id).FirstOrDefault();
        }

    }
}
