namespace baitap6._2.Data;

internal static class DatabaseSettings
{
    private const string DefaultConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=HRMDB;Trusted_Connection=True;TrustServerCertificate=True";

    public static string ConnectionString =>
        Environment.GetEnvironmentVariable("HRMDB_CONNECTION_STRING") ?? DefaultConnectionString;
}
