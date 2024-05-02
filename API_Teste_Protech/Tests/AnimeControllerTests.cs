using Microsoft.AspNetCore.Mvc;
using Moq;
using ProtechAnime.API.Controllers;
using ProtechAnime.Application.Services;
using ProtechAnime.Domain.Entities;
using Xunit;

namespace ProtechAnime.API.Tests
{   
    public class AnimeControllerTests
    {
        private Mock<IAnimeService> _animeServiceMock;
        private AnimeController _animeController;

        public AnimeControllerTests()
        {
            _animeServiceMock = new Mock<IAnimeService>();
            _animeController = new AnimeController(_animeServiceMock.Object);
        }

        // Algum erro nao me esta permitindo rodar os testes...
        [Fact]
        public async Task GetAnimeById_RetornaOk()
        {
            // Arrange
            int animeId = 1;
            var anime = new Anime { Id = animeId, Nome = "Bleach", Diretor = "Tite Kubo", Resumo = "jornada de um shinigami" };
            _animeServiceMock.Setup(service => service.GetAnimeAsync(animeId)).ReturnsAsync(anime);

            // Act
            var result = await _animeController.GetAnimeById(animeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Anime>(okResult.Value);
            Assert.Equal(animeId, model.Id);
        }
    }
}