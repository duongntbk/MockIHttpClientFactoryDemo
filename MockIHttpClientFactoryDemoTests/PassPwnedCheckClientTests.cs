using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FakeItEasy;
using MockIHttpClientFactoryDemo;
using Xunit;

namespace MockIHttpClientFactoryDemoTests
{
    public class PassPwnedCheckClientTests
    {
        [Fact]
        public async Task TestWithFakeItEasy()
        {
            // Arrange
            var prefix = "90CF8";
            var postfix = "2C8ABCBEB601EF133F2407C32A00E6AB7F9";
            var times = 17;

            var handler = A.Fake<HttpMessageHandler>();
            SetupMessageHandler(handler,
                new Uri($"https://api.pwnedpasswords.com/range/{prefix}"),
                $"{postfix}:{times}");

            var httpClient = new HttpClient(handler);
            var httpClientFactory = A.Fake<IHttpClientFactory>();
            A.CallTo(() => httpClientFactory.CreateClient(Constants.HttpClientName)).Returns(httpClient);
            var sut = new PassPwnedCheckClient(httpClientFactory);

            // Act
            var rs = await sut.GetHashes(prefix);

            // Assert
            Assert.True(rs[postfix] == times);
        }

        [Fact]
        public async Task TestWithMockClass()
        {
            // Arrange
            var prefix = "90CF8";
            var postfix = "2C8ABCBEB601EF133F2407C32A00E6AB7F9";
            var times = 17;

            var requestUri = new Uri($"https://api.pwnedpasswords.com/range/{prefix}");
            var result = $"{postfix}:{times}";
            var handler = new MockMessageHandler(requestUri, result);

            var httpClient = new HttpClient(handler);
            var httpClientFactory = A.Fake<IHttpClientFactory>();
            A.CallTo(() => httpClientFactory.CreateClient(Constants.HttpClientName)).Returns(httpClient);
            var sut = new PassPwnedCheckClient(httpClientFactory);

            // Act
            var rs = await sut.GetHashes(prefix);

            // Assert
            Assert.True(rs[postfix] == times);
        }

        private void SetupMessageHandler(HttpMessageHandler handler, Uri requestUri, string result)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(result)
            };
            A.CallTo(handler).Where(x => x.Method.Name == "SendAsync" &&
                (x.Arguments[0] as HttpRequestMessage).RequestUri == requestUri)
            .WithReturnType<Task<HttpResponseMessage>>()
            .Returns(Task.FromResult(response));
        }
    }
}
