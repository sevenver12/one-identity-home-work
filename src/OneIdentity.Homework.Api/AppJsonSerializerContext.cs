using OneIdentity.Homework.Repository.Models.User;
using System.Text.Json.Serialization;

namespace OneIdentity.Homework.Api;

[JsonSerializable(typeof(CreateUser[]))]
[JsonSerializable(typeof(UpdateUser[]))]
[JsonSerializable(typeof(User[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}