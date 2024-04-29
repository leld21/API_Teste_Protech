using ProtechAnime.Application.Services;
using ProtechAnime.Domain.Entities;
using ProtechAnime.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechAnime.Application.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeService(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<List<Anime>> GetAnimesAsync(string director, string name, string keyword, int pageIndex, int pageSize)
        {
            return await _animeRepository.GetAnimesAsync(director, name, keyword, pageIndex, pageSize);
        }

        public async Task<Anime> GetAnimeAsync(int id)
        {
            return await _animeRepository.GetAnimeAsync(id);
        }

        public async Task<int> GetTotalCountAsync(string director, string name, string keyword)
        {
            return await _animeRepository.GetTotalCountAsync(director, name, keyword);
        }
    }
}
