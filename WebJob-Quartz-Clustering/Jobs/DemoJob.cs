using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebJob_Quartz_Clustering.Jobs
{
    /**
     * Essa classe precisa implementar a interface IJob e respectivamente seu método Execute
     */
     [DisallowConcurrentExecution]
    public class DemoJob : IJob
    {
        /**
         * Esse é o método que será chamado quando o job for iniciado
         */
        public Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine($"DemoJob: {DateTime.Now}");
            Debug.WriteLine($"JobKey: {context.JobDetail.Key}");

            return Task.CompletedTask;
        }
    }
}
