using System;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Configuration;
using SettleUpDiscordBot.Emojis;

namespace SettleUpDiscordBot
{
    public class Program
    {
        private static string _botUsername;
        private static EmojiCounter _emojiCounter;

        public static async Task Main(string[] args)
        {
            _emojiCounter = new EmojiCounter();

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string discordToken = configuration.GetSection("AppSettings").GetSection("Token").Value;

            _botUsername = configuration.GetSection("AppSettings").GetSection("BotUsername").Value;

            var discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = discordToken,
                TokenType = TokenType.Bot
            });

            discordClient.MessageCreated += OnMessageCreated;

            await discordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task OnMessageCreated(MessageCreateEventArgs e)
        {
            if (e.Message.Author.Username != _botUsername)
            {
                var possibleEmojis = EmojiCounter.ExtractEmojis(e.Message.Content);
                foreach (string emoji in possibleEmojis)
                {
                    _emojiCounter.AddEmoji(emoji, e.Message.Author.Username, e.Message.Timestamp.UtcDateTime);
                }

                if (e.Message.Content == "!emojicount")
                {
                    await e.Message.RespondAsync(_emojiCounter.ToString());
                }
                //await e.Message.RespondAsync($"Hola {e.Message.Author.Username}");
            }
        }
    }
}
