using System.Collections.Generic;
using Solar.FiestaLib.Data;

namespace Solar.World.Data
{
    public sealed class ZoneInfo
    {
        public byte ID { get; set; }
        public ushort Port { get; set; }
        public string IP { get; set; }
        public List<MapInfo> Maps { get; set; }
    }
}
