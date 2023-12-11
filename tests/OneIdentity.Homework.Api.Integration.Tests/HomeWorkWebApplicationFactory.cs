using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using OneIdentity.Homework.ServiceDefaults;

namespace OneIdentity.Homework.Api.Integration.Tests;
internal class HomeWorkWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private string? _dbContainerAddress;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseSetting($"Services:{Constants.DatabaseServiceName}:1", _dbContainerAddress);
    }
    public void ConfigureDbContainer(string hostname, int port)
    {
        _dbContainerAddress = $"{hostname}:{port}";
    }
}
