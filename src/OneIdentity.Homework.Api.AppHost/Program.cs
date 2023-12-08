using OneIdentity.Homework.ServiceDefaults;

var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddContainer(Constants.Mongo, builder.Configuration["Containers:Mongo:Image"]!, builder.Configuration["Containers:Mongo:Tag"]!)
    .WithServiceBinding(containerPort: 27017, hostPort: 27017, name: Constants.MongoEndpoint);

builder.AddProject<Projects.OneIdentity_Homework_Api>(Constants.Api)
    .WithReference(mongo.GetEndpoint(Constants.MongoEndpoint));

builder.Build().Run();
