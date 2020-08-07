﻿using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Discord_Bot
{
    public class ModSample : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [Summary("Kick a user from the server.")]
        [RequireBotPermission(GuildPermission.KickMembers)] // Require the bot to have permission to kick users.
        [RequireUserPermission(GuildPermission.KickMembers)] // Require the user to have permission to kick users.
        public async Task Kick(SocketGuildUser targetUser, [Remainder]string reason = "No reason provided.")
        {
            await targetUser.KickAsync(reason); // Kick the user
            await ReplyAsync($"**{targetUser}** has been kicked. Bye bye :wave:");
        }

        [Command("ban")]
        [Summary("Ban a user from the server")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task Ban(SocketGuildUser targetUser, [Remainder]string reason = "No reason provided.")
        {
            await Context.Guild.AddBanAsync(targetUser.Id, 0, reason);
            await ReplyAsync($"**{targetUser}** has been banned. Bye bye :wave:");
        }

        [Command("unban")]
        [Summary("Unban a user from the server")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Unban(ulong targetUser)
        {
            await Context.Guild.RemoveBanAsync(targetUser);
            await Context.Channel.SendMessageAsync($"The user has been unbanned :clap:");
        }

        [Command("purge")]
        [Summary("Bulk deletes messages in chat")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Purge(int delNumber)
        {
            var channel = Context.Channel as SocketTextChannel;
            var items = await Context.Channel.GetMessagesAsync(delNumber + 1).FlattenAsync();
            await channel.DeleteMessagesAsync(items);
        }

        [Command("reloadconfig")]
        [Summary("Reloads the config and applies changes")]
        [RequireOwner] // Only the bot owner can use this command.
        public async Task ReloadConfig()
        {
            await Functions.SetBotStatus(Context.Client);
            await ReplyAsync("Reloaded!");
        }
    }
}
