using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Item
{
    public static CodeInstanceContainer<Item> ItemCodeInstanceContainer = new CodeInstanceContainer<Item>();
    
    private int _itemInstanceCode;
    public int ItemInstanceCode
    {
        get { return _itemInstanceCode; }
        private set { _itemInstanceCode = value; }
    }

    public Item()
    {
        ItemInstanceCode = ItemCodeInstanceContainer.Add(this);
    }

    ~Item()
    {
        ItemCodeInstanceContainer.Remove(ItemInstanceCode);
    }
}




public enum ItemType
{
    NONE,
    WEAPON,
    HELMET,
    
}
