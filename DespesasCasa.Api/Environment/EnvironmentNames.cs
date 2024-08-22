namespace DespesasCasa.Api.Environment;

public static class EnvironmentNames
{
    public static string Debug => "Debug";
    public static string Development => "Development";
    public static string Quality => "Quality";
    public static string Production => "Production";

    public static string[] all => new[] { Debug, Development, Quality, Production };
}
