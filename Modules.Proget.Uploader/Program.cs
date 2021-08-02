using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Proget.Uploader.ActionHandlers;
using Modules.Proget.Uploader.CommandLine;
using Modules.Proget.Uploader.Exceptions;
using Polly;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Toolset.CommandLine;
using Toolset.Core;
using Toolset.Http;
using Toolset.Http.ProGetHome;
using Toolset.Http.ProGetHome.AssetDirectory.Get;

namespace Modules.Proget.Uploader
{
    class Program
    {
        private static IConfiguration _configuration;
        private static Action<DelegateResult<HttpResponseMessage>, TimeSpan> ActionOnRetry =>
                              (ex, timeSpan) =>
                              {
                                  Console.WriteLine($"Upload failed: [{MessagePreparer.Prepare(ex)}]");
                                  Console.WriteLine($"Retrying in {timeSpan.Seconds} seconds");
                              };

        static async Task<int> Main(string[] args)
        {
            var returnValue = 0;
            try
            {
                var serviceCollection = new ServiceCollection();

                _configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                   .AddJsonFile("appsettings.json", false)
                   .Build();

                serviceCollection.AddSingleton(_configuration);

                var sp = serviceCollection
                    .AddToolset()
                    .AddCommandLineParser<CommandLineArgs>(args, ParserErrorHandler.Run, true)
                    .AddHttpClient()
                    .AddProGetHome(onRetry: ActionOnRetry)
                    .Build();

                var standardOutput = new StreamWriter(Console.OpenStandardOutput())
                {
                    AutoFlush = true
                };

                Console.SetOut(standardOutput);

                //var opts = sp.GetRequiredService<ICommandLineData>();
                //var fileUploader = new FileUploader(sp, opts.Get<CommandLineArgs>(), standardOutput);

                //return await fileUploader.UpoadAsync().ConfigureAwait(false);

                var downloader = sp.GetRequiredService<IDownloadAssetEndpoint>();
                var data_1 = await downloader.Downloald("Modules/Aptiv/TAB_019_538_KSK_LHD.xml");

                File.WriteAllBytes(@"c:\temp\TAB_019_538_KSK_LHD.xml", data_1);

                return 0;

            }
            catch (HelpOrVersionRequestExcepion)
            {
                returnValue = -1;
            }
            catch (ParserErrorExcepion ex)
            {
                Console.WriteLine(ex.Message);
                returnValue = -2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnValue = -3;
            }

            Console.ReadKey();

            return returnValue;
        }
    }
}
