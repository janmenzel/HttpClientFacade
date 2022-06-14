using System.Text.Json;
using MinimalHttpClientFacade.HttpClient;

namespace MinimalHttpClientFacade.Authentication.OpenIdConnect
{
    public class OidcAuthenticationStrategy : IAuthenticationStrategy
	{
		private readonly OidcAuthenticationSettings settings;

		public OidcAuthenticationStrategy(OidcAuthenticationSettings settings)
		{
			this.settings = settings;
		}

		public async Task Execute(IHttpClientWrapper httpClientWrapper)
		{
			HttpRequestMessage authenticationRequest = this.CreateAuthenticationRequest();

			HttpResponseMessage authenticationResponse = await httpClientWrapper.SendAsync(authenticationRequest);
			if (!authenticationResponse.IsSuccessStatusCode)
			{
				throw new ArgumentException("Could not authenticate - include further logging here.");
			}

			string? accessToken = await ParseAccessToken(authenticationResponse.Content);

			httpClientWrapper.SetAuthorizationHeader(accessToken, "Bearer");
		}

		private static async Task<string?> ParseAccessToken(HttpContent content)
		{
			string response = await content.ReadAsStringAsync();
			string? accessToken = JsonSerializer.Deserialize<OidcAuthenticationResponse>(response)?.AccessToken;

			if (accessToken is null)
			{
				throw new ArgumentException("Access Token is not present in response. - Further logging here.");
			}

			return accessToken;
		}

		private HttpRequestMessage CreateAuthenticationRequest()
		{
			return new HttpRequestMessage(HttpMethod.Post, this.settings.AuthenticationUrl)
			{
				Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
				{
					new("grant_type", this.settings.GrantType),
					new("client_id", this.settings.ClientId),
					new("username", this.settings.Username),
					new("password", this.settings.Password)
				})
			};
		}
	}
}