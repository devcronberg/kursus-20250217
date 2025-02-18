//Task t1 = File.WriteAllTextAsync(@"", "");
//await t1;

using System.Threading.Tasks;

//async Task t() {
//    Console.WriteLine();
//}

await File.WriteAllTextAsync(@"", "");

Task t1 = File.WriteAllTextAsync(@"", "");
Task t2 = File.WriteAllTextAsync(@"", "");
Task t3 = File.WriteAllTextAsync(@"", "");
await Task.WhenAll(t1, t2, t3);

Task<string> t = File.ReadAllTextAsync("");
string res = await t;

string res2 = await File.ReadAllTextAsync("");


HttpClient c = new HttpClient();
string a = await c.GetStringAsync("");



