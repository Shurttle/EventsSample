using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

Tamagocha tamagocha = new Tamagocha { Name = "Паша Михайлюк" };
tamagocha.HungryChanged += Tamagocha_HungryChanged;
tamagocha.ThirstyChanged += Tamagocha_ThirstyChanged;
tamagocha.DirtyChanged += Tamagocha_DirtyChanged;

ConsoleKeyInfo command;
Random random = new Random();
do
{
    command = Console.ReadKey();
    if (command.Key == ConsoleKey.F)
        tamagocha.Feed();
    else if (command.Key == ConsoleKey.I)
        tamagocha.PrintInfo();
    else if (command.Key == ConsoleKey.D)
        tamagocha.Drink();
    else if (command.Key == ConsoleKey.C)
        tamagocha.Clean();
    else if (command.Key == ConsoleKey.P)
    {
        IPresent present;
        int presentType = random.Next(1, 4);
        switch (presentType)
        {
            case 1:
                present = new Toy();
                break;
            case 2:
                present = new Candy();
                break;
            case 3:
                present = new Book();
                break;
            default:
                present = new Toy();
                break;
        }

        int actionType = random.Next(1, 4);
        switch (actionType)
        {
            case 1:
                present.Open();
                break;
            case 2:
                present.Gnaw();
                break;
            case 3:
                present.Smash();
                break;
            default:
                present.Open();
                break;
        }
    }
}
while (command.Key != ConsoleKey.Escape);
tamagocha.Stop();

void Tamagocha_HungryChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 0);
    Console.Write($"{tamagocha.Name} голодает! Показатель голода растет: {tamagocha.Hungry}");
    if (tamagocha.Hungry > 20000)
    {
        tamagocha.IsDead = true;
        Console.SetCursorPosition(0, 14);
        Console.WriteLine($"{tamagocha.Name} умери от голода");
    }

}

void Tamagocha_ThirstyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 1);
    Console.Write($"{tamagocha.Name} хочет пить! Показатель жажды растет: {tamagocha.Thirsty}");
    if (tamagocha.Thirsty > 10000)
    {
        tamagocha.IsDead = true;
        Console.SetCursorPosition(0, 15);
        Console.WriteLine($"{tamagocha.Name} умер от жажды");
    }
}

void Tamagocha_DirtyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 2);
    Console.Write($"{tamagocha.Name} грязный! Показатель грязности растет: {tamagocha.Dirty}");
    if (tamagocha.Dirty > 150000)
    {
        tamagocha.IsDead = true;
        Console.SetCursorPosition(0, 16);
        Console.WriteLine($"{tamagocha.Name} ушел от вас");
    }
}

class Toy : IPresent
{
    public void Open()
    {
        Console.WriteLine("Игрушка открыта!");
    }

    public void Gnaw()
    {
        Console.WriteLine("Игрушка погрызана!");
    }

    public void Smash()
    {
        Console.WriteLine("Игрушка разбита!");
    }
}

class Candy : IPresent
{
    public void Open()
    {
        Console.WriteLine("Конфеты открыты!");
    }

    public void Gnaw()
    {
        Console.WriteLine("Конфеты погрызаны!");
    }

    public void Smash()
    {
        Console.WriteLine("Конфеты разбиты!");
    }
}

class Book : IPresent
{
    public void Open()
    {
        Console.WriteLine("Книга открыта!");
    }

    public void Gnaw()
    {
        Console.WriteLine("Книга погрызана!");
    }

    public void Smash()
    {
        Console.WriteLine("Книга порвана!");
    }
}

class Tamagocha
{
    public string Name { get; set; }
    public int Health { get; set; } = 100;
    public int Hungry
    {
        get => hungry;
        set
        {
            hungry = value;
            HungryChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Dirty
    {
        get => dirty;
        set
        {
            dirty = value;
            DirtyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Thirsty
    {
        get => thirsty; set
        {
            thirsty = value;
            ThirstyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool IsDead { get => isDead; set => isDead = value; }
    public event EventHandler HungryChanged;
    public event EventHandler DirtyChanged;
    public event EventHandler ThirstyChanged;
    public Tamagocha()
    {
        Thread thread = new Thread(LifeCircle);
        thread.Start();
    }
    Random random = new Random();
    private int hungry = 0;
    private int dirty = 0;
    private int thirsty = 0;
    private bool isDead = false;

    private void LifeCircle(object? obj)
    {
        while (!IsDead)
        {
            Thread.Sleep(500);
            int rnd = random.Next(0, 2);
            switch (rnd)
            {
                case 0: JumpMinute(); break;
                case 1: FallSleep(); break;
                case 2: break;
                case 3: break;
                case 4: break;
                case 5: break;
                default: break;
            }

        }

    }

    private void FallSleep()
    {
        WriteMessageToConsole($"{Name} внезапно начинает спать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void JumpMinute()
    {
        WriteMessageToConsole($"{Name} внезапно начинает прыгать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void WriteMessageToConsole(string message)
    {
        Console.SetCursorPosition(0, 10);
        Console.Write(message);
        Console.SetCursorPosition(0, 5); // возвращаем курсор для ввода команды!
    }

    public void PrintInfo()
    {
        Console.SetCursorPosition(0, 8);
        Console.WriteLine($"{Name}: Health:{Health} Hungry:{Hungry} Dirty:{Dirty} Thirsty:{Thirsty} IsDead:{IsDead}");
    }

    public void Stop()
    {
        IsDead = true;
    }

    internal void Feed()
    {
        WriteMessageToConsole($"{Name} внезапно начинает ЖРАТЬ как угорелый.");

        Hungry -= random.Next(5, 10);
    }

    internal void Drink()
    {
        WriteMessageToConsole($"{Name} внезапно начинает ПИТЬ как угорелый.");

        Thirsty -= random.Next(5, 10);
    }

    internal void Clean()
    {
        WriteMessageToConsole($"{Name} внезапно начинает МЫТЬСЯ как угорелый.");

        Dirty -= random.Next(5, 10);
    }

    public void DoSomethingWithPresent(IPresent present)
    {
        present.Open();
        present.Gnaw();
        present.Smash();
    } 
}