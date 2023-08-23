using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ItemData
{
    private static DataTable _table = new DataTable();
    public static DataTable Table
    {
        get { return _table; }
        set { _table = value; }
    }

    public static DataRow GetItemDataByItemCode(int itemCode)
    {
        DataRow? dataRow = Table.Rows.Find(itemCode);
        if (dataRow == null) throw new DataMisalignedException();
        return dataRow;
    }

    public static void Initialize()
    {
        InitializeColumn();
        InitializeRow();
    }

    private static void InitializeColumn()
    {
        Table.Columns.Add("Code", typeof(int));
        DataColumn[] key = new DataColumn[1];
        key[0] = Table.Columns[0];
        Table.PrimaryKey = key;
        Table.Columns.Add("Type", typeof(ItemType));
        Table.Columns.Add("Name", typeof(string));
        Table.Columns.Add("Grade", typeof(ItemGrade));
        Table.Columns.Add("Stat", typeof(string));
        Table.Columns.Add("Gold", typeof(int));
        Table.Columns.Add("Description", typeof(string));
    }

    private static void InitializeRow()
    {
        Table.Rows.Add(new object[] { 0001, ItemType.ARMOR, "무쇠갑옷", ItemGrade.COMMON, 5, 300, "무쇠로 만들어져 튼튼한 갑옷입니다." });
        Table.Rows.Add(new object[] { 0002, ItemType.WEAPON, "낡은 검", ItemGrade.COMMON, 2, 400, "쉽게 볼 수 있는 낡은 검입니다." });
        //Table.Rows.Add(new object[] { });
    }

}