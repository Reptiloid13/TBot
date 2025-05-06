using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tbot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Tbot.Controllers;

public class InlineKeyboardController

{
    private readonly IStorage _memoryStorage;
    private readonly ITelegramBotClient _telegramClient;

    public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage)
    {
        _telegramClient = telegramClient;
        _memoryStorage = memoryStorage;
    }

    public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
    {
        if (callbackQuery?.Data == null) return;

        // Обновление пользовательской сессии новыми данными

        _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;

        //Генерим информационное сообщение 

        string languageText = callbackQuery.Data switch
        {
            "ru" => " Русский",
            "en" => " Английский",
            _ => String.Empty
        }; // Что значит _

        // Отправляем в ответ уведомление о выборе
        await
            _telegramClient.SendMessage(callbackQuery.From.Id, $"<b> Язык аудио - {languageText}." +
            $"{Environment.NewLine}</b>" + $"{Environment.NewLine} Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
    }

}
