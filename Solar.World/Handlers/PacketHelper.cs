using System;
using Solar.FiestaLib;
using Solar.FiestaLib.Data;
using Solar.FiestaLib.Networking;
using Solar.Util;
using Solar.World.Data;

namespace Solar.World.Handlers
{
    public sealed class PacketHelper
    {
        public static void WriteBasicCharInfo(WorldCharacter wchar, Packet packet)
        {
            packet.WriteInt(wchar.Character.ID);
            packet.WriteString(wchar.Character.Name, 20);
            packet.WriteByte(wchar.Character.CharLevel);
            packet.WriteByte(0);
            packet.WriteByte(wchar.Character.Slot);
            MapInfo mapinfo;
            if (!DataProvider.Instance.Maps.TryGetValue(wchar.Character.Map, out mapinfo))
            {
                Log.WriteLine(LogLevel.Warn, "{0} has an invalid MapID ({1})", wchar.Character.Name, wchar.Character.Map);
                wchar.Character.Map = 0;//we reset
                packet.WriteString("Rou", 13);
            }
            else
            {
                packet.WriteString(mapinfo.ShortName, 13);
            }

            packet.WriteByte(36); // Idk right now
            packet.WriteByte(193); // Idk right now
            packet.WriteByte(18); // Idk right now
            packet.WriteByte(11); // Idk right now

            WriteLook(wchar, packet);
            WriteEquipment(wchar, packet);
            WriteRefinement(wchar, packet);

            packet.WriteString(mapinfo == null ? "Rou" : mapinfo.ShortName, 12); //TODO: load from mapinfo.shn
            packet.WriteInt(0);				// X, doesn't matter
            packet.WriteInt(0);        		// Y, neither

            packet.WriteInt(0x63dd45ca);
            packet.WriteByte(0);
            packet.WriteInt(100);      		// Test later!
            packet.WriteByte(0);
            packet.WriteByte(2);
            packet.WriteByte(0);
            packet.WriteByte(0);
            packet.WriteByte(0);
            packet.WriteByte(0);
            wchar.Detach();
        }

        public static void WriteLook(WorldCharacter wchar, Packet packet)
        {
            packet.WriteByte(Convert.ToByte(0x00001 | (wchar.Character.Job << 2) | (Convert.ToByte(wchar.Character.Male)) << 7));
            packet.WriteByte(wchar.Character.Hair);
            packet.WriteByte(wchar.Character.HairColor);
            packet.WriteByte(wchar.Character.Face);

        }

        public static void WriteEquipment(WorldCharacter wchar, Packet packet)
        {
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.Helm));
            packet.WriteUShort(Settings.Instance.ShowEquips ? wchar.GetEquipBySlot(ItemSlot.Weapon) : (ushort)0xffff);
            packet.WriteUShort(Settings.Instance.ShowEquips ? wchar.GetEquipBySlot(ItemSlot.Weapon2) : (ushort)0xffff);
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.Armor));
            packet.Fill(2, 0xff);
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.Pants));
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.Boots));
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.CostumeBoots));
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.CostumePants));
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.CostumeArmor));
            packet.Fill(6, 0xff);              // UNK
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.Glasses));
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.CostumeHelm));
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.Wing));
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.CostumeWeapon));
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.Tail));
            packet.WriteUShort(wchar.GetEquipBySlot(ItemSlot.Pet));
            packet.Fill(2, 0xff);              // UNK
        }

        public static void WriteRefinement(WorldCharacter wchar, Packet pPacket)
        {
            //TODO: pPacket.WriteByte(Convert.ToByte(this.Inventory.GetEquippedUpgradesByType(ItemType.Weapon) << 4 | this.Inventory.GetEquippedUpgradesByType(ItemType.Shield))); 
            // pPacket.WriteByte(0x00); //this must be the above, but currently not cached
            // pPacket.WriteByte(0xf0);    		// UNK
            // pPacket.WriteByte(0xff);    		// UNK


            pPacket.Fill(2, 0x00);      		// UNK
            pPacket.WriteByte(0xF0);         // UNK
            pPacket.Fill(4, 0xFF);         // UNK
        }
    }
}
