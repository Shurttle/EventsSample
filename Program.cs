using System;

Tamagocha tamagocha = new Tamagocha { Name = "Варфоломей" };
tamagocha.HungryChanged += Tamagocha_HungryChanged;
tamagocha.ThirstyChanged += Tamagocha_ThirstyChanged;
tamagocha.DirtyChanged += Tamagocha_DirtyChanged;


void Tamagocha_HungryChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 0);
    Console.Write($"{tamagocha.Name} голодает! Показатель голода растет: {tamagocha.Hungry}");
    if (tamagocha.Hungry > 200)
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
    if (tamagocha.Thirsty > 100)
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
    if (tamagocha.Dirty > 150)
    {
        tamagocha.IsDead = true;
        Console.SetCursorPosition(0, 16);
        Console.WriteLine($"{tamagocha.Name} ушел от вас");
    }
}



class Tamagocha
{ 
    public string Name { get; set; }
    public int Health { get; set; } = 100;
    public int Hungry
    {
        get => hungry;
        set { 
            hungry = value;
            HungryChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Dirty 
    { get => dirty; set
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
    public bool IsDead { get; set; } = false;

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

    private void LifeCircle(object? obj)
    {
        while (!IsDead)
        {
            Thread.Sleep(500);
            int rnd = random.Next(0, 2);
            switch(rnd)
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
        Console.SetCursorPosition(0, 10);
        Console.Write($"{Name} внезапно начинает спать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void JumpMinute()
    {
        Console.SetCursorPosition(0, 10);
        Console.Write($"{Name} внезапно начинает прыгать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    public void PrintInfo()
    {
        Console.WriteLine($"{Name}: Health:{Health} Hungry:{Hungry} Dirty:{Dirty} Thirsty:{Thirsty} IsDead:{IsDead}");
    }
}