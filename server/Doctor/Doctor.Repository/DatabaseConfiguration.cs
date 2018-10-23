namespace Doctor.Repository
{
    class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["doctor"].ConnectionString;
        }
    }
}
