using BuggyDemoConsole.Models;


Console.WriteLine("Press the");
Console.WriteLine("1) Crash - Null reference exceptions.");
Console.WriteLine("2) Crash - GC Heap presssure, OOM Exceptions.");

ConsoleKeyInfo keyReaded = Console.ReadKey();
Console.WriteLine();

switch (keyReaded.Key)
{
    case ConsoleKey.D1: 
        NullReferenceException();
        break;

    case ConsoleKey.D2: //Number 1 Key
        OutOfMemoryException();
        break;

    default: //Not known key pressed
        Console.WriteLine("Wrong key, please try again.");
        break;
}

Console.WriteLine("Hit any key to exit");
Console.ReadKey();

static void NullReferenceException()
{
    var f = new Foo();
    var name = f.Bar.Baz.Name;
}


static void OutOfMemoryException()
{
    List<Product> products = new List<Product>();
    string answer = "";
    do
    {
        for (int i = 0; i < 10000; i++)
        {
            products.Add(new Product(i, "product" + i));
        }
        Console.WriteLine("Leak some more? Y/N");
        answer = Console.ReadLine().ToUpper();

    } while (answer == "Y");
}