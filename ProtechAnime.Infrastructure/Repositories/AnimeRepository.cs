using ProtechAnime.Domain.Entities;
using ProtechAnime.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProtechAnime.Infrastructure.Persistence;
using System.Xml.Linq;

namespace AnimeProtech.Infrastructure.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AnimeDbContext _context;

        public AnimeRepository(AnimeDbContext context)
        {
            _context = context;
        }

        public async Task<Anime> GetAnimeAsync(int id)
        {
            var anime =  await _context.Animes.FindAsync(id);

            if (anime != null && anime.Ativo)
            {
                return anime;
            }

            return null;
        }

        public async Task<List<Anime>> GetAnimesFilteredAsync(string filtro, int modo)
        {
            var animes = new List<Anime>(); 
            if(modo == 1)
            {
                animes.Add(await _context.Animes.FirstOrDefaultAsync(a => a.Nome == filtro && a.Ativo == true));
            }
            else if (modo == 2)
            {
                animes = await _context.Animes
                    .Where(a => a.Diretor.Contains(filtro) && a.Ativo == true)
                    .ToListAsync();
            }
            else
            {
                animes = await _context.Animes
                    .Where(a => a.Resumo.Contains(filtro) && a.Ativo == true)
                    .ToListAsync();
            }

            return animes;

        }

        public async Task<int?> CreateAnimeAsync(Anime anime)
        {
            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
            return anime.Id;
        }
        public async Task<Anime> UpdateAnimeAsync2(Anime anime)
        {
            // Entity Framework Procura as atualizacoes em "anime" e atualiza no banco.
            _context.Entry(anime).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return anime;
        }

        public async Task<Anime> UpdateAnimeAsync(Anime anime)
        {
            var animeParaAtualizar = await _context.Animes.FindAsync(anime.Id);

            if(animeParaAtualizar == null)
            {
                animeParaAtualizar = await _context.Animes.FirstOrDefaultAsync(a => a.Nome == anime.Nome);
            }

            animeParaAtualizar.Nome = anime.Nome != null ? anime.Nome : animeParaAtualizar.Nome;
            animeParaAtualizar.Diretor = anime.Diretor != null ? anime.Diretor : animeParaAtualizar.Diretor;
            animeParaAtualizar.Resumo = anime.Resumo != null ? anime.Resumo : animeParaAtualizar.Resumo;
            animeParaAtualizar.Ativo = true;

            _context.Entry(animeParaAtualizar).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return animeParaAtualizar;
        }

        public async Task<Anime> LogicalDeleteAsync(Anime anime)
        {
            var animeParaAtualizar = await _context.Animes.FindAsync(anime.Id);

            if (animeParaAtualizar == null)
            {
                animeParaAtualizar = await _context.Animes.FirstOrDefaultAsync(a => a.Nome == anime.Nome);
            }

            animeParaAtualizar.Ativo = false;

            _context.Entry(animeParaAtualizar).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return animeParaAtualizar;
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