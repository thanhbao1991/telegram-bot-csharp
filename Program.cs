using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string token = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN") ?? throw new Exception("BOT_TOKEN is missing");
TelegramBotClient botClient = new TelegramBotClient(token);

// Start bot polling
using var cts = new CancellationTokenSource();
var receiverOptions = new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() };

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandleErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

// Minimal API HTTP endpoint để Render thấy app đang chạy
app.MapGet("/", () => "Bot is running!");
app.Run();

static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken token)
{
    if (update?.Message?.Text != null)
    {
        var chatId = update.Message.Chat.Id;
        var text = update.Message.Text;
        Console.WriteLine($"Message from {chatId}: {text}");
        await bot.SendTextMessageAsync(chatId, $"You said: {text}", cancellationToken: token);
    }
}

static Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken token)
{
    Console.WriteLine($"Error: {ex.Message}");
    return Task.CompletedTask;
}