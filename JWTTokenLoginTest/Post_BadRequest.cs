//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.TestHost;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Newtonsoft.Json;
//using System.Net.Http;
//using System.Text;

//[TestClass]
//public class Post_BadRequest : BaseBDDTestFixture
//{
//    private TestServer _server;
//    private HttpClient _client;
//    private WebHostBuilder _webHostBuilder;
//    private HttpResponseMessage _post;
//    private string _retornoMsgErro = string.Empty;

//    public override void Initialize()
//    {
//        _webHostBuilder = new WebHostBuilder();
//        _webHostBuilder
//          //.ConfigureTestServices(service => service.AddScoped<IPosicaoCarteiraRepository>(serviceProvider => _repositorioPosicaoCarteira.Object))
//          .UseStartup<StartupTest>(); // Startup class of your web app project
//    }

//    public override void Given()
//    {
//        //Arrange
 
//    }

//    public override void When()
//    {
//        //Act
//        var jsonContent = JsonConvert.SerializeObject(_carteiraSolicitada);
//        var postContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

//        using (var server = new TestServer(_webHostBuilder))
//        {
//            using (var client = server.CreateClient())
//            {
//                _post = client.PostAsync("/api/PosicaoCarteira/ObtemPosicaoCarteira", postContent).Result;
//                _retornoMsgErro = _post.Content.ReadAsStringAsync().Result;
//            }
//        }
//    }

//    [Then, TestCategory("EachBuild")]
//    public void Quando_SolicitoPosicaoCarteira_ComPosicaoExistente_E_FiltroPreenchidoIncorretamente_ListaCodigoCarteiraVazio_Entao_Erro()
//    {
//        //Assert
//        Assert.IsNotNull(_post);
//        Assert.IsNull(_retornoPosicaoCarteiraDto);
//        Assert.AreEqual(_post.StatusCode, HttpStatusCode.BadRequest);
//        Assert.IsTrue(_retornoMsgErro.Contains("A lista de códigos de carteiras deve conter elementos."));
//    }
//}