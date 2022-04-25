using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace XproBot
{

    public class TelegramBotConnect
    {
        private TelegramBotClient botClient = new TelegramBotClient("2024013779:AAESkS56r1F9GUtmwdPK_f3vTVMmvbvkdJU");
        private CancellationTokenSource cts = new CancellationTokenSource();
        public String id, username;
        //public static String[] messages;
        public static String messages;
        public static Boolean loopMsgCondition;
        public static int sleep;
        public static Scraper scraper;
        public static Dictionary<long, String> userData = new Dictionary<long, String>();

        public TelegramBotConnect(int sleep = 50000)
        {
            TelegramBotConnect.sleep = sleep;
            var t = Task.Run(() => telegramRequestAsync());
            t.Wait();
        }

        public void telegramRequest()
        {
            var me = botClient.GetMeAsync();
            id = me.Id.ToString();
            username = me.Result.FirstName;
        }

        public async Task telegramRequestAsync()
        {
            var me = await botClient.GetMeAsync();
            id = me.Id.ToString();
            username = me.Username;
        }

        public void startBotWork(Scraper scraper)
        {
            //telegramRequestAsync();
            TelegramBotConnect.scraper = scraper;
            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            botClient.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token);
            Trace.WriteLine($"Start listening for @{username} ");
        }

        public void stopBotWork()
        {
            cts.Cancel();
            Trace.WriteLine("Stop Sending Data");
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Trace.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }


        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Type != UpdateType.Message)
                return;
            if (update.Message.Type != MessageType.Text)
                return;

            var updateId = update.Id;

            Trace.WriteLine($"Update Id: {updateId}.");

            var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { "/start" },
                    new KeyboardButton[] { "/stop" }
                     //Send Data 💻🤖
                })
            { ResizeKeyboard = true };

            while (true) {

                var chatUpdates = await botClient.GetUpdatesAsync(offset: updateId);

                foreach (var chatUpdate in chatUpdates)
                {
                    String? msg = chatUpdate?.Message?.Text;
                    if (msg == "/start")
                    {
                        if (!userData.ContainsKey(chatUpdate.Message.Chat.Id))
                            userData.Add(chatUpdate.Message.Chat.Id, chatUpdate.Message.Chat.FirstName);
                    }
                    if (msg == "/stop")
                    {
                        if (userData.ContainsKey(chatUpdate.Message.Chat.Id))
                            userData.Remove(chatUpdate.Message.Chat.Id);
                    }
                }
                if (userData != null)
                {
                    messages = scraper.getMsgsText();

                    foreach (var item in userData)
                    {
                        Trace.Write($"Received by {item.Value} with chat Id : {item.Key}");
                        await botClient.SendTextMessageAsync(
                                    chatId: item.Key,
                                    text: messages,
                                    replyMarkup: replyKeyboardMarkup
                                );
                        //foreach (string msg in messages)
                        //{
                        //    try
                        //    {
                        //        await botClient.SendTextMessageAsync(
                        //            chatId: item.Key,
                        //            text: msg,
                        //            replyMarkup: replyKeyboardMarkup
                        //        );
                        //        //Thread.Sleep(2000);
                        //    }
                        //    catch(Exception ex)
                        //    {
                        //        Trace.Write($"\n Error : {ex} \n");
                        //        break;
                        //    }
                        //}
                        Trace.Write("/start right now \n");
                    }
                    try
                    {
                        scraper.refreshSite();
                    }
                    catch (Exception ex)
                    {
                        stopBotWork();
                    }
                }
                updateId++;
                Thread.Sleep(sleep);
            }

            //if (update.Message.Text.Contains("/start"))
            //{
            //    loopMsgCondition = true;
            //}
            //if (update.Message.Text.Contains("/stop"))
            //{
            //    loopMsgCondition = false;
            //}

            //if (loopMsgCondition)
            //{
            //    foreach (string msg in messages)
            //    {
            //        await botClient.SendTextMessageAsync(
            //            chatId: chatId,
            //            text: msg,
            //            replyMarkup: replyKeyboardMarkup
            //        );
            //        Thread.Sleep(2000);
            //    }
            //    Trace.Write("/start right now");
            //    stopBotWork();
            //}
            //if (!loopMsgCondition)
            //{
            //    Trace.Write("/stop right now");
            //    stopBotWork();
            //}
        }

        //async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        //{

        //    var handler = update.Type switch
        //    {
        //        // UpdateType.Unknown:
        //        // UpdateType.ChannelPost:
        //        // UpdateType.EditedChannelPost:
        //        // UpdateType.ShippingQuery:
        //        // UpdateType.PreCheckoutQuery:
        //        // UpdateType.Poll:
        //        UpdateType.Message => BotOnMessageReceivedLoop(botClient, update.Message, messages),
        //        UpdateType.EditedMessage => BotOnMessageReceived(botClient, update.EditedMessage),
        //        _ => UnknownUpdateHandlerAsync(botClient, update)
        //    };

        //    try
        //    {
        //        await handler;
        //    }
        //    catch (Exception exception)
        //    {
        //        await HandleErrorAsync(botClient, exception, cancellationToken);
        //    }
        //}

    //    private static async Task BotOnMessageReceivedLoop(ITelegramBotClient botClient, Message message, String[] messagesData)
    //    {
    //        Trace.WriteLine($"Receive message type:{message.Type}");
    //        if (message.Type != MessageType.Text)
    //            return;

    //        if (message.Text == "/start")
    //        {
    //            loopMsgCondition = true;
    //        }
    //        if (message.Text == "/stop")
    //        {
    //            loopMsgCondition = false;
    //        }
    //        if (loopMsgCondition)
    //        {
    //            SendingDataMessages(botClient, message, messagesData);
    //        }
    //        if (loopMsgCondition == false)
    //        {
    //            StopSendingDataMessages(botClient, message);
    //        }


    //        static async Task SendingDataMessages(ITelegramBotClient botClient, Message message, String[] messagesData)
    //        {
    //            var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
    //                {
    //                new KeyboardButton[] { "/start" },
    //                new KeyboardButton[] { "/stop" }
    //                 //Send Data 💻🤖
    //            })
    //            { ResizeKeyboard = true };

    //            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
    //                                                        text: "💻🤖 Start Sending Data !!!",
    //                                                        replyMarkup: replyKeyboardMarkup);
    //            foreach (string msg in messagesData)
    //            {
    //                Thread.Sleep(2000);
    //                await botClient.SendTextMessageAsync(
    //                    chatId: message.Chat.Id,
    //                    text: msg
    //                );
    //            }
    //        }

    //        static async Task StopSendingDataMessages(ITelegramBotClient botClient, Message message)
    //        {
    //            var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
    //                {
    //                new KeyboardButton[] { "/start" },
    //                new KeyboardButton[] { "/stop" }
    //                 //Send Data 💻🤖
    //            })
    //            { ResizeKeyboard = true };

    //            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
    //                                                        text: "💻🤖 Stop Sending Data !!!",
    //                                                        replyMarkup: replyKeyboardMarkup);
    //        }

    //    }


    //    ///standart method
    //    private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
    //    {
    //        Trace.WriteLine($"Receive message type:{message.Type}");
    //        if (message.Type != MessageType.Text)
    //            return;
    //        var action = (message.Text.Split(' ').First()) switch
    //        {
    //            "/start" => SendingDataMessages(botClient, message, messages),
    //            "/stop" => StopSendingDataMessages(botClient, message)
    //        };
    //        var sentMessage = await action;
    //        foreach (string msg in messages)
    //        {
    //            await botClient.SendTextMessageAsync(
    //                chatId: message.Chat.Id,
    //                text: msg
    //            );
    //            Thread.Sleep(2000);
    //        }
    //        Trace.Write("/start right now");
    //        messages = null;
    //        Trace.WriteLine($"The message was sent with id: {sentMessage.MessageId}");

    //        ////message method
    //        static async Task<Message> SendingDataMessages(ITelegramBotClient botClient, Message message, String[] messages)
    //        {
    //            var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
    //                {
    //                new KeyboardButton[] { "/start" },
    //                 new KeyboardButton[] { "/stop" }
    //                 //Send Data 💻🤖
    //            })
    //            { ResizeKeyboard = true };

    //            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
    //                                                        text: "💻🤖 Start Sending Data !!!",
    //                                                        replyMarkup: replyKeyboardMarkup);

    //            foreach (string msg in messages)
    //            {
    //                Thread.Sleep(2000);
    //                await botClient.SendTextMessageAsync(
    //                    chatId: message.Chat.Id,
    //                    text: msg
    //                );
    //            }
    //            Trace.Write("/start right now");

    //            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
    //                                                        text: "/start",
    //                                                        replyMarkup: replyKeyboardMarkup);
    //        }

    //        static async Task<Message> StopSendingDataMessages(ITelegramBotClient botClient, Message message)
    //        {
    //            var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
    //                {
    //                new KeyboardButton[] { "/start" },
    //                 new KeyboardButton[] { "/stop" }
    //                 //Send Data 💻🤖
    //            })
    //            { ResizeKeyboard = true };

    //            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
    //                                                        text: "💻🤖 Stop Sending Data !!!",
    //                                                        replyMarkup: replyKeyboardMarkup);
    //        }
    //    }

    //    private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
    //    {
    //        Trace.WriteLine($"Unknown update type: {update.Type}");
    //        return Task.CompletedTask;
    //    }
    }
}
