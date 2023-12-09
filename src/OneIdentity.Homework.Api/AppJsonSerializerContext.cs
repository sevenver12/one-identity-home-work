using OneIdentity.Homework.Repository.Models.User;
using System.Text.Json.Serialization;

[JsonSerializable(typeof(CreateUser[]))]
[JsonSerializable(typeof(UpdateUser[]))]
[JsonSerializable(typeof(User[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}