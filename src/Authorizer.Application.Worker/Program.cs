using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authorizer.Application.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var service = new ServiceCollection();
                var services = Startup.ConfigureServices(service);
                var serviceProvider = services.BuildServiceProvider();

                serviceProvider.GetService<EntryPoint>().Run(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
