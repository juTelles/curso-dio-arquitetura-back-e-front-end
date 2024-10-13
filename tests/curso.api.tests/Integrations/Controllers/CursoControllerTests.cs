using AutoBogus;
using curso.api.Models.Cursos;
using curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace curso.api.tests.Integrations.Controllers
{
    public class CursoControllerTests : UsuarioControllerTests
    {

        public CursoControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        //WhenGivenThen
        public async Task Registrar_InformandoDadosDeUmCursoValidoEUmUsuarioAutenticado_DeveRetornarSucesso()
        {
            //Arrange
            var cursoViewModelInput = new AutoFaker<CursoViewModelInput>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(cursoViewModelInput.Generate()), Encoding.UTF8, "application/json");

            //Act
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginViewModelOutput.Token);
            var httpClientRequest = await _httpClient.PostAsync("api/v1/cursos", content);

            //Assert
            _output.WriteLine($"{nameof(CursoControllerTests)}_{nameof(Registrar_InformandoDadosDeUmCursoValidoEUmUsuarioAutenticado_DeveRetornarSucesso)} = {await httpClientRequest.Content.ReadAsStringAsync()}");
            Assert.Equal(HttpStatusCode.Created, httpClientRequest.StatusCode);
        }

        [Fact]
        //WhenGivenThen
        public async Task Registrar_InformandoDadosDeUmCursoValidoEUmUsuarioNaoAutenticado_DeveRetornarSucesso()
        {
            //Arrange
            var cursoViewModelInput = new AutoFaker<CursoViewModelInput>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(cursoViewModelInput.Generate()), Encoding.UTF8, "application/json");

            //Act
            var httpClientRequest = await _httpClient.PostAsync("api/v1/cursos", content);

            //Assert
            _output.WriteLine($"{nameof(CursoControllerTests)}_{nameof(Registrar_InformandoDadosDeUmCursoValidoEUmUsuarioNaoAutenticado_DeveRetornarSucesso)} = {await httpClientRequest.Content.ReadAsStringAsync()}");
            Assert.Equal(HttpStatusCode.Unauthorized, httpClientRequest.StatusCode);
        }

        [Fact]
        //WhenGivenThen
        public async Task Obter_InformandoUsuarioAutenticado_DeveRetornarSucesso()
        {
            //Arrange
            await Registrar_InformandoDadosDeUmCursoValidoEUmUsuarioAutenticado_DeveRetornarSucesso();

            //Act
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginViewModelOutput.Token);
            var httpClientRequest = await _httpClient.GetAsync("api/v1/cursos");

            //Assert
            _output.WriteLine($"{nameof(CursoControllerTests)}_{nameof(Obter_InformandoUsuarioAutenticado_DeveRetornarSucesso)} = {await httpClientRequest.Content.ReadAsStringAsync()}");
            var curso = JsonConvert.DeserializeObject<IList<CursoViewModelOutput>>(await httpClientRequest.Content.ReadAsStringAsync());

            Assert.NotEmpty(curso);
            Assert.Equal(HttpStatusCode.OK, httpClientRequest.StatusCode);
        }


        public Task DisposeAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task InitializeAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
