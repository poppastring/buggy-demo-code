using BuggyDemoConsole.Models;



Console.WriteLine("Press 1 or 2 please...");

ConsoleKeyInfo keyReaded = Console.ReadKey();
Console.WriteLine();

switch (keyReaded.Key)
{
    case ConsoleKey.D1: //Number 1 Key
        NullReferenceException();
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
