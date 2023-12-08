using OneIdentity.Homework.ServiceDefaults;

var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddContainer(Constants.Mongo, builder.Configuration["Containers:Mongo:Image"]!, builder.Configuration["Containers:Mongo:Tag"]!)
    .WithServiceBinding(containerPort: 27017, hostPort: 27017, scheme: "http", name: Constants.MongoEndpoint)
    .WithEnvironment("MONGO_INITDB_ROOT_USERNAME", builder.Configuration["Containers:Mongo:Env:RootUser"]!)
    .WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", builder.Configuration["Containers:Mongo:Env:RootPassword"]);

builder.AddProject<Projects.OneIdentity_Homework_Api>(Constants.Api)
    .WithReference(mongo.GetEndpoint(Constants.MongoEndpoint));

builder.Build().Run();
