using Moq;
using web_app_repository;
using web_app_performance.Controllers;
using web_app_domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Test
{
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioRepository> _userRepositoryMock;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            _userRepositoryMock = new Mock<IUsuarioRepository>();
            _controller = new UsuarioController(_userRepositoryMock.Object);
        }

        [Fact]

        public async Task Get_UsuariosOk()
        {
            //arrange
            var usuarios = new List<Usuario>
            {
                new Usuario()
                {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "Guilherme Miguel"
                }
            };
            _userRepositoryMock.Setup(r => r.ListarUsuarios()).ReturnsAsync(usuarios);

            //Act
            var result = await _controller.GetUsuario();

            //Asserts
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(usuarios), JsonConvert.SerializeObject(okResult.Value));
        }

        [Fact]

        public async Task Get_ListarRetornarNotFound()
        {
            _userRepositoryMock.Setup(u => u.ListarUsuarios())
                .ReturnsAsync((IEnumerable<Usuario>)null);

            var result = await _controller.GetUsuario();


            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_SalvarUsuario()
        {

            //Arrange
            var usuario = new Usuario()
            {
                Id = 1,
                Email = "teste@fiap.com",
                Nome = "Vicenzo Castelli"
            };

            _userRepositoryMock.Setup(u => u.SalvarUsuario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            //Act
            var result = _controller.Post(usuario);
            Assert.IsType<OkObjectResult>(result);
            _userRepositoryMock.Verify(u => u.SalvarUsuario(It.IsAny<Usuario>()), Times.Once);

        }

    }
}
