using MinimalHttpClientFacade;
using MinimalHttpClientFacade.Authentication.OpenIdConnect;
using MinimalHttpClientFacade.RequestExecution;
using NSubstitute;
using NUnit.Framework;

namespace MinimalHttpClientFacadeTests;

public class HttpClientFacadeTests
{
    private IHttpRequestExecutor httpRequestExecutorMock = null!;
    private HttpClientFacade httpClientFacade = null!;

    [SetUp]
    public void SetUp()
    {
        httpRequestExecutorMock = Substitute.For<IHttpRequestExecutor>();

        httpClientFacade = new HttpClientFacade(httpRequestExecutorMock);
    }

    [Test]
    public void SendAsync_CallsRequestExecutor_WithGivenHttpRequestMessage()
    {
        HttpRequestMessage httpRequestMessage = new();

        this.httpClientFacade.SendAsync(httpRequestMessage);

        this.httpRequestExecutorMock.Received(1).Execute(httpRequestMessage);
    }

    [Test]
    public async Task SendAsync_ReturnsResultOfExecutor()
    {
        HttpRequestMessage httpRequestMessage = new();
        HttpResponseMessage expectedResponseMessage = new();
        this.httpRequestExecutorMock
            .Execute(httpRequestMessage)
            .Returns(Task.FromResult(expectedResponseMessage));

        HttpResponseMessage result = await this.httpClientFacade.SendAsync(httpRequestMessage);

        Assert.That(result, Is.SameAs(expectedResponseMessage));
    }

    [TestCase("https://google.de")]
    [TestCase("http://www.example.com")]
    public void GetAsync_CallsExecuteWithHttpMethodGet(string uri)
    {
        this.httpClientFacade.GetAsync(new Uri(uri));

        this.httpRequestExecutorMock
            .Received(1)
            .Execute(Arg.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get));
    }

    [TestCase("https://www.my-secret-api.io")]
    [TestCase("http://some-uri.com/api/v1/test")]
    public void GetAsync_CallsExecuteWithGivenUri(string uri)
    {
        Uri expectedUri = new(uri);

        this.httpClientFacade.GetAsync(expectedUri);

        this.httpRequestExecutorMock
            .Received(1)
            .Execute(Arg.Is<HttpRequestMessage>(m => m.RequestUri == expectedUri));
    }

    [TestCase("https://i-am-running-out-of-sample-domains.net")]
    [TestCase("http://Test-me-please.io")]
    public async Task GetAsync_RetursResultOfExecutor(string uri)
    {
        HttpResponseMessage expectedResponseMessage = new();
        this.httpRequestExecutorMock
            .Execute(Arg.Any<HttpRequestMessage>())
            .Returns(Task.FromResult(expectedResponseMessage));

        HttpResponseMessage result = await this.httpClientFacade.GetAsync(new Uri(uri));

        Assert.That(result, Is.SameAs(expectedResponseMessage));
    }

    [Test]
    public void UseOidcAuthentication_CallsExecutorWithGivenSettings()
    {
        OidcAuthenticationSettings oidcSettings = new();

        this.httpClientFacade.UseOidcAuthentication(oidcSettings);

        this.httpRequestExecutorMock.Received(1).UseOidcAuthentication(oidcSettings);
    }
}