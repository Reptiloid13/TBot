using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using Tbot.Controllers;
using Tbot.Services;
using Tbot.Configuration;

namespace Tbot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            //Объект отвечающий за постоянный жизненный цикл приложения

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию 
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем
            Console.WriteLine("Сервис запущен");

            await

                host.RunAsync();
            Console.WriteLine("Сервис Остановлен");
        }
        public static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(appSettings);

            services.AddTransient<DefaultMessageController>();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddSingleton<IFileHandler, AudioFileHandler>();
            //Регистрируем объект TelegramBotClient с токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("7718435678:AAH4BrfLohZdv9lprSTzGOoaN5cUYPX4y7g"));
            //Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();


        }
        public static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                DownloadsFolder = "C:\\Users\\фвьшт\\Downloads\\Tbot\"",
                BotToken = "5353047760:AAECHVcGyM-cQJIfA4sCStnGDBPimhlIV-g", // откуда мы его взяли?
                AudioFileName = "audio",
                InputAudioFormat = "ogg",
            };
        }
    }
}
