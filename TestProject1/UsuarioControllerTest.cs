using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace TestProject1
{
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioRepository> _userResporitoryMock;
        private readonly UsuarioController _controller;


        public UsuarioControllerTest()
        {
            _userResporitoryMock = new Mock <IUsuarioRepository>();
            _controller = new UsuarioController(_userResporitoryMock.Object);
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
                    Nome = "Vicenzo"
                }
            };
            _userResporitoryMock.Setup(r => r.ListarUsuarios()).ReturnsAsync(usuarios);


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
           
        }
    }
}
