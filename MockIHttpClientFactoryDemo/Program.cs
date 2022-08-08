using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MockIHttpClientFactoryDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<ICheckPasswordService>();

            Console.Write("Please enter your password: ");
            var password = Console.ReadLine();
            var times = await service.DoCheck(password);

            if (times > 0)
            {
                Console.WriteLine($"Your password has been leaked: {times} time(s).");
            }
            else
            {
                Console.WriteLine("Your password hasn't been leaked yet.");
            }
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services
                .AddSingleton<IHashService, HashService>()
                .AddSingleton<IPassPwnedCheckClient, PassPwnedCheckClient>()
                .AddSingleton<ICheckPasswordService, CheckPasswordService>();
            services.AddHttpClient(Constants.HttpClientName);

            return services.BuildServiceProvider();
        }
    }
}
