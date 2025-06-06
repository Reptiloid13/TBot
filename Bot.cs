﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Tbot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Tbot;

public class Bot : BackgroundService
{
    //Клиент к телеграм Бот APi
    private ITelegramBotClient _telegramClient;
    private DefaultMessageController _defaultMessageController;
    private InlineKeyboardController _inlineKeyboardController;
    private TextMessageController _textMessageController;
    private VoiceMessageController _voiceMessageController;

    public Bot(ITelegramBotClient telegramClient,
        InlineKeyboardController inlineKeyboardController,
        TextMessageController textMessageController,
        VoiceMessageController voiceMessageController,
        DefaultMessageController defaultMessageController)
    {
        _telegramClient = telegramClient;
        _inlineKeyboardController = inlineKeyboardController;
        _textMessageController = textMessageController;
        _voiceMessageController = voiceMessageController;
        _defaultMessageController = defaultMessageController;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _telegramClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions() { AllowedUpdates = { } },
            // Здесь выбираем, какие обновления хотим получать В данном случае разрешены все
            cancellationToken: stoppingToken);

        Console.WriteLine("Бот запущен");


    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        //  Обрабатываем нажатия на кнопки  из Telegram Bot API: https://core.telegram.org/bots/api#callbackquery
        if (update.Type == UpdateType.CallbackQuery)
        {

            await
                _inlineKeyboardController.Handle(update.CallbackQuery, cancellationToken);
        }
        if (update.Type == UpdateType.Message)
        {
            switch (update.Message!.Type)
            {
                case MessageType.Voice:
                    await
                        _voiceMessageController.Handle(update.Message, cancellationToken);
                    return;

                case MessageType.Text:
                    await
                        _textMessageController.Handle(update.Message, cancellationToken);
                    return;

                default:
                    await
                        _defaultMessageController.Handle(update.Message, cancellationToken);

                    return;
            }


        }
    }

    public async Task HandleErrorAsync(ITelegramBotClient botClietnt, Exception exception, CancellationToken cancellationToken)
    {
        //Задаем сообщение об ошибке в зависимости от того, какая именно ошибка произошла

        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}] \n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        //выводим в консоль сообщение об ошибке
        Console.WriteLine(errorMessage);

        //Задержка перед повторным подключением
        Console.WriteLine("Ожидаем 10 секунд перед повторным подключением");
        //Thread.Sleep(10000);
        await Task.Delay(10000, cancellationToken);

        //return Task.CompletedTask;
    }
}

