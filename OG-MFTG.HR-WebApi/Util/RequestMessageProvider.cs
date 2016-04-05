using System.Net.Http;
using SimpleInjector;

namespace OG_MFTG.HR_WebApi.Util
{
    public class RequestMessageProvider : IRequestMessageProvider
    {
        private readonly Container _container;

        public RequestMessageProvider(Container container)
        {
            _container = container;
        }

        public HttpRequestMessage CurrentMessage
        {
            get { return _container.GetCurrentHttpRequestMessage(); }
        }
    }
}