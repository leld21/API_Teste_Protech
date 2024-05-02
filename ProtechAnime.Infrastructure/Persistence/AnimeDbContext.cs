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
            optionsBuilder.UseSqlServer("{connectin-string}");
        }

    }

    
}
