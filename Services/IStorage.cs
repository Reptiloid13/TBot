using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tbot.Models;

namespace Tbot.Services;

public interface IStorage
{
    //Получение сессии пользователя по идентификатору
    Session GetSession(long chatId);
}
