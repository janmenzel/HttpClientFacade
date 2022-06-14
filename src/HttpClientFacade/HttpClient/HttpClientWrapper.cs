using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace MinimalHttpClientFacade.HttpClient
{
    using HttpClient = System.Net.Http.HttpClient;

	[ExcludeFromCodeCoverage]
	public class HttpClientWrapper : IHttpClientWrapper
	{
		private readonly HttpClient httpClient;

		public HttpClientWrapper(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
		{
			return await this.httpClient.SendAsync(request);
		}

		public void SetAuthorizationHeader(string? scheme, string value)
		{
			this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, value);
		}
	}

	public interface IHttpClientWrapper
	{
		Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

		void SetAuthorizationHeader(string? scheme, string value);
	}
}