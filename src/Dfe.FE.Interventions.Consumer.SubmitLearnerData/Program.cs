using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dfe.FE.Interventions.Consumer.SubmitLearnerData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var startup = new Startup();
            
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) => startup.ConfigureServices(services));
        }
    }
}