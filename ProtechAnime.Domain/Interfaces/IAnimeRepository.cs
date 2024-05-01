using ProtechAnime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechAnime.Domain.Interfaces
{
    public interface IAnimeRepository
    {
        Task<List<Anime>> GetAnimesAsync(string diretor, string nome, string keyword, int pageIndex, int pageSize);
        Task<Anime> GetAnimeAsync(int id);
        Task<List<Anime>> GetAnimesFilteredAsync(string filtro, int modo);
        Task<int?> CreateAnimeAsync(Anime anime);
        Task<Anime> UpdateAnimeAsync(Anime anime);
        Task<Anime> LogicalDeleteAsync(Anime anime);
        Task<int> GetTotalCountAsync(string diretor, string nome, string keyword);
    }
}