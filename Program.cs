using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace SettleUpDiscordBot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = "Nzk1MzI5NTcwNjM0NzI3NDU0.X_HyYg.hdSTyGYMhvSLOEAO_UG_lrAHqBY",
                TokenType = TokenType.Bot
            });

            discordClient.MessageCreated += OnMessageCreated;

            await discordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task OnMessageCreated(MessageCreateEventArgs e)
        {
            await e.Message.RespondAsync($"Hola {e.Message.Author.Username}");
        }
    }
}
