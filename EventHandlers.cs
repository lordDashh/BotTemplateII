using System;
using System.IO;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;

namespace BotTemplate
{
    public static class EventHandlers
    {
        private static Random _rng;

        /// <summary>
        ///   Register the event handlers
        /// </summary>
        public static void Setup(DiscordClient c)
        {
            _rng = new Random();

            c.Ready += (e) =>
            {
                c.DebugLogger.LogMessage(LogLevel.Info, nameof(c), "Ready", DateTime.Now);
                return Task.CompletedTask;
            };

            c.MessageCreated += async (e) =>
            {
                if (e.Message.Content=="&lewdr" && _rng.Next(6)==0)
                    await e.Channel.SendMessageAsync("What a pervert");
            };

            c.GetCommandsNext().CommandErrored += async (e) =>
            {
                var ctx = e.Context;
                var ex = e.Exception;
                switch (ex)
                {
                    case CommandNotFoundException _:
                        break;
                    case UserException _:
                        await ctx.RespondAsync(ex.Message);
                        break;
                    default:
                        string estr=ex.Message.ToString();
                        if (estr.Length<1900)
                        {
                            await ctx.RespondAsync($">>> {estr}");
                        }
                        else
                        {
                            using var stream = new MemoryStream();
                            var writer = new StreamWriter(stream);
                            writer.Write(estr);
                            writer.Flush();
                            stream.Position=0;
                            await ctx.RespondWithFileAsync("error.txt", stream);
                        }

                        break;
                };
            };
        }
    }
}
