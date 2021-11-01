using BuggyDemoConsole.Models;


Console.ReadKey();


var f = new Foo();
var name = f.Bar.Baz.Name;

Console.WriteLine("Hit any key to exit");
Console.ReadKey();