using MinimalHttpClientFacade.Authentication;
using MinimalHttpClientFacade.Authentication.OpenIdConnect;
using MinimalHttpClientFacade.HttpClient;

namespace MinimalHttpClientFacade.RequestExecution
{
    public class HttpRequestExecutor : IHttpRequestExecutor
	{
		private readonly IHttpClientWrapper httpClient;

		private IAuthenticationStrategy? authenticationStrategy;

		public HttpRequestExecutor(IHttpClientWrapper httpClient)
		{
			this.httpClient = httpClient;
			this.authenticationStrategy = null;
		}

		public async Task<HttpResponseMessage> Execute(HttpRequestMessage request)
		{
			if (this.HasAuthenticationStrategy)
			{
				await this.authenticationStrategy!.Execute(this.httpClient);
			}

			return await this.httpClient.SendAsync(request);
		}

		public void UseOidcAuthentication(OidcAuthenticationSettings settings)
		{
			this.authenticationStrategy = new OidcAuthenticationStrategy(settings);
		}

		private bool HasAuthenticationStrategy => this.authenticationStrategy is not null;
	}
}