using System;

using Solar.FiestaLib.Data;
using Solar.Zone.Data;

namespace Solar.Zone.Game
{
    public class DroppedItem
    {
        public short Amount { get; set; }
        public ushort ItemID { get; protected set; }
        public virtual DateTime? Expires { get; set; }
        public ItemInfo Info { get { return DataProvider.Instance.GetItemInfo(this.ItemID); } }

        public DroppedItem()
        {
        }

        public DroppedItem(Item pBase)
        {
            Amount = pBase.Amount;
            ItemID = pBase.ItemID;
            Expires = pBase.Expires;
        }
    }
}
