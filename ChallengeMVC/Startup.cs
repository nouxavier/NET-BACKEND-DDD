using ChallengeAplicacao.Cache.Localidade;
using ChallengeAplicacao.Operacoes;
using ChallengeAplicacao.Operacoes.Localidade;
using ChallengeAplicacao.Operacoes.Sensores;
using ChallengeAplicacao.Repositorio;
using ChallengeDominio;
using ChallengeDominio._Util;
using ChallengeDominio.Cache.Localidade;
using ChallengeDominio.Conversores;
using ChallengeDominio.Model.Sensores;
using ChallengeDominio.Model.Sensores.V1;
using ChallengeRepositorioPostgresql._Util;
using ChallengeRepositorioPostgresql.Repositorio.Localidade;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using ChallengeDominio.Conversores.V1;
using ChallengeDominio.Conversores.Sensores.V1;
using ChallengeDominio.VO;
using ChallengeDominio.VO.Sensores.V1;

namespace ChallengeMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //Log
            Serilog.Core.Logger serilog = new LoggerConfiguration()
              .ReadFrom.Configuration(Configuration)
              .CreateLogger();
            var loggerFactory = new LoggerFactory()
                .AddSerilog(serilog);
            Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger("RedeAssistenciaLogger");
            services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(logger);
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
