using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tbot.Models;

namespace Tbot.Services;

public class MemoryStorage : IStorage
{
    //Хранилище сессий
    private readonly ConcurrentDictionary<long, Session> _sessions;
    public MemoryStorage()
    {
        _sessions = new ConcurrentDictionary<long, Session>();
    }
    public Session GetSession(long chatId)
    {
        //Возвращаем сессию по ключу если она существует 
        if (_sessions.ContainsKey(chatId))
            return _sessions[chatId];

        // Создаем и возвращаем новую, если такой не было
        var newSession = new Session() { LanguageCode = "Ru" }; // тут было ru
        _sessions.TryAdd(chatId, newSession);
        return newSession;
    }
}
