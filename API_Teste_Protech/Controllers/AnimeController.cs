using ProtechAnime.Application.Services;
using ProtechAnime.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProtechAnime.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeService _animeService;

        public AnimeController(IAnimeService animeService)
        {
            _animeService = animeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimeById(int id)
        {
            try
            {
                var anime = await _animeService.GetAnimeAsync(id);
                if (anime == null)
                {
                    return NotFound();
                }

                return Ok(anime);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("listagemPorNome/{nome}")]
        public async Task<IActionResult> GetAnimesByName([FromRoute] string nome)
        {
            try
            {
                var animes = await _animeService.GetAnimesFilteredAsync(nome, 1);
                if (animes == null)
                {
                    return NotFound();
                }

                return Ok(animes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("listagemPorDiretor/{diretor}")]
        public async Task<IActionResult> GetAnimesByDirector([FromRoute] string diretor)
        {
            try
            {
                var animes = await _animeService.GetAnimesFilteredAsync(diretor, 2);
                if (animes == null)
                {
                    return NotFound();
                }

                return Ok(animes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("listagemPorResumo/{palavrasChave}")]
        public async Task<IActionResult> GetAnimesBySummary([FromRoute] string palavrasChave)
        {
            try
            {
                var animes = await _animeService.GetAnimesFilteredAsync(palavrasChave, 3);
                if (animes == null)
                {
                    return NotFound();
                }

                return Ok(animes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        /// <summary>
        /// Cadastro de um anime
        /// </summary>
        /// <remarks>
        /// Para Cadastrar um anime, basta colocar na requição
        /// todos os dados do anime.
        /// Exemplo:
        ///     
        ///     {
        ///        "Nome": "Attack On Titan",
        ///        "Diretor": "Araki, Tetsurou",
        ///        "Resumo": "anime sobre titans"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("cadastrarAnime")]
        public async Task<IActionResult> CreateAnime([FromBody] Anime anime)
        {
            try
            {
                if (anime == null)
                {
                    return BadRequest("Os dados do anime estão inválidos.");
                }

                var animeId = await _animeService.CreateAnimeAsync(anime);

                return Ok($"novo anime: {animeId}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualizacao de um anime
        /// </summary>
        /// <remarks>
        /// Para Atualizar um anime, basta colocar na requição
        /// o Id ou o Nome do Anime e os outros elementos
        /// que deseja ser atualizado.
        /// Exemplo:
        ///     
        ///     {
        ///        "Nome": "Attack On Titan",
        ///        "Diretor": "Araki, Tetsurou"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("atualizarAnime")]
        public async Task<IActionResult> UpdateAnime([FromBody] Anime anime)
        {
            // anime pego pelo body da requisicao, pode ser Id ou Nome
            try
            {
                if (anime == null)
                {
                    return BadRequest("Os dados do anime estão inválidos.");
                }

                var animeAtualizado = await _animeService.UpdateAnimeAsync(anime);

                return Ok($"Anime {animeAtualizado.Nome} Atualizado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Remoção logica de um anime
        /// </summary>
        /// <remarks>
        /// Para Deletar um anime, basta colocar na requição
        /// o Id ou o Nome do Anime para deletar.
        /// Exemplo:
        ///     
        ///     {
        ///        "Nome": "Attack On Titan"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("deletarAnime")]
        public async Task<IActionResult> DeleteAnime([FromBody] Anime anime)
        {
            // anime pego pelo body da requisicao, pode ser Id ou Nome
            try
            {
                if (anime == null)
                {
                    return BadRequest("Os dados do anime estão inválidos.");
                }

                var animeAtualizado = await _animeService.LogicalDeleteAsync(anime);

                return Ok($"Anime {animeAtualizado.Nome} Deletado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}