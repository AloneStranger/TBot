using System;

namespace BotLibrary
{
    /// <summary>
    /// Класс обрабатывающий CallBack запросы от кнопок
    /// </summary>
    public static class DoCallBackQuery
    {
        /// <summary>
        /// Обрабатывает CallBack запрос
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev">Объект содержащий запрос</param>
        public static async void Proceed(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            try
            {
                var message = ev.CallbackQuery.Message;
                switch (ev.CallbackQuery.Data)
                {
                    case "callback1": await Main.Bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Выбрано " + ev.CallbackQuery.Data, true); break;
                    case "callback2": await Main.Bot.SendTextMessageAsync(message.Chat.Id, "тест", replyToMessageId: message.MessageId); await Main.Bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id); break;
                    case "callback3": await Main.Bot.SendTextMessageAsync(message.Chat.Id, "тест2", replyToMessageId: message.MessageId); await Main.Bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id); break;
                    case "timerIngress": await Main.Bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id); DoMessage.SetTimer(ev.CallbackQuery.From.Id, 24, "Ingress"); break;
                    case "timerHorse": await Main.Bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id); DoMessage.SetTimer(ev.CallbackQuery.From.Id, 20, "TES"); break;
                }
            }
            catch { }
        }


    }
}
