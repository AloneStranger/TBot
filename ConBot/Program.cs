using System;
using System.Text;
using Telegram.Bot;

namespace Awesome
{
    class Program
    {
        static void Main()
        {
            Console.Write("Code here: ");
            string code = Console.ReadLine();
            var bc = new TelegramBotClient(code);
            Telegram.Bot.Types.User me;
            try
            {
                Console.WriteLine($"Try start bot {bc.BotId}.");
                me = bc.GetMeAsync().Result;
                Console.WriteLine($"I am user {me.Id} and my name is {me.FirstName}.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {GetErrors(e)}");
            }
            
            Console.ReadLine();
        }

        static string GetErrors(Exception e)
        {
            StringBuilder sb = new StringBuilder();
            while (e != null)
            {
                sb.AppendLine(e.Message);
                e = e.InnerException;
            }

            return sb.ToString();
        }
    }
}