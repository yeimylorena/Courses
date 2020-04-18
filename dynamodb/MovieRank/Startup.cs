using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MovieRank.Libs.Repositories.ObjectPersistenceModel;
using MovieRank.Libs.Mappers.ObjectPersistenceModel;
using MovieRank.Services;
using Amazon.Extensions.NETCore.Setup;
using Amazon;

namespace MovieRank
{
    public class Startup
    {
        private readonly IHostingEnvironment env;
        public Startup(IHostingEnvironment env)
        {
            this.env = env;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddDefaultAWSOptions(
            //    new AWSOptions
            //    {
            //        Region = RegionEndpoint.GetBySystemName("us-east-1")
            //    });

            if (env.IsDevelopment())
            {
                services.AddSingleton<IAmazonDynamoDB>(cc =>
                {
                    var clientConfig = new AmazonDynamoDBConfig { ServiceURL = "http:localhost:8000" };//4569
                    return new AmazonDynamoDBClient(clientConfig);
                
                });
            }
            else
            {
                services.AddAWSService<IAmazonDynamoDB>();
            }
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddSingleton<IMovieRankService, MovieRank.Services.ObjectPersistenceModel.MovieRankService>();
            services.AddSingleton<IMovieRankRepository, MovieRankRepository>();
            services.AddSingleton<IMapper, Mapper>();
            //services.AddSingleton<ISetupService, SetupService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
