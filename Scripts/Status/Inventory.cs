using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

public class Inventory
{
    /// <summary>
    /// 장착 중인 아이템의 ItemID를 보관합니다.
    /// </summary>
    /// <param name = "ItemType"> [key] 장착한 Item의 종류입니다. </param>
    /// <param name = "int"> [value] 장착한 Item의 ItemID입니다. </param>
    public Dictionary<ItemType, int> ItemOnEquip = new Dictionary<ItemType, int>();

    /// <summary>
    /// 아이템의 ItemID를 보관합니다.
    /// </summary>
    /// <param name = "int"> Item의 ItemID입니다. </param>
    private List<int> _itemList = new List<int>();
    public Item this[int idx]
    {
        get
        {
            return Item.ItemIDContainer[_itemList[idx]];
        }
    }
    public int count
    {
        get { return _itemList.Count; }
    }

    public Inventory()
    {
        for (int i = 1; i < 3; i++)
        {
            _itemList.Add(new Item(i).ItemId);
        }
    }

    public void EquipItem(int idx)
    {
        if (ItemOnEquip.ContainsKey(this[idx].ItemType) && ItemOnEquip[this[idx].ItemType] == this[idx].ItemId)
            UnequipItem(this[idx].ItemType);
        else ItemOnEquip[this[idx].ItemType] = idx;
    }

    public void UnequipItem(ItemType itemType)
    {
        ItemOnEquip.Remove(itemType);
    }

    public bool isEquipped(int idx)
    {
        int ItemID = _itemList[idx];
        return ItemOnEquip.ContainsValue(ItemID);
    }

    public void SortInventory()
    {
        _itemList.Sort((a, b) => {
            Item itemA = Item.ItemIDContainer[a];
            Item itemB = Item.ItemIDContainer[b];

            int itemCodeA = Item.ItemIDContainer[a].ItemCode;
            int itemCodeB = Item.ItemIDContainer[b].ItemCode;

            string itemNameA = ItemData.GetItemDataByItemCode(itemCodeA).ToString();
            string itemNameB = ItemData.GetItemDataByItemCode(itemCodeB).ToString();

            if (String.Compare(itemNameA, itemNameB) < 0) return -1;
            else if (String.Compare(itemNameA, itemNameB) > 0) return 1;
            else
                if (itemCodeA < itemCodeB) return -1;
                else return 1;
        });
    }
}
