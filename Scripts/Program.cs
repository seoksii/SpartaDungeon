using System.Data;

internal class Program
{
    private static Character player;

    static void Main(string[] args)
    {
        ItemData.Initialize();
        GameDataSetting();
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

        // 아이템 정보 세팅
    }

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
                break;
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.Atk} (+{player.AtkByItem})");
        Console.WriteLine($"방어력 : {player.Def} (+{player.DefByItem})");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayInventory()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("인벤토리");
        Console.ResetColor();
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine("    아이템 이름          효과                     설명               ");

        for (int i = 0; i < player.Inventory.count; i++)
        {
            Item item = player.Inventory[i];
            Console.WriteLine($"- {(player.Inventory.isEquipped(i) ? "[E]" : "") + item.ItemName, -10} | {(item.ItemType == ItemType.WEAPON ? "공격력" : "방어력") + " + " + item.ItemStat, 20} | {item.Description, -30}");
        }

        Console.WriteLine("\n1. 장착 관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine("\n원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
            case 1:
                DisplayEquipManagement();
                break;
        }
    }

    static void DisplayEquipManagement()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("인벤토리 - 장착 관리");
        Console.ResetColor();
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine("    아이템 이름          효과                     설명               ");

        for (int i = 0; i < player.Inventory.count; i++)
        {
            Item item = player.Inventory[i];
            Console.WriteLine($"- {(i + 1).ToString() + " " + (player.Inventory.isEquipped(i) ? "[E]" : "") + item.ItemName, -10} | {(item.ItemType == ItemType.WEAPON ? "공격력" : "방어력") + " + " + item.ItemStat,20} | {item.Description,-30}");
        }

        Console.WriteLine("\n0. 나가기");
        Console.WriteLine("\n원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, player.Inventory.count);
        switch (input)
        {
            case 0:
                DisplayInventory();
                break;
            default:
                player.EquipItem(input - 1);
                DisplayEquipManagement();
                break;
        }
    }

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}