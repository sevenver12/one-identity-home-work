var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.OneIdentity_Homework_Api>("Homework Api");

builder.Build().Run();
