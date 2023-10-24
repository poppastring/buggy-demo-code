using BuggyDemoConsole.Models;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;

const int INDEX_RANGE = 10;
string arg0 = null;
string arg1  = string.Empty;
string arg2 = "32.3";
int[] arg3 = new int[INDEX_RANGE];

Console.WriteLine("Press the");
Console.WriteLine("D1) Null Reference Exception");
Console.WriteLine("D2) Custom Exception???");
Console.WriteLine("D3) Invalid Operation Exception");
Console.WriteLine("D4) System Sql Exception");
Console.WriteLine("D5) Argument Exception");
Console.WriteLine("D6) Exception");
Console.WriteLine("D7) Format Exception");
Console.WriteLine("D8) Argument Null Exception");
Console.WriteLine("D9) Argument Out Of Range Exception");
Console.WriteLine("D0) Index Out Of Range Exception");
Console.WriteLine("NP1) Invalid Cast Exception");
Console.WriteLine("NP2) File Not Found Exception");
Console.WriteLine("NP3) Microsoft Sql Exception");
Console.WriteLine("NP4) IO Exception");
Console.WriteLine("NP5) Http Request Exception");
Console.WriteLine("NP6) Not Supported Exception");


ConsoleKeyInfo keyReaded = Console.ReadKey();
Console.WriteLine();

switch (keyReaded.Key)
{
    case ConsoleKey.D1: // Null Reference Exception
        NotWhatYouThinkItIs();
        WhereIsTheProblem();
        break;
    case ConsoleKey.D2: //CustomException
        MyExceptionIsBetterThanYours();
        break;
    case ConsoleKey.D3: //Invalid Operation Exception
        LoopHolesInLinq();
        // IsItNullOrNothing();
        break;
    case ConsoleKey.D4: //System Sql Exception
        
        break;
    case ConsoleKey.D5: //Argument Exception
        ValidateThisValue(arg1);
        break;
    case ConsoleKey.D6: //Exception

        break;
    case ConsoleKey.D7: //Format Exception
        CanYouConvertThisValue(arg2);
        break;
    case ConsoleKey.D8: //Argument Null Exception
        ValidateThisValue(arg0);
        break;
    case ConsoleKey.D9: //Argument Out Of Range Exception
        ArguingRarelyFixesAnything(arg3);
        break;
    case ConsoleKey.D0: //Index Out Of Range Exception
        CheckTheIndexCards(arg3);
        break;
    case ConsoleKey.NumPad1: //Invalid Cast Exception
        TheCastOfTheShowIncludes(arg2);
        break;
    case ConsoleKey.NumPad2: //File Not Found Exception
        WhereDidIPutThatThing("file.txt");
        break;
    case ConsoleKey.NumPad3: //Microsft SQL Exception

        break;
    case ConsoleKey.NumPad4: //IO Exception

        break;
    case ConsoleKey.NumPad5: //Http Request Exception
        DataDataBricksAndClay();
        break;

    case ConsoleKey.NumPad6: //
        TryingThisCerealItsGreat();
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

}

static void MyExceptionIsBetterThanYours()
{
    var people = new List<string> { "Mark Downie", "Mark Wilson-Thomas", "Andy Sterland", "Filisha Shah" };

    if(!people.Exists(x => x.StartsWith("Harshada")))
    {
        throw new EmployeeNotFoundException("Harshada");
    }   
}

static async void DataDataBricksAndClay()
{
    HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://www.poppastring.com/"),
    };

    using HttpResponseMessage response = await sharedClient.GetAsync(".well-known1/webfinger?resource=acct:poppastring@dotnet.social");
    response.EnsureSuccessStatusCode();

    // var jsonresponse = await response.Content.ReadAsStringAsync();
    //User? userinfo = JsonSerializer.Deserialize<User>(jsonresponse);
    //Console.WriteLine(userinfo.person.firstname);
}

static void LoopHolesInLinq()
{
    var people = new List<string> { "Mark Downie", "Mark Wilson-Thomas", "Andy Sterland", "Filisha Shah" };
    // var mark = people.SingleOrDefault(x => x.StartsWith("Mark"));

    var mark = people.First(x => x.StartsWith("Harshada"));
}

static void IsItNullOrNothing()
{
    int? nullableInt = null;
    int nowInt = (int)nullableInt;
}

static int ValidateThisValue(string thevalue)
{
    if (thevalue == null)
    {
        throw new ArgumentNullException(nameof(thevalue));
    }
    
    if (thevalue.Length == 0)
    {
        throw new ArgumentException("Zero-length string invalid", nameof(thevalue));
    }
    return thevalue.Length;
}


static int CanYouConvertThisValue(string age)
{
    return int.Parse(age);
}

static void ArguingRarelyFixesAnything(int[] myarray)
{
    if(myarray.Length >= INDEX_RANGE) 
    {
        throw new ArgumentOutOfRangeException(nameof(myarray), "Index is out of range.");
    }
}

static void CheckTheIndexCards(int[] myarray)
{
    myarray[INDEX_RANGE] = 1000;
}


static int TheCastOfTheShowIncludes(object val)
{
    return (int)val;
}

static void TryingThisCerealItsGreat()
{
    var resp = new MyDataResponse() { Message = "Some message...", Status = IntPtr.MinValue };
   
    var str = JsonSerializer.Serialize(resp);

    Console.WriteLine(str);
}

static string WhereDidIPutThatThing(string filename)
{
    string content = string.Empty;
    FileInfo file = new FileInfo(filename);
    StreamReader stRead = file.OpenText();
    while (!stRead.EndOfStream)
    {
        content = content + stRead.ReadLine();
    }

    return content;
}

static void NotWhatYouThinkItIs()
{
    Console.Write("Enter a number: ");
    double x = Convert.ToDouble(Console.ReadLine());
    x = (7 * x / 4 + 1 / 2) - (5 * x / 4 + 1 / 2) * Math.Acos(Math.PI * x);
    Console.WriteLine(x);
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

