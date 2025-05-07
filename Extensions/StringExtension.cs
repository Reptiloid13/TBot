using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tbot.Extensions;

public class StringExtension
{
    //Преобразуем строку, чтобы она начиналась с заглавной буквы
    public static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s)) // почему тут нет else ? 
            return string.Empty;

        return char.ToUpper(s[0]) + s.Substring(1);
    }
}
