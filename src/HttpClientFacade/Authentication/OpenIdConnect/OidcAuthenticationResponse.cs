
using System.Text.Json.Serialization;

namespace MinimalHttpClientFacade.Authentication.OpenIdConnect
{
    public class OidcAuthenticationResponse
	{
		[JsonPropertyName("access_token")]
		public string? AccessToken { get; set; }
	}
}