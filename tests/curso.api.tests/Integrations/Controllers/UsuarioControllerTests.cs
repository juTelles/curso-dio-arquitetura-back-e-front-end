using AutoBogus;
using curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace curso.api.tests.Integrations.Controllers
{
    public class UsuarioControllerTests : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper _output;
        private readonly HttpClient _httpClient;
        protected RegistroViewModelInput RegistroViewModelInput;


        public UsuarioControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        //WhenGivenThen
        public async Task Registrar_InformandoUsuarios_DeveRetornarSucesso()
        {
            //Arrange
            RegistroViewModelInput = new RegistroViewModelInput
            {
                Login = "teste13",
                Email = "teste13@teste.com",
                Senha = "teste13"
            };

        StringContent content = new StringContent(JsonConvert.SerializeObject(RegistroViewModelInput), Encoding.UTF8, "application/json");

            //Act 
            var httpClientRequest = await _httpClient.PostAsync("api/v1/usuario/registrar", content);

            //Assert
            Assert.Equal(HttpStatusCode.Created, httpClientRequest.StatusCode);
            _output.WriteLine(httpClientRequest.ToString());
        }

        [Fact]
        //WhenGivenThen
        public async Task Logar_InformandoUsuarioESenhaExistentes_DeveRetornarSucesso()
        {
            //Arrange
            var loginViewModeInput = new LoginViewModelInput
            {
                Login = RegistroViewModelInput.Login,
                Senha = RegistroViewModelInput.Senha
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginViewModeInput), Encoding.UTF8, "application/json");

            //Act 
            var httpClientRequest = await _httpClient.PostAsync("api/v1/usuario/logar", content);

            var loginViewModelOutput = JsonConvert.DeserializeObject<LoginViewModelOutput>(await httpClientRequest.Content.ReadAsStringAsync());

            //Assert
            Assert.Equal(HttpStatusCode.OK, httpClientRequest.StatusCode);
            Assert.NotNull(loginViewModelOutput.Token);
            Assert.Equal(loginViewModeInput.Login, loginViewModelOutput.Usuario.Login);
            _output.WriteLine(loginViewModelOutput.Token);

        }

        
        public async Task InitializeAsync()
        {
            await Registrar_InformandoUsuarios_DeveRetornarSucesso();
        }

        public async Task DisposeAsync()
        {
            _httpClient.Dispose();
        }
    }
}
