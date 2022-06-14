using MinimalHttpClientFacade.Authentication.OpenIdConnect;
using MinimalHttpClientFacade.RequestExecution;

namespace MinimalHttpClientFacade
{
    public class HttpClientFacade : IHttpClientFacade
	{
		private readonly IHttpRequestExecutor httpRequestExecutor;

		public HttpClientFacade(IHttpRequestExecutor httpRequestExecutor)
		{
			this.httpRequestExecutor = httpRequestExecutor;
		}

		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
		{
			return this.httpRequestExecutor.Execute(message);
		}

		public Task<HttpResponseMessage> GetAsync(Uri uri)
		{
			return this.httpRequestExecutor.Execute(new HttpRequestMessage(HttpMethod.Get, uri));
		}

		public void UseOidcAuthentication(OidcAuthenticationSettings settings)
		{
			this.httpRequestExecutor.UseOidcAuthentication(settings);
		}
	}
}