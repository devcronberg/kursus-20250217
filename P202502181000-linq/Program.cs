using Dumpify;

List<int> l = [5, 1, 1, 2, 20];

var res1 = from tal in l where tal < 5 orderby tal select tal;
res1.Dump();


//Func<int, bool> f = new Func<int, bool>(Test);
//Func<int, bool> f = Test;
//var res2 = l.Where(f).OrderBy(i => i);
//var res2 = l.Where(Test).OrderBy(i => i);
//var res2 = l.Where((int i) => { return i < 5; }).OrderBy(i => i);
//var res2 = l.Where(i => i < 5).OrderBy(i => Random.Shared.Next(1,100));
//var res2 = l.Where(i => i < 5).OrderBy(i => i);
//var res2 = l.Where(i => i < 5);
//var res3 = res2.OrderBy(i => i);
var res2 = l.Where(i => i < 5).OrderBy(i => i).ToList();
res2.Dump();


// Skab en liste af filer i en mappe vi kan lege med
var mappe = new System.IO.DirectoryInfo(@"c:\git");
var filer = mappe.GetFiles("*.*", System.IO.SearchOption.AllDirectories);


var res3 = filer.Where(f => f.Length>100000 && DateTime.Now.DayOfWeek== DayOfWeek.Tuesday).ToList();
foreach (var item in res3)
{
    Console.WriteLine(item.FullName);
}

var res4 = filer.Where(f => f.Length > 100000).OrderBy(i => i.Length).ThenBy(i=>i.Name).ToList();
foreach (var item in res3)
{
    Console.WriteLine(item.FullName);
}


var res5 = filer.GroupBy(i => i.Extension);
foreach (var gruppe in res5)
{
    Console.WriteLine(gruppe.Key);
    foreach (var item in gruppe)
    {
        Console.WriteLine("\t" + item.Name);
    }
}

//var res6 = filer.Select(i => i.Length).ToList();
//var res6 = filer.Select(i => new MinKlasse { Navn = i.Name, Længde = i.Length }).ToList();
//string json = System.Text.Json.JsonSerializer.Serialize(res6);
//var res6 = filer.Select(i => (i.Name, i.Length)).ToList();
var res6 = filer.Select(i => new { i.Name, i.Length } ).ToList();




var res7 = filer.Skip(100).Take(10).ToList();
Console.WriteLine();
//bool Test(int i) {
//    return i < 5;
//}

class MinKlasse {
    public string? Navn { get; set; }
    public long Længde { get; set; }
}