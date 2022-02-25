using DTN.Logic.Helpers;
using DTN.Logic.Helpers.Interfaces;
using DTN.Logic.Services;
using DTN.Logic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DTN.Test
{
    public class CustomServiceBuilder
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public CustomServiceBuilder()
        {
            var service = new ServiceCollection();
            service.AddSingleton<IPasswordHasher, PasswordHasher>();
            service.AddSingleton<IUserService, UserService>();
            ServiceProvider = service.BuildServiceProvider();

        }
    }
}
