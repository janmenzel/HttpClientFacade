using MinimalHttpClientFacade.Authentication.OpenIdConnect;

namespace MinimalHttpClientFacade.Authentication
{
    public interface IAuthenticatable
	{
		void UseOidcAuthentication(OidcAuthenticationSettings settings);
	}
}