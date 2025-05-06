using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Tbot.Controllers;

public class VoiceMessageController
{
    private readonly ITelegramBotClient _telegramClient;

    public VoiceMessageController(ITelegramBotClient telegramClient)
    {
        _telegramClient = telegramClient;
    }

    public async Task Handle(Message message, CancellationToken ct)
    {
        Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");

        await _telegramClient.SendMessage(message.Chat.Id, $"Получил текстовое сообщение", cancellationToken: ct);
    }
}
