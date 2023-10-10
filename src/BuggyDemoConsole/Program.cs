using BuggyDemoConsole.Models;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;

string arg0 = null;
string arg1  = string.Empty;

Console.WriteLine("Press the");
Console.WriteLine("D1) Null Reference Exception");
Console.WriteLine("D2) Custom Exception???");
Console.WriteLine("D3) Invalid Operation Exception");
Console.WriteLine("D4) System Sql Exception");
Console.WriteLine("D5) Argument Exception");
Console.WriteLine("D6) Exception");
Console.WriteLine("D7) Format Exception");
Console.WriteLine("D8) Argument Exception");
Console.WriteLine("D9) Argument Out Of Range Exception");
Console.WriteLine("D0) Index Out Of Range Exception");
Console.WriteLine("NP1) Invalid Cast Exception");
Console.WriteLine("NP2) File Not Found Exception");
Console.WriteLine("NP3) Microsoft Sql Exception");
Console.WriteLine("NP4) IO Exception");
Console.WriteLine("NP5) Http Request Exception");


ConsoleKeyInfo keyReaded = Console.ReadKey();
Console.WriteLine();

switch (keyReaded.Key)
{
    case ConsoleKey.D1: // Null Reference Exception
        WhereIsTheProblem();
        break;
    case ConsoleKey.D2: //CustomException
        MyExceptionIsBetterThanYours();
        break;
    case ConsoleKey.D3: //Invalid Operation Exception
        LoopHolesInLinq();
        break;
    case ConsoleKey.D4: //System Sql Exception

        break;
    case ConsoleKey.D5: //Argument Exception
        ValidateThisValue(arg1);
        break;
    case ConsoleKey.D6: //Exception

        break;
    case ConsoleKey.D7: //Format Exception

        break;
    case ConsoleKey.D8: //Argument Null Exception
        ValidateThisValue(arg0);
        break;
    case ConsoleKey.D9: //Argument Out Of Range Exception

        break;
    case ConsoleKey.D0: //Index Out Of Range Exception

        break;
    case ConsoleKey.NumPad1: //Invalid Cast Exception

        break;
    case ConsoleKey.NumPad2: //File Not Found Exception

        break;
    case ConsoleKey.NumPad3: //File Not Found Exception

        break;
    case ConsoleKey.NumPad4: //IO Exception

        break;
    case ConsoleKey.NumPad5: //Http Request Exception

        break;
    default: //Not known key pressed
        Console.WriteLine("Wrong key, please try again.");
        break;
}

Console.WriteLine("Hit any key to exit");
Console.ReadKey();

static void WhereIsTheProblem()
{
    var fu = new Foo();
    var name = fu.Bar.Baz.Name;

    //HttpClient sharedClient = new()
    //{
    //    BaseAddress = new Uri("https://www.poppastring.com"),
    //};
    //using HttpResponseMessage response = await sharedClient.GetAsync(".well-known/webfinger?resource=acct:poppastring@dotnet.social");
    //var jsonresponse = await response.Content.ReadAsStringAsync();
    //User? userinfo = JsonSerializer.Deserialize<User>(jsonresponse);
    //Console.WriteLine(userinfo.person.firstname);
}

static void MyExceptionIsBetterThanYours()
{
    var people = new List<string> { "Mark Downie", "Mark Wilson-Thomas", "Andy Sterland", "Filisha Shah" };

    if(!people.Exists(x => x.StartsWith("Harshada")))
    {
        throw new EmployeeNotFoundException("Harshada");
    }   
}

static void LoopHolesInLinq()
{
    var people = new List<string> { "Mark Downie", "Mark Wilson-Thomas", "Andy Sterland", "Filisha Shah" };

    var mark = people.SingleOrDefault(x => x.StartsWith("Mark"));

    // var Shah = people.First(x => x.StartsWith("Shah"));
    // var Mark2 = people.Single(x => x.StartsWith("Mark"));
}

static int ValidateThisValue(string thevalue)
{
    if (thevalue == null)
    {
        throw new ArgumentNullException("thevalue");
    }
    
    if (thevalue.Length == 0)
    {
        throw new ArgumentException("Zero-length string invalid", "thevalue");
    }
    return thevalue.Length;
}


static void NotSupportedException()
{
    var resp = new MyDataResponse() { Message = "Some message...", Status = IntPtr.Parse("1") };

    // 1. We return a json value of the data
    var str = JsonSerializer.Serialize(resp);

    // 2. Copilot: Explain the NotSupportedException
    // 3. Copilot: Give me an JsonExport example that supports IntPtr.Zero
    // 4. Copilot: Show me how to call the JsonExportExample

    Console.WriteLine(str);
}

public class MyDataResponse
{
    public string Message { get; set; }
    public IntPtr Status { get; set; }
}


public class EmployeeNotFoundException : Exception
{
    public EmployeeNotFoundException()
    {
    }

    public EmployeeNotFoundException(string message)
        : base(message)
    {
    }

    public EmployeeNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

