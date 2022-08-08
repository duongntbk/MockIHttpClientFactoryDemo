using System.Threading.Tasks;

namespace MockIHttpClientFactoryDemo
{
    public class CheckPasswordService : ICheckPasswordService
    {
        private readonly IHashService _hashService;
        private readonly IPassPwnedCheckClient _passPwnedCheckClient;

        public CheckPasswordService(IHashService hashService, IPassPwnedCheckClient passPwnedCheckClient)
        {
            _hashService = hashService;
            _passPwnedCheckClient = passPwnedCheckClient;
        }

        public async Task<int> DoCheck(string password)
        {
            var passHash = _hashService.HashText(password);
            var prefix = passHash[..5];
            var postfix = passHash[5..];

            var hashDict = await _passPwnedCheckClient.GetHashes(prefix);
            if (hashDict.TryGetValue(postfix, out var times))
            {
                return times;
            }

            return 0;
        }
    }
}
