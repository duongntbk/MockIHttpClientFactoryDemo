using System.Threading.Tasks;

namespace MockIHttpClientFactoryDemo
{
    public interface ICheckPasswordService
    {
        Task<int> DoCheck(string password);
    }
}