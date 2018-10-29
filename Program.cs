﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace si.dezo.test.DotNetAudit {
    public class Program {
        public static void Main (string[] args) {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog ("nlog.config").GetCurrentClassLogger ();
            logger.Debug ("Starting up ...");
            CreateWebHostBuilder (args).Build ().Run ();
            NLog.LogManager.Shutdown ();
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ()
            .ConfigureLogging (logging => {
                logging.ClearProviders ();
                logging.SetMinimumLevel (Microsoft.Extensions.Logging.LogLevel.Trace);
            })
            .UseNLog ()
        ;
    }
}