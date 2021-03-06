﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace WebJob_Quartz_Clustering
{
    /**
     * Caso queira usar classe para configurar
     */
    public class QuartzConfiguration
    {

        /**
        * Método com as configurações do banco remoto
        * 
        */
        public static NameValueCollection RemoteConfig()
        {
            NameValueCollection configuration = new NameValueCollection
        {
            { "quartz.scheduler.instanceName", "RemoteServer" },
            { "quartz.scheduler.instanceId", "RemoteServer" },
            { "quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz" },
            { "quartz.jobStore.useProperties", "true" },
            { "quartz.jobStore.dataSource", "default" },
            { "quartz.jobStore.tablePrefix", "QRTZ_" },
            { "quartz.dataSource.default.connectionString",
              "Server=(servername);Database=(datbasename);Trusted_Connection=true;" },
            { "quartz.dataSource.default.provider", "SqlServer" },
            { "quartz.threadPool.threadCount", "1" },
            { "quartz.serializer.type", "binary" },
        };

            return configuration;
        }

        /**
         * Método com as configurações do banco local
         * 
         */
        public static NameValueCollection LocalConfig()
        {
            NameValueCollection configuration = new NameValueCollection
        {
            { "quartz.scheduler.instanceName", "LocalServer" },
            { "quartz.scheduler.instanceId", "LocalServer" },
            { "quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz" },
            { "quartz.jobStore.useProperties", "true" },
            { "quartz.jobStore.dataSource", "default" },
            { "quartz.jobStore.tablePrefix", "QRTZ_" },
            { "quartz.dataSource.default.connectionString",
              "Server=(localdb)\\MSSQLLocalDB;Database=configuracoes_quartz;Trusted_Connection=true;" },
            { "quartz.dataSource.default.provider", "SqlServer" },
            { "quartz.threadPool.threadCount", "1" },
            { "quartz.serializer.type", "binary" },
        };

            return configuration;
        }
    }
}
