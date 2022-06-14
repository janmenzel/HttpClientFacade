namespace MinimalHttpClientFacade.Authentication.OpenIdConnect
{
    public class OidcAuthenticationSettings
	{
		public string GrantType { get; set; }

		public string ClientId { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public Uri AuthenticationUrl { get; set; }
	}
}