using System.Threading.Tasks;

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace BotTemplate.Modules
{
    public class MiscModule : BaseCommandModule
    {
        [Command("ping"), Description("best command in existence")]
        public async Task PingAsync(CommandContext ctx)
            => await ctx.RespondAsync("No.");
    }
}
