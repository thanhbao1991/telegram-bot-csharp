using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

class Program
{
    static string TELEGRAM_TOKEN = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN");
    static TelegramBotClient botClient = new TelegramBotClient(TELEGRAM_TOKEN);

    static async Task Main()
    {
        Console.WriteLine("Bot started...");
        using var cts = new CancellationTokenSource();
        var receiverOptions = new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() };

        botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cts.Token);
        Console.ReadLine();
    }

    static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken token)
    {
        if (update.Message is { } message && message.Text != null)
        {
            await bot.SendTextMessageAsync(message.Chat.Id, "Hello 👋", cancellationToken: token);
        }
    }

    static Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken token)
    {
        Console.WriteLine($"Error: {ex.Message}");
        return Task.CompletedTask;
    }
}