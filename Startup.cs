using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreVanillaJs
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

            services.AddCors(options => {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllers();




            //**   //O services abaixo representa uma injecao de dependencias
            //services.AddCors(options =>
            //{ 
            //options representa uma politica
            // options.AddPolicy("permitir", //<<<<o primeiro parametro è um nome qualquer a sua escolh, no caso "permitir"
            // builder =>// o builder representa o que vai estar dentro da politica, dentro da permissao da injecao da dependencia
            //  { 
            //        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); //aqui o builder esta permitindo qualquer encabezado, metodo e origin
            //      }); 
            //com essa politica builder vai nos permitir fazer as solicitacoes sem problemas
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting();
            app.UseCors("AllowAll");
         //   app.UseCors("permitir");  //na configure, esta usando a palavra que foi declarada na injecao de dependencias de Cors
            //e depois teremos que colocar essa chamada da permissao no controlador

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            
        }
    }
}
