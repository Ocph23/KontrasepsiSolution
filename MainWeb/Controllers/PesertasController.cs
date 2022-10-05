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
    public class PesertasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public PesertasController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: api/Pesertas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peserta>>> GetPeserta()
        {
            var requestUrl = $"{Request.Scheme}://{Request.Host.Value}/";
            if (_context.Peserta == null)
          {
              return NotFound();
          }
            return await _context.Peserta.ToListAsync();
        }

        // GET: api/Pesertas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Peserta>> GetPeserta(int id)
        {
          if (_context.Peserta == null)
          {
              return NotFound();
          }
            Peserta? peserta = await _context.Peserta.Include(x => x.Pelayanan)
                .ThenInclude(x=>x.AlatKontrasepsiPilihan).FirstOrDefaultAsync(x=>x.Id==id);

            if (peserta == null)
            {
                return NotFound();
            }

            return peserta;
        }

        // PUT: api/Pesertas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeserta(int id, Peserta peserta)
        {
            if (id != peserta.Id)
            {
                return BadRequest();
            }

            _context.Entry(peserta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(peserta);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PesertaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }

            return NoContent();
        }

        // POST: api/Pesertas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Peserta>> PostPeserta(Peserta peserta)
        {
            var trans = await _context.Database.BeginTransactionAsync();
            try
            {
                if (_context.Peserta == null)
                {
                    throw new SystemException("Entity set 'ApplicationDbContext.Peserta'  is null.");
                }
                var password = Helper.CreateRandomPassword();
                var user = new IdentityUser(peserta.Email) { Email = peserta.Email };
                var createResult = await _userManager.CreateAsync(user, password);

                if (!createResult.Succeeded)
                {
                    throw new SystemException(createResult.Errors.FirstOrDefault().Description);
                }

                await _userManager.AddToRoleAsync(user, "Pasien");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var requestUrl = $"{Request.Scheme}://{Request.Host.Value}/";
                var callbackUrl = $"{requestUrl}/Identity/Account/confirmemail?userid={user.Id}&code={code}";
                peserta.UserId = user.Id;
                //save File

                _context.Peserta.Add(peserta);
                await _context.SaveChangesAsync();
                await _emailSender.SendEmailAsync(peserta.Email, "Confirm your email",
                   $"<p>Your UserName : {user.Email} </p>" +
                   $"<p>Your Password : {password} </p>" +
                   $"<p>Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

               await trans.CommitAsync();
                return CreatedAtAction("GetPeserta", new { id = peserta.Id }, peserta);
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return Problem(ex.Message);
            }
        }

        // DELETE: api/Pesertas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeserta(int id)
        {
            if (_context.Peserta == null)
            {
                return NotFound();
            }
            var peserta = await _context.Peserta.FindAsync(id);
            if (peserta == null)
            {
                return NotFound();
            }

            _context.Peserta.Remove(peserta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PesertaExists(int id)
        {
            return (_context.Peserta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
