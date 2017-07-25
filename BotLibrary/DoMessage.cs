using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using File = System.IO.File;

namespace BotLibrary
{
    /// <summary>
    /// Обрабатывает сообщения
    /// </summary>
    public static class DoMessage
    {
        //Путь для сохранения файлов
        const string path = @"D:\TB\";

        /// <summary>
        /// Обрабатывает сообщения
        /// </summary>
        /// <param name="su"></param>
        /// <param name="evu">Объект содержащий сообщение</param>
        public static void Proceed(object su, Telegram.Bot.Args.UpdateEventArgs evu)
        {
            //Проверка типа объекта. Если не сообщение - выход
            if (evu.Update.CallbackQuery != null || evu.Update.InlineQuery != null) return;

            var message = evu.Update.Message;
            if (message == null) return; 

            Console.WriteLine(message.Type);

            //Проверка типа сообщения и выбор процедуры обработки
            switch (message.Type)
            {
                case MessageType.AudioMessage: Audio(message); break;
                case MessageType.ContactMessage: Contact(message); break;
                case MessageType.DocumentMessage: Document(message); break;
                case MessageType.LocationMessage: Location(message); break;
                case MessageType.PhotoMessage: Photo(message); break;
                case MessageType.ServiceMessage: Service(message); break;
                case MessageType.StickerMessage: Sticker(message); break;
                case MessageType.TextMessage: Text(message); break;
                case MessageType.UnknownMessage: break;
                case MessageType.VenueMessage: Venue(message); break;
                case MessageType.VideoMessage: Video(message); break;
                case MessageType.VoiceMessage: Voice(message); break;
            }
        }

        /// <summary>
        /// Сохраняет на диск файл из сообщения
        /// </summary>
        /// <param name="FileID">Идентификатор файла</param>
        async static void SaveFile(string FileID)
        {
            var file = await Main.Bot.GetFileAsync(FileID);

            if (file.FileStream.Length != 0)
                try
                {
                    using (var fileStream = File.Create(String.Format(@"{0}\{1}{2}", path, FileID, file.FilePath.Contains(".") ? file.FilePath.Remove(0, file.FilePath.LastIndexOf(".")) : "")))
                        await file.FileStream.CopyToAsync(fileStream);
                }
                catch (Exception e) { Console.WriteLine(String.Format("Ошибка: {0}", e.Message)); }
            Console.WriteLine(String.Format("Успешно сохранили файл: {0}", file.FilePath));
        }

        /// <summary>
        /// Обрабатывает аудио сообщение
        /// </summary>
        /// <param name="message">Обрабатываемое сообщение</param>
        async static void Audio(Message message)
        {
            var a = message.Audio;
            if (a == null)
            {
                await Main.Bot.SendTextMessageAsync(message.Chat.Id, "Непонятный формат сообщения", replyToMessageId: message.MessageId);
                return;
            }

            Console.WriteLine(String.Format("Получена музыка длительностью {0} сек", a.Duration));
            await Main.Bot.SendTextMessageAsync(message.Chat.Id, String.Format("Послушаем {0} сек", a.Duration), replyToMessageId: message.MessageId);

            SaveFile(a.FileId);
        }

        /// <summary>
        /// Обрабатывает сообщение-контакт
        /// </summary>
        /// <param name="message">Обрабатываемое сообщение</param>
        static void Contact(Message message)
        {
            //await Main.Bot.SendTextMessageAsync(message.Chat.Id, "Запомню контактик", replyToMessageId: message.MessageId);
        }

        /// <summary>
        /// Обрабатывает документ
        /// </summary>
        /// <param name="message">Обрабатываемое сообщение</param>
        async static void Document(Message message)
        {
            var d = message.Document;
            if (d == null)
            {
                await Main.Bot.SendTextMessageAsync(message.Chat.Id, "Непонятный формат сообщения", replyToMessageId: message.MessageId);
                return;
            }

            Console.WriteLine(String.Format("Получен документ {0}", d.FileName));
            await Main.Bot.SendTextMessageAsync(message.Chat.Id, "Сохраню документик", replyToMessageId: message.MessageId);

            SaveFile(d.FileId);
        }

        /// <summary>
        /// Обрабатывает гео-локацию
        /// </summary>
        /// <param name="message">Обрабатываемое сообщение</param>
        async static void Location(Message message)
        {
            var l = message.Location;
            Console.WriteLine(String.Format("{0}; {1}", l.Latitude, l.Longitude));
            await Main.Bot.SendTextMessageAsync(message.Chat.Id, String.Format("Съездим в http://maps.yandex.ru/?ll={0}%2C{1}", l.Latitude, l.Longitude), replyToMessageId: message.MessageId);
        }

        /// <summary>
        /// Обрабатывает фото
        /// </summary>
        /// <param name="message">Обрабатываемое сообщение</param>
        async static void Photo(Message message)
        {
            var p = message.Photo;
            if (p == null)
            {
                await Main.Bot.SendTextMessageAsync(message.Chat.Id, "Непонятный формат сообщения", replyToMessageId: message.MessageId);
                return;
            }

            Console.WriteLine("Получено фото");
            await Main.Bot.SendTextMessageAsync(message.Chat.Id, "Красивое фото", replyToMessageId: message.MessageId);

            SaveFile(p[p.Length - 1].FileId);
        }

        /// <summary>
        /// Обрабатывает служебное сообщение
        /// </summary>
        /// <param name="message">Обрабатываемое сообщение</param>
        static void Service(Message message)
        {
            //await Main.Bot.SendTextMessageAsync(message.Chat.Id, "...", replyToMessageId: message.MessageId);
        }

        /// <summary>
        /// Обрабатывает стикеры
        /// </summary>
        /// <param name="message">Обрабатываемое сообщение</param>
        static void Sticker(Message message)
        {
            //await Main.Bot.SendTextMessageAsync(message.Chat.Id, "Сохраню документик", replyToMessageId: message.MessageId);
        }

        /// <summary>
        /// Обрабатывает текстовые сообщения, содержащие команды полученные ботом
        /// </summary>
        /// <param name="command">Сообщение, содержащее команду</param>
        async static void Command(Message command)
        {
            //Пример работы с inline кнопками
            if (command.Text.ToLower() == "/tes2")
            {
                var keyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(
                    new InlineKeyboardButton[][]
                    {
                        new [] {
                            InlineKeyboardButton.WithCallbackData("1", "callback1"),
                            InlineKeyboardButton.WithCallbackData("2", "callback2"),
                        },
                        new [] {
                            InlineKeyboardButton.WithCallbackData("3", "callback3"),
                        },

                    }
                );

                await Main.Bot.SendTextMessageAsync(command.Chat.Id, "Выбери число!", replyMarkup: keyboard);
                return;
            }

            //Пример работы с reply кнопками
            if (command.Text.ToLower() == "/test1")
            {
                var keyboard = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                {
                    Keyboard = new[] {
                        new[]{
                            new KeyboardButton("Да"),
                            new KeyboardButton("Нет")
                        },
                    },
                    ResizeKeyboard = true
                };

                await Main.Bot.SendTextMessageAsync(command.Chat.Id, "Посчитаем?", replyMarkup: keyboard);
            }

            if (command.Text.ToLower() == "/timer")
            {
                var keyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(
                    new InlineKeyboardButton[][]
                    {
                        new [] {
                            InlineKeyboardButton.WithCallbackData("Ingress", "timerIngress"),
                        },
                        new [] {
                            InlineKeyboardButton.WithCallbackData("TES Horse", "timerHorse"),
                        },

                    }
                );

                await Main.Bot.SendTextMessageAsync(command.Chat.Id, "Установи таймер", replyMarkup:keyboard);
                return;
            }

            if (command.Text.ToLower().StartsWith("/timer"))
            {
                DoTimer(command);
                return;
            }
        }

        static void DoTimer(Message command)
        {
            SetTimer(command.From.Id, 1, "Test");
        }

        public static async void SetTimer(int UId, int Hours, string Description)
        {
            if (!DBWork.CheckRight(UId, "timer"))
            {
                await Main.Bot.SendTextMessageAsync(UId, "Недостаточно прав на установку таймера");
                return;
            }

            if (!DBWork.SetTimer(UId, Hours, Description))
            {
                await Main.Bot.SendTextMessageAsync(UId, "Не удалось установить таймер");
                return;
            }

            await Main.Bot.SendTextMessageAsync(UId, String.Format("{0} {1}: <i>{2}</i>", DBWork.GetTimers(UId), Description, DateTime.Now.AddHours(Hours)), parseMode: ParseMode.Html);
        }

        /// <summary>
        /// Обрабатывает текстовые сообщения
        /// </summary>
        /// <param name="message">Обрабатываемое сообщение</param>
        static void Text(Message message)
        {
            Console.WriteLine(message.Text);
            if (message.Text.StartsWith("/"))
            {
                Command(message);
                return;
            }

            //await Bot.SendTextMessageAsync(message.Chat.Id, message.Text, replyToMessageId: message.MessageId);
        }

        static void Venue(Message message)
        {
            //await Main.Bot.SendTextMessageAsync(message.Chat.Id, "...", replyToMessageId: message.MessageId);
        }

        /// <summary>
        /// Обрабатывает сообщения с видео
        /// </summary>
        /// <param name="message">Обрабатываемое сообщение</param>
        async static void Video(Message message)
        {
            var v = message.Video;
            if (v == null)
            {
                await Main.Bot.SendTextMessageAsync(message.Chat.Id, "Непонятный формат сообщения", replyToMessageId: message.MessageId);
                return;
            }
            Console.WriteLine(String.Format("Получено видео {0} сек", v.Duration));

            await Main.Bot.SendTextMessageAsync(message.Chat.Id, String.Format("Посмотрим {0} сек", v.Duration), replyToMessageId: message.MessageId);

            SaveFile(v.FileId);
        }

        /// <summary>
        /// Обрабатывает голосовые сообщения
        /// </summary>
        /// <param name="message">Обрабатываемое сообщение</param>
        async static void Voice(Message message)
        {
            var v = message.Voice;
            if (v == null)
            {
                await Main.Bot.SendTextMessageAsync(message.Chat.Id, "Непонятный формат сообщения", replyToMessageId: message.MessageId);
                return;
            }

            Console.WriteLine(String.Format("Получено голосовое сообщение длительностью {0}", v.Duration));
            await Main.Bot.SendTextMessageAsync(message.Chat.Id, String.Format("Послушаем {0} сек", v.Duration), replyToMessageId: message.MessageId);

            SaveFile(v.FileId);
        }


    }
}
