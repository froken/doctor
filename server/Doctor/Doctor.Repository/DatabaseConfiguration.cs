namespace Doctor.Repository
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["doctor"].ConnectionString;
        }
    }
}
