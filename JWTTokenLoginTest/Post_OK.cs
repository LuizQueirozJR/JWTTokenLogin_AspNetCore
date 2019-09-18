using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

[TestClass]
public class Post_OK : BaseBDDTestFixture
{
  private TestServer _server;
  private HttpClient _client;
  private WebHostBuilder _webHostBuilder;
  private HttpResponseMessage _postToken;
  private HttpResponseMessage _getValues;
  private string _token;
  private List<string> _values;

  public override void Initialize()
  {
    _webHostBuilder = new WebHostBuilder();
    _webHostBuilder
      //.ConfigureTestServices(service => service.AddScoped<IInterface>(serviceProvider => _mockObject.Object))
      .UseStartup<StartupTest>(); // Startup class of your web app project
  }

  public override void Given()
  {
    //Arrange
  }

  public override void When()
  {
    //Act
    var jsonContent = JsonConvert.SerializeObject(string.Empty);
    var postContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

    using (var server = new TestServer(_webHostBuilder))
    {
      using (var client = server.CreateClient())
      {
        //username and password in a base64 text
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes("Admin:Admin"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        _postToken = client.PostAsync("/api/auth/token", postContent).Result;
        _token = _postToken.Content.ReadAsStringAsync().Result;
      }

      using (var client = server.CreateClient())
      {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        _getValues = client.GetAsync("/api/values/get").Result;

      }
    }
  }

  [Then, TestCategory("EachBuild")]
  public void Quando_SolicitoPosicaoCarteira_ComPosicaoExistente_E_FiltroPreenchidoCorretamente_Entao_Sucesso()
  {
    //Assert
    Assert.IsNotNull(_postToken);
    Assert.AreEqual(_postToken.StatusCode, HttpStatusCode.OK);
    Assert.IsTrue(!string.IsNullOrWhiteSpace(_token));
  }
}