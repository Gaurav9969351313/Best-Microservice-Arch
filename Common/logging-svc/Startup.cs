﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Infrastructure.ServiceDiscovery;
using OpenTracing;
using OpenTracing.Util;
using System.Reflection;
using Jaeger;
using Jaeger.Samplers;

namespace logging_svc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureConsul(services);
            services.AddSingleton<ITracer>(serviceProvider =>
                 {
                     string serviceName = Assembly.GetEntryAssembly().GetName().Name;

                     ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                     ISampler sampler = new ConstSampler(sample: true);

                     ITracer tracer = new Tracer.Builder(serviceName)
                         .WithLoggerFactory(loggerFactory)
                         .WithSampler(sampler)
                         .Build();

                     GlobalTracer.Register(tracer);

                     return tracer;
                 });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void ConfigureConsul(IServiceCollection services)
        {
            var serviceConfig = Configuration.GetServiceConfig();
            services.RegisterConsulServices(serviceConfig);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
