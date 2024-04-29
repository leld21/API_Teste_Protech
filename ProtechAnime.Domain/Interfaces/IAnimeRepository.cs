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
        Task<int> GetTotalCountAsync(string diretor, string nome, string keyword);
    }
}