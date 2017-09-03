using BotLibrary;
using System;
using System.ServiceProcess;
using System.Threading;
using Telegram.Bot;

namespace TBotService
{
    public partial class TBotSrv : ServiceBase
    {
        private string _key = "";
        private bool Working = false;
        public TBotSrv(string key)
        {
            InitializeComponent();
            _key = key;
        }

        protected override void OnStart(string[] args)
        {
            if (String.IsNullOrEmpty(_key))
                return;

            try
            {
                Main.Bot = new TelegramBotClient(_key); // инициализируем API
            }
            catch
            {
                return;
            }

            Working = true;
            
            //Стартуем бота
            DoWork();

            while (Working)
                Thread.Sleep(100);
        }



        protected override void OnStop()
        {
            Working = false;
        }

        /// <summary>
        /// Задаем методы обрабатывающие события бота и стартуем
        /// </summary>
        static async void DoWork()
        {
            try
            {
                await Main.Bot.SetWebhookAsync("");

                Main.Bot.OnInlineQuery += DoInlineQuery.Proceed;
                Main.Bot.OnCallbackQuery += DoCallBackQuery.Proceed;
                Main.Bot.OnUpdate += DoMessage.Proceed;
                Main.Bot.StartReceiving();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
