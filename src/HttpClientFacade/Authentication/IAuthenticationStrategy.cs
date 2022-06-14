using MinimalHttpClientFacade.HttpClient;

namespace MinimalHttpClientFacade.Authentication
{
    public interface IAuthenticationStrategy
	{
		Task Execute(IHttpClientWrapper httpClientWrapper);
	}
}