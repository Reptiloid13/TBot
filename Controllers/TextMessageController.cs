using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace Tbot.Controllers;

public class TextMessageController
{
    private readonly ITelegramBotClient _telegramClient;

    public TextMessageController(ITelegramBotClient telegramClient)
    {
        _telegramClient = telegramClient;
    }

    public async Task Handle(Message message, CancellationToken ct)
    {
        switch (message.Text)
        {
            case "/start":

                //Объект предоставляющий кнопки
                var buttons = new List<InlineKeyboardButton[]>();// Почему тут квадратные кнопки массив?
                buttons.Add(new[] // создаем кнопку 
                {
                    InlineKeyboardButton.WithCallbackData($"Русский", $"ru"), // создаем кнопку
                    InlineKeyboardButton.WithCallbackData($"English", $"en") // создаемм кнопку 
                });
                //Передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                await
                    _telegramClient.SendMessage(message.Chat.Id, $"<b> Кристюша, этот   бот превращает аудио в текст." +
                    $"</b> {Environment.NewLine}" + $"{Environment.NewLine} Можешь  записать  свои голосовые сообщения и переслать их сюда, если лень печатать. {Environment.NewLine}", cancellationToken: ct,
                    parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                break;
            default:
                await
                    _telegramClient.SendMessage(message.Chat.Id, "Отправьте аудио для превращения в текст.", cancellationToken: ct);
                break;

        }
    }
}
