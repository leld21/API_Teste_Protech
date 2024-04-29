using ProtechAnime.Domain.Entities;
using ProtechAnime.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProtechAnime.Infrastructure.Persistence;

namespace AnimeProtech.Infrastructure.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AnimeDbContext _context;

        public AnimeRepository(AnimeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Anime>> GetAnimesAsync(string diretor, string nome, string keyword, int pageIndex, int pageSize)
        {
            var query = _context.Animes.AsQueryable();

            if (!string.IsNullOrEmpty(diretor))
                query = query.Where(a => a.Diretor.Contains(diretor));

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(a => a.Nome.Contains(nome));

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(a => a.Resumo.Contains(keyword));

            query = query.Skip(pageIndex * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string diretor, string nome, string keyword)
        {
            var query = _context.Animes.AsQueryable();

            if (!string.IsNullOrEmpty(diretor))
                query = query.Where(a => a.Diretor.Contains(diretor));

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(a => a.Nome.Contains(nome));

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(a => a.Resumo.Contains(keyword));

            return await query.CountAsync();
        }
    }
}