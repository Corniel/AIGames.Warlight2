using AIGames.Warlight2.Game;
using System;
using System.Text.RegularExpressions;

namespace AIGames.Warlight2.Instructions
{
    public class SettingsBotNameInstruction : Instruction
    {
        public static readonly Regex Pattern = new Regex("^settings (?<type>(your|opponent))_bot (?<name>[A-Za-z0-9]+)$", RegexOptions.Compiled);

        /// <summary>Private constructor.</summary>
        private SettingsBotNameInstruction (PlayerType player, BotNameType tp): base(player)
        {
            this.NameType = tp;
        }

        /// <summary>Gets the name  type of the bot.</summary>
        public BotNameType NameType { get; private set; }

        /// <summary>Represents the instruction as System.String.</summary>
        public override string ToString()
        {
            return string.Format("settings {0}_bot {1}", this.NameType, this.PlayerType);
        }

        /// <summary>Parses the instruction.</summary>
        public static SettingsBotNameInstruction Parse(String line)
        {
            var match = Pattern.Match(line ?? String.Empty);
            if (!match.Success)
            {
                throw new ArgumentException("line", "invalid instruction");
            }
            var tp = (BotNameType) Enum.Parse(typeof(BotNameType),match.Groups["type"].Value);
            var name = Player.Parse( match.Groups["name"].Value);

            return new SettingsBotNameInstruction(name, tp);
        }
    }

    public enum BotNameType
    {
        your,
        opponent,
    }
}
