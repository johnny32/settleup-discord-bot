using System;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Configuration;

namespace SettleUpDiscordBot
{
    public class Program
    {
        private static string _botUsername;

        public static async Task Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string discordToken = configuration.GetSection("token").Value;

            _botUsername = configuration.GetSection("botUsername").Value;

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
                await e.Message.RespondAsync($"Hola {e.Message.Author.Username}");
            }
        }
    }
}
