using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tbot.Configuration;
using Tbot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Tbot.Controllers;

public class VoiceMessageController
{
    //private readonly AppSettings _appSettings;
    private readonly ITelegramBotClient _telegramClient;
    private readonly IFileHandler _audioFileHandler;
    private readonly IStorage _memoryStorage; // Добавили вместо addsettings

    public VoiceMessageController(IStorage memoryStorage, ITelegramBotClient telegramClient, IFileHandler audioFileHandler)
    {
        //_appSettings = appSettings;
        _telegramClient = telegramClient;
        _audioFileHandler = audioFileHandler;
        _memoryStorage = memoryStorage;

    }

    public async Task Handle(Message message, CancellationToken ct)
    {
        var fileId = message.Voice?.FileId;
        if (fileId == null)
            return;

        await _audioFileHandler.Download(fileId, ct);

        await _telegramClient.SendMessage(message.Chat.Id, $"Голосовое сообщение загружено", cancellationToken: ct);

        string userLanguageCode = _memoryStorage.GetSession(message.Chat.Id).LanguageCode; //Здесь получим язык из сессии пользователя

        _audioFileHandler.Process(userLanguageCode); // запустим обработку

        await _telegramClient.SendMessage(message.Chat.Id, "Голосовое сообщение конвертировано в формат .WAV", canellationToken: ct);

    }
}
