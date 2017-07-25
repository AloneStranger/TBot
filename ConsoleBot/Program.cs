using System;
using BotLibrary;
using Telegram.Bot;

namespace ConsoleBot
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

            //Стартуем бота
            DoWork();

            //Ждем Enter для завершения работы
            Console.ReadLine();
        }

        /// <summary>
        /// Задаем методы обрабатывающие события бота и стартуем
        /// </summary>
        static async void DoWork()
        {
            try
            {
                await BotLibrary.Main.Bot.SetWebhookAsync("");

                BotLibrary.Main.Bot.OnInlineQuery += DoInlineQuery.Proceed;
                BotLibrary.Main.Bot.OnCallbackQuery += DoCallBackQuery.Proceed;
                BotLibrary.Main.Bot.OnUpdate += DoMessage.Proceed;
                BotLibrary.Main.Bot.StartReceiving();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
