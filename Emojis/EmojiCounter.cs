using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettleUpDiscordBot.Emojis
{
    public class EmojiCounter
    {
        private readonly Dictionary<string, List<Emoji>> _emojis;

        public EmojiCounter()
        {
            _emojis = new Dictionary<string, List<Emoji>>();
        }

        public static IEnumerable<string> ExtractEmojis(string message)
        {
            var emojis = new List<string>();

            string[] parts = message.Split(':');
            if (parts.Length > 2)
            {
                for (var i = 1; i < parts.Length - 1; i++)
                {
                    string part = parts[i];
                    if (!part.Contains(' '))
                    {
                        emojis.Add($":{part}:");
                    }
                }
            }

            return emojis;
        }

        public void AddEmoji(string id, string username, DateTime timestamp)
        {
            var emoji = new Emoji
            {
                Id = id,
                Username = username,
                Timestamp = timestamp
            };

            if (!_emojis.ContainsKey(id))
            {
                _emojis.Add(id, new List<Emoji>());
            }

            _emojis[id].Add(emoji);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (string id in _emojis.Keys)
            {
                sb.AppendLine($"| {id} | {_emojis[id].Count()} |");
            }

            return sb.ToString();
        }
    }
}