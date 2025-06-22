using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Tbot.Extensions;
using Vosk;
using Newtonsoft.Json.Linq;



namespace Tbot.Utilities;

public class SpeechDetector
{
    public static string DetectSpeech(string audioPath, float inputBitrate, string langeageCode)
    {
        Vosk.Vosk.SetLogLevel(0);
        var modelPath = Path.Combine(DirectoryExtension.GetSolutionRoot(), "Speech-models", $"vosk-model-small-{langeageCode.ToLower()}");
        Model model = new(modelPath);
        return GetWords(model, audioPath, inputBitrate);
    }

    //Основной метод для распознавания слов
    private static string GetWords(Model model, string audioPath, float inputBitrate)
    {
        // В конструтор для распознавания передаем битрейт, а также используемую языковую модель

        VoskRecognizer rec = new(model, inputBitrate);//TODO Здесь ошибка  
        rec.SetMaxAlternatives(0);
        rec.SetWords(true);

        StringBuilder textbuffer = new();

        using (Stream source = File.OpenRead(audioPath))
        {
            byte[] buffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                // Распознование отдельных слова
                if (rec.AcceptWaveform(buffer, bytesRead))
                {
                    var sentenceJson = rec.Result();
                    // Сохраняем текстовый вывод в Json - объект и извлекаем данные
                    JObject sentencObj = JObject.Parse(sentenceJson);
                    string sentence = (string)sentencObj["text"];
                    textbuffer.Append(StringExtension.UppercaseFirst(sentence) + ".");
                }
            }
        }

        // Распознавание предложений
        var finalSentence = rec.FinalResult();

        //Сохраняем текстовый вывод в JSON -  объект и извлекаем данные 

        JObject finalSentenceObj = JObject.Parse(finalSentence);

        //Собираем итоговый текст

        textbuffer.Append((string)finalSentenceObj["text"]);

        // Возвращаем в виде строки 

        return textbuffer.ToString();
    }

}
