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

        // Kiểm tra token có được truyền đúng không
        if (string.IsNullOrEmpty(TELEGRAM_TOKEN))
        {
            Console.WriteLine("❌ TELEGRAM_TOKEN is null or empty. Check environment variable.");
        }
        else
        {
            Console.WriteLine($"✅ TELEGRAM_TOKEN = {TELEGRAM_TOKEN}");
        }

        try
        {
            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // Nhận tất cả các loại update
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandleErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            Console.WriteLine("Bot is listening. Press Enter to exit.");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ EXCEPTION:");
            Console.WriteLine(ex.ToString());
            Console.ReadLine();
        }
    }

    static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken token)
    {
        Console.WriteLine("🟟 Received an update");

        if (update == null || update.Message == null)
        {
            Console.WriteLine("⚠️ Update or Message is null");
            return;
        }

        var chatId = update.Message.Chat.Id;
        var text = update.Message.Text;

        Console.WriteLine($"🟟 From: {chatId} - Text: {text}");

        await bot.SendTextMessageAsync(
            chatId: chatId,
            text: "🟟 Hello from Render!",
            cancellationToken: token
        );
    }

    static Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken token)
    {
        Console.WriteLine($"❌ Error: {ex.Message}");
        return Task.CompletedTask;
    }
}