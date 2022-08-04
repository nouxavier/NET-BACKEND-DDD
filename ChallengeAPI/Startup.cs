using ChallengeDominio._Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using ChallengeDominio.Conversores;
using ChallengeDominio;
using ChallengeDominio.VO;
using ChallengeDominio.Conversores.V1;
using ChallengeDominio.Model.Sensores;
using ChallengeDominio.Model.Sensores.V1;
using ChallengeDominio.Conversores.Sensores.V1;
using ChallengeDominio.VO.Sensores.V1;
using ChallengeAplicacao.Operacoes.Sensores;
using ChallengeAplicacao.Repositorio;
using ChallengeRepositorioPostgresql.Repositorio.Localidade;
using ChallengeRepositorioPostgresql._Util;
using Microsoft.OpenApi.Models;
using Serilog;
using ChallengeDominio.Cache.Localidade;
using ChallengeAplicacao.Cache.Localidade;
using ChallengeAplicacao.Operacoes;
using ChallengeAplicacao.Operacoes.Localidade;

namespace ChallengeAPI
{
    public class Startup
    {
        public DbConnection DbConnection => new NpgsqlConnection(Configuration.GetConnectionString("Challenge"));
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllers();
            //Log
            Serilog.Core.Logger serilog = new LoggerConfiguration()
              .ReadFrom.Configuration(Configuration)
              .CreateLogger();
            var loggerFactory = new LoggerFactory()
                .AddSerilog(serilog);
            Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger("RedeAssistenciaLogger");
            services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(logger);


            //Api Version
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChallengeAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()

                    }
                });

            });
            /////Registros de DI/////
            //Caches
            services.AddSingleton<ICacheLocalidade, CacheLocalidade>();
            services.AddMemoryCache();
            //Utils
            services.AddSingleton<IStringLocalizer, StringProvider>();
            services.AddScoped<ITradutorExcecao, TradutorExcecao>();

            //DI Conversores
            services.AddSingleton<IConversor<IOpcoesPesquisa, OpcoesPesquisaVO>, ConversorOpcoesPesquisa>();
            services.AddSingleton<IConversor<IOpcoesEventosSensores, OpcoesEventosSensoresVO>, ConversorOpcoesEventosSensores>();

            //DI Operacoes
            services.AddScoped<IOperacoesSensores, OperacoesSensores>();
            services.AddTransient<IOpcoesEventosSensores, OpcoesEventosSensores>();
            services.AddScoped<IUoWAplicacao, UoWAplicacao>();

            services.AddScoped<IOperacoesLocalidade, OperacoesLocalidade>();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Configurando Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
