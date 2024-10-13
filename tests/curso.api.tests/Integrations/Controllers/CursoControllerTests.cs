using AutoBogus;
using curso.api.Models.Cursos;
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

}
