
using Solar.FiestaLib;
using Solar.FiestaLib.Networking;

namespace Solar.World.Handlers
{
    public sealed class Handler25
    {
        public static Packet CreateWorldMessage(WorldMessageTypes pType, string pMessage)
        {
            var packet = new Packet(SH25Type.WorldMessage);
            packet.WriteByte((byte)pType);
            packet.WriteStringLen(pMessage, true);
            return packet;
        }
    }
}
