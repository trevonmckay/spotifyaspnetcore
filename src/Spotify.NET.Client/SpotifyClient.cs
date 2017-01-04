using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spotify.NET.Common;

namespace Spotify.NET
{
    public class SpotifyClient
    {
        private struct TokenResponse
        {
            public string AccessToken { get; set; }
            public string TokenType { get; set; }
            public int ExpiresIn { get; set; }
            public DateTime Issued { get; set; }
            public DateTime Expires
            {
                get
                {
                    return Issued.AddSeconds(ExpiresIn);
                }
            }

            public TokenResponse(string body)
            {
                JToken _accessToken;
                JToken _tokenType;
                JToken _expiresIn;

                var o = JObject.Parse(body);
                if (!o.TryGetValue("access_token", StringComparison.OrdinalIgnoreCase, out _accessToken))
                {
                    var innerException = new MissingFieldException("Missing 'access_token' property");
                    throw new ArgumentException("The JSON string is not a valid access token response", "body", innerException);
                }

                if (!o.TryGetValue("token_type", StringComparison.OrdinalIgnoreCase, out _tokenType))
                {
                    var innerException = new MissingFieldException("Missing 'token_type' property");
                    throw new ArgumentException("The JSON string is not a valid access token response", "body", innerException);
                }

                if (!o.TryGetValue("expires_in", StringComparison.OrdinalIgnoreCase, out _expiresIn))
                {
                    var innerException = new MissingFieldException("Missing 'expires_in' property");
                    throw new ArgumentException("The JSON string is not a valid access token response", "body", innerException);
                }

                this.AccessToken = _accessToken.Value<string>();
                this.TokenType = _tokenType.Value<string>();
                this.ExpiresIn = _expiresIn.Value<int>();
                this.Issued = DateTime.UtcNow;
            }
        }

        const string API_BASE_URL = "https://api.spotify.com/v1";
        const string AUTHORIZE_ENDPOINT = "https://accounts.spotify.com/authorize";
        const string TOKEN_ENDPOINT = "https://accounts.spotify.com/api/token";
        const string SEARCH_RESOURCE_PATH = "/search";

        public string ClientId { get; private set; }

        private string ClientSecret { get; set; }

        private TokenResponse? ClientToken { get; set; }

        public bool IsAuthenticated
        {
            get
            {
                if (!this.ClientToken.HasValue)
                    return false;

                return (DateTime.UtcNow < this.ClientToken.Value.Expires.ToUniversalTime());
            }
        }

        public SpotifyClient() { }

        public SpotifyClient(string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId), "The argument 'clientId' cannot be null or empty");

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentNullException(nameof(clientSecret), "The argument 'clientSecret' cannot be null or empty");

            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
        }
        
        private async Task<TokenResponse?> RequestClientToken(string clientId, string clientSecret, Uri tokenEndpoint)
        {
            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            };
            var encodedContent = new FormUrlEncodedContent(parameters);

            var authHeader = String.Format("{0}:{1}", clientId, clientSecret);
            var authHeaderBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(authHeader);
            var encodedAuthHeader = Convert.ToBase64String(authHeaderBytes);

            var httpClient = new HttpClient();
            var httpRequest = new HttpRequestMessage();
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedAuthHeader);
            httpRequest.Content = encodedContent;
            httpRequest.Method = HttpMethod.Post;
            httpRequest.RequestUri = tokenEndpoint;

            var response = await httpClient.SendAsync(httpRequest);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = new TokenResponse(responseBody);
                return tokenResponse;
            }

            return null;
        }

        private async Task<TokenResponse?> RequestClientToken(string clientId, string clientSecret, string tokenEndpoint)
        {
            var uri = new Uri(tokenEndpoint);
            return await RequestClientToken(clientId, clientSecret, uri);
        }

        public async Task<SP_ERROR> AuthenticateAsync()
        {
            if(!this.ClientToken.HasValue)
                return SP_ERROR.SP_ERROR_NO_CREDENTIALS;

            if (this.ClientId == null)
                return SP_ERROR.SP_ERROR_INVALID_INDATA;

            if (this.ClientSecret == null)
                return SP_ERROR.SP_ERROR_INVALID_INDATA;

            try
            {
                var tokenResponse = await RequestClientToken(this.ClientId, this.ClientSecret, TOKEN_ENDPOINT);
                this.ClientToken = tokenResponse;
            }
            catch (ArgumentException)
            {
                return SP_ERROR.SP_ERROR_INVALID_ARGUMENT;
            }

            return (this.ClientToken == null) ? SP_ERROR.SP_ERROR_API_INITIALIZATION_FAILED : SP_ERROR.SP_ERROR_OK;
        }

        public async Task<SPSearchResult> SearchAsync(string query, params SPItemType[] type)
        {
            var typeList = new List<string>();
            if (type.Contains(SPItemType.Album))
                typeList.Add("album");
            if (type.Contains(SPItemType.Artist))
                typeList.Add("artist");
            if (type.Contains(SPItemType.Playlist))
                typeList.Add("playlist");
            if (type.Contains(SPItemType.Track))
                typeList.Add("track");

            var types = string.Join(",", typeList);

            var httpClient = new HttpClient();
            var httpRequest = new HttpRequestMessage();
            if (this.ClientToken.HasValue)
            {
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.ClientToken.Value.AccessToken);
            }
            httpRequest.Method = HttpMethod.Get;
            var requestUri = new UriBuilder(API_BASE_URL + SEARCH_RESOURCE_PATH);
            requestUri.AddParameter("q", query);
            if (!string.IsNullOrWhiteSpace(types))
            {
                requestUri.AddParameter("type", types, false);
            }
            httpRequest.RequestUri = requestUri.Uri;
            var response = await httpClient.SendAsync(httpRequest);
            var responseBody = await response.Content.ReadAsStringAsync();

            if(!response.IsSuccessStatusCode)
            {
                return null;
            }

            try
            {
                var result = JsonConvert.DeserializeObject<SPSearchResult>(responseBody);
                return result;
            }
            catch (JsonSerializationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to deserialize search result body. {ex.Message}. (Source: {responseBody})");
                return null;
            }
        }
    }
}
