using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelkomCareBot
{
    class TelegramBotConnect
    {
        private TelegramBotClient botClient = new TelegramBotClient("2061979630:AAGTFVkbk9h38VygH9W1CzbMvcf3Rg3ezZE");
        private CancellationTokenSource cts = new CancellationTokenSource();
        public String id, username;
        public static String messages;
        public static Scraper scraper;
        public static int msgLimit;
        public static int sleep;
        public static Dictionary<long,string> userData = new Dictionary<long,string>();

        public TelegramBotConnect(int sleep = 50000,int msgLimit = 5)
        {
            TelegramBotConnect.sleep = sleep;
            TelegramBotConnect.msgLimit = msgLimit;
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
            TelegramBotConnect.scraper = scraper;
            //telegramRequestAsync();
            //messages = scraper.getMsgsText();
            //Trace.WriteLine(messages[7]);
            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            botClient.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token);
            Trace.WriteLine($"Start listening for @{username} ");
        }

        public void stopBotWork()
        {
            cts.Dispose();
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
            Trace.WriteLine("UpdateId: " + updateId.ToString());

            //Trace.WriteLine($"Received a '{update.Message.Text}' message in chat {chatId}.");

            var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { "/start" },
                    new KeyboardButton[] { "/stop" },
                     //Send Data 💻🤖
                })
            { ResizeKeyboard = true };
            while (true)
            {

                var chatUpdates = await botClient.GetUpdatesAsync(offset:updateId);

                foreach (Update chatUpdate in chatUpdates)
                {
                    String? msg = chatUpdate?.Message?.Text;
                    if (msg == "/start")
                    {
                        if(!userData.ContainsKey(chatUpdate.Message.Chat.Id))
                        userData.Add(chatUpdate.Message.Chat.Id, chatUpdate.Message.Chat.Username);
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
                        Trace.WriteLine($"Received by '{item.Value}' in chat {item.Key}.");
                        Trace.Write("\n Mesages : " + messages +"\n");
                        await botClient.SendTextMessageAsync(
                            chatId: item.Key,
                            text: messages,
                            replyMarkup: replyKeyboardMarkup
                            );
                        }


                    //////////////if message array

                    //int n = msgLimit >= messages.Length ? messages.Length : msgLimit;
                    //foreach (var item in userData)
                    //{
                    //    for (int i = 1; i <= n; i++)
                    //    {

                    //        Trace.WriteLine($"Received by '{item.Value}' in chat {item.Key}.");
                    //        Trace.Write("\n Mesages : " + messages[i]);
                    //        await botClient.SendTextMessageAsync(
                    //            chatId: item.Key,
                    //            text: messages[i],
                    //            replyMarkup: replyKeyboardMarkup
                    //        );
                    //        Trace.Write("\n Mesages : " + messages[i]);
                    //        //Thread.Sleep(1000);
                    //    }
                    //}

                    /////////////////refresh method

                    scraper.refreshSite();
                }
                updateId++;
                Thread.Sleep(sleep);
                Trace.Write("/start right now \n");
            }
        }
    }
}
