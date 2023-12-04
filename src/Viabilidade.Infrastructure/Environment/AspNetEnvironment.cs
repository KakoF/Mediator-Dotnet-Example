namespace Viabilidade.Infrastructure.Environment
{
    public static class AspNetEnvironment
    {
        private const string PRODUCTION_ENVIRONMENT = "Production";
        private const string STAGING_ENVIRONMENT = "Staging";
        private const string DEVELOPMENT_ENVIRONMENT = "Development";
        private const string DISASTER_RECOVERY_ENVIRONMENT = "DisasterRecovery";

        public static string GetEnvironment()
        {
            string aspnetEnv = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            aspnetEnv = string.IsNullOrEmpty(aspnetEnv) ? PRODUCTION_ENVIRONMENT : aspnetEnv;
            return aspnetEnv;
        }

        public static bool IsProductionOrStaging()
        {
            string aspnetEnv = GetEnvironment();
            return aspnetEnv.ToLower() == PRODUCTION_ENVIRONMENT.ToLower() || aspnetEnv.ToLower() == STAGING_ENVIRONMENT.ToLower();
        }

        public static bool IsDevelopment()
        {
            string aspnetEnv = GetEnvironment();
            return aspnetEnv.ToLower() == DEVELOPMENT_ENVIRONMENT.ToLower();
        }

        public static bool IsDisasterRecovery()
        {
            string aspnetEnv = GetEnvironment();
            return aspnetEnv.ToLower() == DISASTER_RECOVERY_ENVIRONMENT.ToLower();
        }
    }
}
