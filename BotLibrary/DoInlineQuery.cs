using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace BotLibrary
{
    /// <summary>
    /// Класс обрабатывающий Inline запросы
    /// </summary>
    public static class DoInlineQuery
    {
        /// <summary>
        /// Обрабатывает Inline запрос
        /// </summary>
        /// <param name="si"></param>
        /// <param name="ei">Объект содержащий запрос</param>
        public static async void Proceed(object si, Telegram.Bot.Args.InlineQueryEventArgs ei)
        {
            //Получаем текст запроса
            var query = ei.InlineQuery.Query;

            //Создаем список ответов на запрос
            List<Telegram.Bot.Types.InlineQueryResults.InlineQueryResult> lst = new List<Telegram.Bot.Types.InlineQueryResults.InlineQueryResult>();

            if (query.Contains("1")) //Если запрос содержит 1 - добавляем текстовое сообщение
            {
                var msg = new Telegram.Bot.Types.InputMessageContents.InputTextMessageContent //Тело сообщения
                {
                    MessageText =
@"Это супер крутой текст статьи
с переносами
и <b>html</b> <i>тегами!</i>",
                    ParseMode = ParseMode.Html, //Тип - html
                };
                lst.Add(new Telegram.Bot.Types.InlineQueryResults.InlineQueryResultArticle
                {
                    Id = "1",
                    Title = "Тестовый тайтл",
                    Description = "Описание статьи тут",
                    InputMessageContent = msg,
                });
            }

            if (query.Contains("2"))
            {
                lst.Add(new Telegram.Bot.Types.InlineQueryResults.InlineQueryResultAudio
                {
                    Url = "http://aftamat4ik.ru/wp-content/uploads/2017/05/mongol-shuudan_-_kozyr-nash-mandat.mp3",
                    Id = "2",
                    FileId = "123423433",
                    Title = "Монгол Шуудан - Козырь наш Мандат!",
                });
            }
            /*
            if (query.Contains("3"))
            {
                lst.Add(new Telegram.Bot.Types.InlineQueryResults.InlineQueryResultPhoto
                {
                    Id = "3",
                    Url = "http://aftamat4ik.ru/wp-content/uploads/2017/05/14277366494961.jpg",
                    //ThumbUrl = "",
                    Caption = "Текст под фоткой",
                    Description = "Описание",
                });
            }
            */
            if (query.Contains("3"))
            {
                lst.Add(new Telegram.Bot.Types.InlineQueryResults.InlineQueryResultVideo
                {
                    Id = "3",
                    Url = "http://aftamat4ik.ru/wp-content/uploads/2017/05/bb.mp4",
                    ThumbUrl = "http://aftamat4ik.ru/wp-content/uploads/2017/05/joker_5-150x150.jpg",
                    Title = "демо видеоролика",
                    MimeType = "video/mp4",
                });
            }

            try
            {
                await Main.Bot.AnswerInlineQueryAsync(ei.InlineQuery.Id, lst.ToArray());
            }
            catch { }
        }
    }
}
