using FFMpegCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tbot.Extensions;

namespace Tbot.Utilities;

public static class AudioConverter
{
    public static void TryConvert(string inputFile, string outputFile)
    {
        //Задаем путь, где лежит вспомогательная программа = конвертер
        GlobalFFOptions.Configure(static options => options.BinaryFolder = Path.Combine(DirectoryExtension.GetSolutionRoot(), "ffmpegcore", "bin")); // Где то тут ошибка 


        //вызываем Ffmpeg переда требуемые аргументы 

        FFMpegArguments.FromFileInput(inputFile)
            .OutputToFile(outputFile, true, options => options.WithFastStart())
        .ProcessSynchronously();
    }
}
