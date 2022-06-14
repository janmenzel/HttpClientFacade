using MinimalHttpClientFacade.Authentication;

namespace MinimalHttpClientFacade
{
    public interface IHttpClientFacade : IAuthenticatable
	{
		Task<HttpResponseMessage> SendAsync(HttpRequestMessage message);

		Task<HttpResponseMessage> GetAsync(Uri uri);
	}
}