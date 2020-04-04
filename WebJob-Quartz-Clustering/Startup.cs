using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using WebJob_Quartz_Clustering.Jobs;

namespace WebJob_Quartz_Clustering
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Veja o Resultado no Console!");
            });

            this.StartScheduler();
        }

        /**
         *  gerenciador (scheduler)
         *  O que fizemos aqui foi criar um SchedulerFactory, o StdSchedulerFactory irá carregar as propriedades do arquivo quartz.config
         *  
         */
        public async void StartScheduler()
        {
            //ISchedulerFactory factory = new StdSchedulerFactory();

            ISchedulerFactory factory = new StdSchedulerFactory(QuartzConfiguration.LocalConfig());

            IScheduler scheduler = await factory.GetScheduler();

            AdicionaJobAoShedule(scheduler);

             await scheduler.Start();
        }

        /**
         * Adicionar o IJob que será usado e as configurações da Trigger
         */
        private async void AdicionaJobAoShedule(IScheduler scheduler) {

            var jobKey = new JobKey("Job", "Radix");
            var triggerKey = new TriggerKey("trigger", "Radix");

            if (!scheduler.CheckExists(jobKey).Result && !scheduler.CheckExists(triggerKey).Result)
            {
                Debug.WriteLine($" \n Configurando o Schedule.... \n");

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<DemoJob>()
                    .WithIdentity(jobKey)
                    .Build();

                // Trigger the job to run now, and then every 40 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(triggerKey)
                    .StartNow()
                    //.WithSimpleSchedule( x => x.WithIntervalInSeconds(15).RepeatForever())
                    .WithCronSchedule("* * * ? * *")
                    .ForJob(job)
                .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }


    }
}
