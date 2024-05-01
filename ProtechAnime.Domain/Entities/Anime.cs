using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechAnime.Domain.Entities
{
    public class Anime
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Resumo { get; set; }
        public string? Diretor { get; set; }
        public bool Ativo {  get; set; } = true;
    }
}
