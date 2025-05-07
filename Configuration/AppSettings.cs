using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tbot.Configuration;

public class AppSettings
{
    // Токен Telegram APi
    public string BotToken { get; set; }

    // папка загрузки аудио файлов
    public string DownloadsFolder { get; set; }


    // Имя файла при загрузке
    public string AudioFileName { get; set; }

    // Формат аудио при загрузке
    public string InputAudioFormat { get; set; }

}
