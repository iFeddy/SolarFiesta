﻿
using Solar.FiestaLib;
using Solar.FiestaLib.Networking;
using Solar.Util;
using Solar.World.Data;
using Solar.World.Networking;

namespace Solar.World.Handlers
{
    public sealed class Handler29
    {
        [PacketHandler(CH29Type.GuildNameRequest)]
        public static void GuildNameRequest(WorldClient client, Packet packet)
        {
            int id;
            if (!packet.TryReadInt(out id)) {
                Log.WriteLine(LogLevel.Warn, "Failed reading Guild Name Request packet {0}", client.GetCharacterName());
                return;
            }

            var guild = WorldGuild.GetGuild(id);
            if (guild == null)
            {
                SendGuildNameResult(client, id, "");
            }
            else
            {
                SendGuildNameResult(client, id, guild.Name);
            }
        }

        public static void SendGuildNameResult(WorldClient client, int pID, string pName)
        {
            using (var packet = new Packet(SH29Type.GuildNameResult))
            {
                packet.WriteInt(pID);
                packet.WriteString(pName, 16);
                client.SendPacket(packet);
            }
        }
    }
}
