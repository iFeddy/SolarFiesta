
using Solar.FiestaLib;
using Solar.FiestaLib.Networking;
using Solar.Zone.Networking;

namespace Solar.Zone.Handlers
{
    public sealed class Handler3
    {
        public static void SendError(ZoneClient client, ServerError error)
        {
            using (Packet pack = new Packet(SH3Type.Error))
            {
                pack.WriteShort((byte)error);
                client.SendPacket(pack);
            }
        }
    }
}
