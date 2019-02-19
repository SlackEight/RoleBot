using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleBot
{
    class Program
    {
        DiscordSocketClient client;
        CommandHandler handler;
    
        static void Main(string[] args)

        => new Program().StartAsync().GetAwaiter().GetResult();
            public async Task StartAsync()
            {
            Console.WriteLine("1");
            if (Config.bot.token == "" || Config.bot.token == null) return;
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            Console.WriteLine("2");
            client.Log += Log; ;
            
            await client.LoginAsync(TokenType.Bot, Config.bot.token);
            await client.StartAsync();
            Console.WriteLine("3");
            handler = new CommandHandler();
            await handler.InitializeAsync(client);
            Console.WriteLine("4");
            await Task.Delay(-1);
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }
    }
}
