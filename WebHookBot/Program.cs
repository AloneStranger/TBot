using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace WebHookBot
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Введите ключ:");
            var key = Console.ReadLine();
            try
            {
                BotLibrary.Main.Bot = new TelegramBotClient(key); // инициализируем API
                Console.WriteLine("Бот запущен");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }

            // Endpoint musst be configured with netsh:
            // netsh http add urlacl url=https://+:8443/ user=<username>
            // netsh http add sslcert ipport=0.0.0.0:8443 certhash=<cert thumbprint> appid=<random guid>

            using (WebApp.Start<Startup>("https://+:8443"))
            {
                // Register WebHook
                BotLibrary.Main.Bot.SetWebhookAsync("https://YourHostname:8443/WebHook").Wait();

                Console.WriteLine("Server Started");

                // Stop Server after <Enter>
                Console.ReadLine();

                // Unregister WebHook
                BotLibrary.Main.Bot.SetWebhookAsync().Wait();
            }
        }
    }

    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            var configuration = new HttpConfiguration();

            configuration.Routes.MapHttpRoute("WebHook", "{controller}");

            appBuilder.UseWebApi(configuration);
        }
    }
    public class WebHookController : ApiController
    {
        public async Task<IHttpActionResult> Post(Update update)
        {

            return Ok();
        }

    }
}
