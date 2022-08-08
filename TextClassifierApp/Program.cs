
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;


namespace TextClassifierApp
{
    class Program
    {

        public static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length < 2)
                {
                    //log
                    Console.WriteLine("Input Error: Missing arguments");
                    return;
                }

                //init  Services Configuration
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);
                var serviceProvider = serviceCollection.BuildServiceProvider();

                //use textClassifier service to classify b args
                var textClassifier = serviceProvider.GetService<ITextClassifier>();
                ClassifierResponse response = textClassifier.Classify(args);

                //print classified data
                if (response.IsSuccess)
                {
                    foreach (var domain in response.Data)
                    {
                        Console.WriteLine(domain);
                    }
                }
                else
                {
                    //log
                    Console.WriteLine("Error: " + response.ErrorMsg);
                }
               
                //Console.ReadLine(); //debug
            }
            catch (Exception ex)
            {
                //log
                Console.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true);
            }));
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                 .AddJsonFile("appsettings.json", false)
                 .Build();
            serviceCollection.AddSingleton<IConfigurationRoot>(configurationBuilder);
            serviceCollection.AddLogging()
            .AddSingleton<ITextClassifier, TextClassifier>()
            .AddSingleton<IJson2Rules, Json2Rules>()
            .AddSingleton<IPathClassifier, PathClassifier>()
            .AddSingleton<IExtractor, CsvExtractor>()
            .AddSingleton<IExtractor, TextExtractor>()
            .AddSingleton<IClassifier, Classifier>();
        }
    }
}

