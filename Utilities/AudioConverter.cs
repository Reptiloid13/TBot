using FFMpegCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tbot.Extensions;
using Telegram.Bot.Types;

namespace Tbot.Utilities;

public static class AudioConverter
{
    public static void TryConvert(string inputFile, string outputFile)
    {
        //Задаем путь, где лежит вспомогательная программа = конвертер
        GlobalFFOptions.Configure(options => options.BinaryFolder = Path.Combine(DirectoryExtension.GetSolutionRoot(), "ffmpegcore", "bin"));



        //вызываем Ffmpeg переда требуемые аргументы 

        FFMpegArguments.FromFileInput(inputFile)
            .OutputToFile(outputFile, true, options => options.WithFastStart())
        .ProcessSynchronously();


    }
    private static string GetSolutionRoot()
    {
        var dir = Path.GetDirectoryName(Directory.GetCurrentDirectory());
        var fullname = Directory.GetParent(dir).FullName;
        var projectRoot = fullname.Substring(0, fullname.Length - 4);
        return Directory.GetParent(projectRoot)?.FullName;
    }
}
