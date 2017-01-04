using System;
using Spotify.NET;
using Spotify.NET.Common;
using Xunit;

namespace Charmer.API.Tests
{
    public class SpotifyClientTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData(null)]
        public void InitializeInvalidClientIdShouldFail(string clientId)
        {
            // Arrange
            var clientSecret = "notarealclientsecret";

            // Act
            Exception ex = Record.Exception(() => new SpotifyClient(clientId, clientSecret));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType(typeof(ArgumentNullException), ex);
            Assert.Equal("clientId", ((ArgumentNullException)ex).ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData(null)]
        public void InitializeInvalidClientSecretShouldFail(string clientSecret)
        {
            // Arrange
            var clientId = "notarealclientsecret";

            // Act
            Exception ex = Record.Exception(() => new SpotifyClient(clientId, clientSecret));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType(typeof(ArgumentNullException), ex);
            Assert.Equal("clientSecret", ((ArgumentNullException)ex).ParamName);
        }

        [Fact]
        public void InitializeShouldSetClientId()
        {
            // Arrange
            var clientId = "notarealclientid";
            var clientSecret = "notarealclientsecret";

            // Act
            var client = new SpotifyClient(clientId, clientSecret);

            // Assert
            Assert.Equal(clientId, client.ClientId);
        }

        [Fact]
        public async void AuthenticateAsyncNoClientIdShouldFailAsync()
        {
            //Given
            var client = new SpotifyClient();

            //When
            var result = await client.AuthenticateAsync();

            //Then
            Assert.True(result == SP_ERROR.SP_ERROR_NO_CREDENTIALS);
            Assert.False(client.IsAuthenticated);
        }

        [Fact]
        public async void AuthenticateAsyncInvalidClientShouldFailAsync()
        {
            //Given
            var clientId = "notarealclientid";
            var clientSecret = "notarealclientsecret";
            var client = new SpotifyClient(clientId, clientSecret);

            //When
            var result = await client.AuthenticateAsync();

            //Then
            Assert.NotEqual(SP_ERROR.SP_ERROR_OK, result);
            Assert.False(client.IsAuthenticated);
        }

        [Fact]
        public async void AuthenticateAsyncShouldSucceedAsync()
        {
            //Given
            var clientId = "e6d1d4b9a9a04114ba684cc0fe2ac5eb";
            var clientSecret = "6412b24d8de34ab291b73ed9b383bdaf";
            var client = new SpotifyClient(clientId, clientSecret);

            //When
            await client.AuthenticateAsync();

            //Then
            Assert.True(client.IsAuthenticated);
        }

        [Fact]
        public async void SearchAsyncShouldReturnOKAsync()
        {
            //Given
            var clientId = "e6d1d4b9a9a04114ba684cc0fe2ac5eb";
            var clientSecret = "6412b24d8de34ab291b73ed9b383bdaf";
            var searchQuery = "Summer Friends Chance the Rapper";
            SPItemType[] searchType = { SPItemType.Album, SPItemType.Track };
            var client = new SpotifyClient(clientId, clientSecret);

            //When
            var result = await client.SearchAsync(searchQuery, searchType);

            //Then
            Assert.NotNull(result);
            Assert.IsType<SPSearchResult>(result);
        }

        [Fact]
        public async void SearchAsyncAuthenticatedShouldReturnOKAsync()
        {
            //Given
            var clientId = "e6d1d4b9a9a04114ba684cc0fe2ac5eb";
            var clientSecret = "6412b24d8de34ab291b73ed9b383bdaf";
            var searchQuery = "Summer Friends Chance the Rapper";
            var client = new SpotifyClient(clientId, clientSecret);
            await client.AuthenticateAsync();

            //When
            var result = await client.SearchAsync(searchQuery, SPItemType.Album, SPItemType.Track);

            //Then
            Assert.NotNull(result);
            Assert.IsType<SPSearchResult>(result);
        }
    }
}
