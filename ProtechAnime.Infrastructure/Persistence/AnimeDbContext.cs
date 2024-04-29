using Microsoft.EntityFrameworkCore;
using ProtechAnime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechAnime.Infrastructure.Persistence
{
    public class AnimeDbContext : DbContext
    {
        internal DbSet<Anime> Animes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:teste-protech.database.windows.net,1433;Initial Catalog=banco-protech;Persist Security Info=False;User ID=teste-protech;Password=I99Dn^y}k|:8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

    }

    
}
