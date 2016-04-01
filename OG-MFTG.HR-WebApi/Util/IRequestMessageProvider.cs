using System.Net.Http;

namespace OG_MFTG.HR_WebApi.Util
{
    public interface IRequestMessageProvider
    {
        HttpRequestMessage CurrentMessage { get; }
    }
}
