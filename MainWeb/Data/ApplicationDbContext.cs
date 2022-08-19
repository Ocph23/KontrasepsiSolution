using MainWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MainWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Petugas> Petugas { get; set; }
        public DbSet<Peserta> Peserta { get; set; }
        public DbSet<Pelayanan> Pelayanan{ get; set; }
        public DbSet<AlatKontrasepsi> AlatKontrasepsi { get; set; }


    }
}                                                                           