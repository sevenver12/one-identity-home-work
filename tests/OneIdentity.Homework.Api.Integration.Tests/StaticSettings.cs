using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace OneIdentity.Homework.Api.Integration.Tests;
internal class StaticSettings
{
    public static class StaticSettingsUsage
    {
        [ModuleInitializer]
        public static void Initialize()
        {
            VerifierSettings.AddExtraSettings(o => o.DefaultValueHandling = Argon.DefaultValueHandling.Include);
            VerifierSettings.AddExtraSettings(o => o.NullValueHandling = Argon.NullValueHandling.Include);
            //VerifierSettings.ignore(line=>line.Contains("traceId: ") ? "traceId" : line);
        }

    }
}
