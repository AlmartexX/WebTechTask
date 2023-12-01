namespace VebTechTask.UI.ServiceCollection
{
    public static class ServiceCollection
    {
        public static void AddServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddEndpointsApiExplorer();
            services.AddCustomDbContext(builder);
            services.AddCustomRepositories();
            services.AddCustomServices();
            services.AddCustomMappers();
            services.AddCustomControllers();
            services.AddCustomAuthorizationPolicies();
            services.AddCustomAuthentication(builder);
            services.AddCustomAutoMapper();
            services.AddCustomSwagger();
        }
    }
}
