using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneIdentity.Homework.Api.Extensions;
using OneIdentity.Homework.Api.Validation;
using OneIdentity.Homework.Database;
using OneIdentity.Homework.Repository.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddRepository();
builder.Services.AddValidatorsFromAssemblyContaining<PagedParametersValidation>();

builder.Services.AddFluentValidationAutoValidation();
builder.AddServiceDefaults();

builder.Services.AddDbContext<EfContext>((sp, opt) =>
{
    opt.UseMongoDB(sp.ResolveMongoDbConnectionString(builder), "mongo");
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapDefaultEndpoints();
app.MapControllers();
app.Run();
