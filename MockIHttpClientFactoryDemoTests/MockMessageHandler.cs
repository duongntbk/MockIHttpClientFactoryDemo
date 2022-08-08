using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MockIHttpClientFactoryDemoTests
{
    public class MockMessageHandler : HttpMessageHandler
    {
        private readonly Uri _requestUri;
        private readonly string _result;

        public MockMessageHandler(Uri requestUri, string result)
        {
            _requestUri = requestUri;
            _result = result;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            if (request.RequestUri == _requestUri)
            {
                response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(_result)
                };
            }
            else
            {
                response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return Task.FromResult(response);
        }
    }
}
