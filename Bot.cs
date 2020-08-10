using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;

using BotTemplate.Objects;

namespace BotTemplate
{
    public class Bot
    {
        private BotConfig _config;
        private CancellationTokenSource _cts;
        private DiscordClient _client;

        private async Task _InitializeAsync()
        {
            using var f = File.OpenRead("./Resources/config.json");
            this._config = await JsonSerializer.DeserializeAsync<BotConfig>(f, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive=true
            });

            this._client = new DiscordClient(new DiscordConfiguration
            {
                Token=this._config.Token,
                TokenType=TokenType.Bot,

                UseInternalLogHandler=true
            });
        }

        private void _PostInitialize()
        {
            this._cts = new CancellationTokenSource();

            Console.CancelKeyPress += (s, e)
                => this._cts.Cancel();

            AppDomain.CurrentDomain.ProcessExit += (s, e)
                => this._cts.Cancel();

            var services = new ServiceCollection()
                .AddSingleton<Bot>(f => this)
                .BuildServiceProvider();

            this._client.UseCommandsNext(new CommandsNextConfiguration
            {
                CaseSensitive=false,
                StringPrefixes=this._config.Prefixes,
                Services=services
            }).RegisterCommands(Assembly.GetExecutingAssembly());

            this._client.UseInteractivity(new InteractivityConfiguration());

            EventHandlers.Setup(this._client);
        }

        public async Task RunAsync()
        {
            await this._InitializeAsync();
            this._PostInitialize();

            try
            {
                await this._client.ConnectAsync();
                await Task.Delay(-1, this._cts.Token);
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
                this._client.DebugLogger.LogMessage(LogLevel.Critical, nameof(Bot), "Exception in main loop", DateTime.Now, ex);
            }

            this._client.DebugLogger.LogMessage(LogLevel.Info, nameof(Bot), "Disconnecting", DateTime.Now);
            await this._client.DisconnectAsync();
        }
    }
}
