using MinimalHttpClientFacade.Authentication;

namespace MinimalHttpClientFacade.RequestExecution
{
    public interface IHttpRequestExecutor : IAuthenticatable
	{
		Task<HttpResponseMessage> Execute(HttpRequestMessage request);
	}
}