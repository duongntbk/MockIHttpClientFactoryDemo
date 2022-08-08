using System.Collections.Generic;
using System.Threading.Tasks;

namespace MockIHttpClientFactoryDemo
{
    public interface IPassPwnedCheckClient
    {
        Task<Dictionary<string, int>> GetHashes(string prefix);
    }
}