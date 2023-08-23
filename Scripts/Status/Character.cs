using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Character
{
    public Inventory Inventory { get; set; }
    public string Name { get; set; }
    public string Job { get; set; }
    public int Level { get; set; }
    public int Atk { get; set; }
    public int AtkByItem { get; set; }
    public int Def { get; set; }
    public int DefByItem { get; set; }
    public int Hp { get; set; }
    public int Gold { get; set; }

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Inventory = new Inventory();
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
        AtkByItem = 0;
        DefByItem = 0;
    }

    public void EquipItem(int idx)
    {
        Inventory.EquipItem(idx);
        
        if (Inventory.ItemOnEquip.ContainsKey(ItemType.WEAPON))
            AtkByItem = Inventory[Inventory.ItemOnEquip[ItemType.WEAPON]].ItemStat;
        DefByItem = 0;
        if (Inventory.ItemOnEquip.ContainsKey(ItemType.ARMOR))
            DefByItem += Inventory[Inventory.ItemOnEquip[ItemType.ARMOR]].ItemStat;
        if (Inventory.ItemOnEquip.ContainsKey(ItemType.HELMET))
            DefByItem += Inventory[Inventory.ItemOnEquip[ItemType.HELMET]].ItemStat;
        if (Inventory.ItemOnEquip.ContainsKey(ItemType.GLOVE))
            DefByItem += Inventory[Inventory.ItemOnEquip[ItemType.GLOVE]].ItemStat;
    }
}