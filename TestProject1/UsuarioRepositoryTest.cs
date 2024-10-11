using Moq;
using web_app_domain;
using web_app_repository;


namespace Test
{
    public class UsuarioRepositoryTest
    {
        [Fact]
        public async Task ListarUsuarios()
        {
            //Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario()
                {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "Vicenzo Castelli"
                },
                  new Usuario()
                {
                    Email = "yyy@gmail.com",
                    Id = 2,
                    Nome = "Vicenzo"
                },
            };

            var userRepositoryMock = new Mock<IUsuarioRepository>();
            userRepositoryMock.Setup(u => u.ListarUsuarios()).ReturnsAsync(usuarios);
            var userRepository = userRepositoryMock.Object;

            //Act
            var result = await userRepository.ListarUsuarios();

            //Assert
            Assert.Equal(usuarios, result);



        }
    }
}
