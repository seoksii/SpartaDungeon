using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Item
{
    public static IDContainer<Item> ItemIDContainer = new IDContainer<Item>();

    private int _itemId;
    public int ItemId
    {
        get { return _itemId; }
        private set { _itemId = value; }
    }

    private int _itemCode;
    public int ItemCode
    {
        get { return _itemCode; }
        private set { _itemCode = value; }
    }

    private int _basicStat = 0;
    public int BasicStat
    {
        get { return _basicStat; }
        private set { _basicStat = value; }
    }
    private int _reinforcement = 0;
    public int Reinforcement
    {
        get { return _reinforcement; }
        private set { _reinforcement = value; }
    }

    public int TotalStat
    {
        get { return BasicStat + (BasicStat / 5) * Reinforcement; }
    }

    public Item(int itemCode)
    {
        ItemCode = itemCode;

        DataRow itemData = ItemData.GetItemDataByItemCode(itemCode);
        BasicStat = Convert.ToInt32(itemData["Stat"]);

        ItemId = ItemIDContainer.Add(this);
    }

    ~Item()
    {
        ItemIDContainer.Remove(ItemId);
    }

    public ItemType ItemType
    {
        get
        {
            return (ItemType)ItemData.GetItemDataByItemCode(ItemCode)["Type"];
        }
    }
    public string ItemName
    {
        get
        {
            return ItemData.GetItemDataByItemCode(ItemCode)["Name"].ToString();
        }
    }
    public ItemGrade ItemGrade
    {
        get
        {
            return (ItemGrade)ItemData.GetItemDataByItemCode(ItemCode)["Grade"];
        }
    }
    public int ItemStat
    {
        get
        {
            return Convert.ToInt32(ItemData.GetItemDataByItemCode(ItemCode)["Stat"]);
        }
    }
    public int Gold
    {
        get
        {
            return Convert.ToInt32(ItemData.GetItemDataByItemCode(ItemCode)["Gold"]);
        }
    }
    public string Description
    {
        get
        {
            return ItemData.GetItemDataByItemCode(ItemCode)["Description"].ToString();
        }
    }
}

public enum ItemType
{
    NONE,
    WEAPON,
    HELMET,
    ARMOR,
    GLOVE,
}

public enum ItemGrade
{
    NONE,
    COMMON,
    RARE,
    EPIC,
    UNIQUE,
}
