using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Tbot.Configuration;
using Tbot.Utilities;

namespace Tbot.Services
{
    public class AudioFileHandler : IFileHandler
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramClient;

        public AudioFileHandler(AppSettings appSettings, ITelegramBotClient telegramClient)
        {
            _appSettings = appSettings;
            _telegramClient = telegramClient;

        }

        public async Task Download(string fileId, CancellationToken ct)
        {
            // Генерируем полный путь файла из конфигурации
            string inputAudioFilePath = Path.Combine(_appSettings.DownloadsFolder, $"{_appSettings.AudioFileName}.{_appSettings.InputAudioFormat}");
            using (FileStream destinationStream = File.Create(inputAudioFilePath))
            {
                //Загружаем информацию о файле
                var file = await
                    _telegramClient.GetFile(fileId, ct);
                if (file.FilePath == null)
                    return;

                //Скачиваем файл
                await
                    _telegramClient.DownloadFile(file.FilePath, destinationStream, ct);
            }
        }
        public string Process(string inputParam)
        {
            string inputAudioPath = Path.Combine(_appSettings.DownloadsFolder, $"{_appSettings.AudioFileName}.{_appSettings.InputAudioFormat}");
            string outputAudioPath = Path.Combine(_appSettings.DownloadsFolder, $"{_appSettings.AudioFileName}.{_appSettings.OutputAudioFormat}");
            Console.WriteLine("Начинаем конвертацию...");
            AudioConverter.TryConvert(inputAudioPath, outputAudioPath);
            Console.WriteLine("Файл  конвертирован");

            return "Конвертация успешно завершена";

        }

    }
}
