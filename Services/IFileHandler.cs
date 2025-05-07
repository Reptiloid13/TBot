using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tbot.Services;

public interface IFileHandler
{
    Task Download(string fileId, CancellationToken ct); // отвечает за первичное скачивание файлаа 
    public string Process(string param); // Метод обрабатывает файл (конвертирует и распознает)

}
