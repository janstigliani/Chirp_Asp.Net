namespace Chirp
{
    public class AppConfig
    {

        public static IConfiguration Configuration { get; private set; }

        static AppConfig()
        {
            if (Configuration == null)
            {
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("AppConfig.json", optional: false, reloadOnChange: true)
                    .Build();
            }
        }

        public static string GetConnectionString()
        {
            return Configuration.GetConnectionString("postgress");
        }
    }
}
