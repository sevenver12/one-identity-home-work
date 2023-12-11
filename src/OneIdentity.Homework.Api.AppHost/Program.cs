using Microsoft.Extensions.Configuration;
using OneIdentity.Homework.ServiceDefaults;

var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddContainer(Constants.DatabaseServiceName,
                builder.Configuration[Constants.DatabaseImagePath]!,
                builder.Configuration[Constants.DatabaseTagPath]!)
    .WithServiceBinding(containerPort: builder.Configuration.GetValue<int>(Constants.DatabaseContainerPortPath),
                        hostPort: builder.Configuration.GetValue<int>(Constants.DatabaseHostPortPath),
                        scheme: "https",
                        name: Constants.DatabaseEndpoint);
foreach (var env in builder.Configuration.GetSection(Constants.DatabaseEnvsPath).GetChildren())
{
    database.WithEnvironment(env.Key, env.Value);
}

builder.AddProject<Projects.OneIdentity_Homework_Api>(Constants.Api)
    .WithReference(database.GetEndpoint(Constants.DatabaseEndpoint));

builder.Build().Run();
