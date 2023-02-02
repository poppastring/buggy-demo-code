using BuggyDemoConsole.Models;
using System.Net.Http;
using System.Text.Json;

Console.WriteLine("Press the");
Console.WriteLine("1) Crash - Null reference exceptions.");
Console.WriteLine("2) Crash - GC Heap presssure, OOM Exceptions.");
Console.WriteLine("3) Crash - Inhandled Exception. Call Stack.");
Console.WriteLine("4) Crash - Null reference exceptions.");

ConsoleKeyInfo keyReaded = Console.ReadKey();
Console.WriteLine();

switch (keyReaded.Key)
{
    case ConsoleKey.D1: 
        NullReferenceException();
        break;

    case ConsoleKey.D2:
        OutOfMemoryException();
        break;

    case ConsoleKey.D3:
        await A();
        break;

    case ConsoleKey.D4:
        await NullReferenceException2();
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

static async Task NullReferenceException2()
{

    HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://www.poppastring.com"),
    };
  
    using HttpResponseMessage response = await sharedClient.GetAsync(".well-known/webfinger?resource=acct:poppastring@dotnet.social");

    var jsonresponse = await response.Content.ReadAsStringAsync();

    User? userinfo = JsonSerializer.Deserialize<User>(jsonresponse);

    Console.WriteLine(userinfo?.person?.firstname);
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

static async Task A()
{
    await B();
}

static async Task B()
{
    await C();
}

static async Task C()
{
    await Task.Delay(3000);
    throw new Exception();
}
