using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MainWeb.Data;
using MainWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MainWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PengajuanController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;

        private readonly UserManager<IdentityUser> _userManager;

        public PengajuanController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _dbcontext = context;
            _userManager = userManager;
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Pelayanan>> GetPelayanan(int id)
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                return Unauthorized();
            }

            if (_dbcontext.Peserta == null)
            {
                return NotFound();
            }
           
            var peserta = await _dbcontext.Peserta.Include(x => x.Pelayanan)
                .ThenInclude(x=>x.AlatKontrasepsiPilihan).FirstOrDefaultAsync(x=>x.UserId==user.Id);

            if (peserta == null)
            {
                return NotFound();
            }

            return peserta.Pelayanan.Where(x=>x.Id==id).FirstOrDefault();
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost()]
        public async Task<ActionResult<Pelayanan>> PostPelayanan(Pelayanan layanan)
        {
            try
            {
                var userName = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return Unauthorized();
                }

                if (_dbcontext.Peserta == null)
                {
                    return NotFound();
                }

                Peserta? peserta = await _dbcontext.Peserta.Where(x => x.UserId == user.Id)
                    .Include(x => x.Pelayanan)
                    .ThenInclude(x => x.AlatKontrasepsiPilihan)
                    .FirstOrDefaultAsync(x => x.UserId == user.Id);

                if (peserta == null)
                {
                    return NotFound();
                }

                peserta.Pelayanan.Add(layanan);
                await _dbcontext.SaveChangesAsync();
                return layanan;
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message);
            }

        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut()]
        public async Task<ActionResult<Pelayanan>> PutPelayanan(int id, Pelayanan layanan)
        {
            try
            {
                var userName = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return Unauthorized();
                }

                if (_dbcontext.Peserta == null)
                {
                    return NotFound();
                }

                var oldlayanan = _dbcontext.Pelayanan.Include(x=>x.AlatKontrasepsiPilihan).FirstOrDefault(x=>x.Id == id);

                if(oldlayanan== null)
                {
                    return NotFound();
                }



                oldlayanan.TerakhirHaid= layanan.TerakhirHaid;
                oldlayanan.Hamil = layanan.Hamil;
                oldlayanan.JumlahKeguguran = layanan.JumlahKeguguran;
                oldlayanan.JumlahKehamilan= layanan.JumlahKehamilan;
                oldlayanan.JumlahPersalinan = layanan.JumlahPersalinan;
                
                oldlayanan.Menyusui = layanan.Menyusui;
                
                oldlayanan.SakitKuning= layanan.SakitKuning;
                oldlayanan.PendarahanPervaginam= layanan.PendarahanPervaginam;
                oldlayanan.KeputihanYangLama = layanan.KeputihanYangLama;
                oldlayanan.Tumor= layanan.Tumor;
                

                oldlayanan.Keadaan= layanan.Keadaan;
                oldlayanan.BeratBadan = layanan.BeratBadan;
                oldlayanan.TekananDarah= layanan.TekananDarah;
                oldlayanan.PosisiRahim= layanan.PosisiRahim;

                oldlayanan.StatusKB = layanan.StatusKB;
                
                
                oldlayanan.Radang= layanan.Radang;
                oldlayanan.KeganasanGinekologi= layanan.KeganasanGinekologi;
                oldlayanan.Diabetes= layanan.Diabetes;
                oldlayanan.PemekuanDarah= layanan.PemekuanDarah;
                oldlayanan.RadangOrchitis= layanan.RadangOrchitis;
                oldlayanan.KeganasanGinekologi2 = layanan.KeganasanGinekologi2;
                oldlayanan.TanggalDicabut= layanan.TanggalDicabut;
                oldlayanan.TanggalDilayani = layanan.TanggalDilayani;
                oldlayanan.TanggalKembali= layanan.TanggalKembali;

                if (oldlayanan.CaraKBTerakhir!= null && layanan.CaraKBTerakhir!= null && 
                    oldlayanan.CaraKBTerakhir.Id == layanan.CaraKBTerakhir.Id)
                {
                    _dbcontext.Entry(oldlayanan.CaraKBTerakhir).State = EntityState.Unchanged;
                }
                else
                {
                    oldlayanan.CaraKBTerakhir= layanan.CaraKBTerakhir;
                }


                if (oldlayanan.AlatKontrasepsiPilihan!=null && layanan.AlatKontrasepsiPilihan != null && oldlayanan.AlatKontrasepsiPilihan.Id == layanan.AlatKontrasepsiPilihan.Id)
                {
                    _dbcontext.Entry(oldlayanan.AlatKontrasepsiPilihan).State = EntityState.Unchanged;
                }
                else
                {
                    oldlayanan.AlatKontrasepsiPilihan= layanan.AlatKontrasepsiPilihan;
                }

                await _dbcontext.SaveChangesAsync();
                return layanan;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
