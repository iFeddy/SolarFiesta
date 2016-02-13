
using Solar.FiestaLib;
using Solar.FiestaLib.Networking;
using Solar.World.Networking;
using System;

namespace Solar.World.Handlers
{
    public sealed class Handler2
    {
        //this is incorrect, somehow?
        [PacketHandler(CH2Type.Pong)]
        public static void Pong(WorldClient client, Packet packet)
        {
            client.Pong = true;
        }

        public static void SendPing(WorldClient client)
        {
            using (var packet = new Packet(SH2Type.Ping))
            {
                client.SendPacket(packet);
            }
        }

        [PacketHandler(CH2Type.GetServerTime)]
        public static void ServerTimeReq(WorldClient client, Packet packet)
        {
            SendServerTime(client);
        }

        public static void SendServerTime(WorldClient client)
        {
            using (var packet = new Packet(SH2Type.ServerTime))
            {
                packet.WriteByte(Convert.ToByte(DateTime.Now.Hour));
                packet.WriteUShort(Convert.ToUInt16(DateTime.Now.Minute));
                client.SendPacket(packet);
            }
        }
    }
}
