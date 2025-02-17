MinDelegate1 a = new MinDelegate1(Console.WriteLine);
a.Invoke();
a();

Action b = Console.WriteLine;
b.Invoke();
b();

MinDelegate2 c = new MinDelegate2(Console.WriteLine);
c.Invoke(1);
c(2);

Action<int> d = Console.WriteLine;
d.Invoke(1);
d(2);

MinDelegate3 e = Test1;
d(e());

Func<int> f = Test1;
d(f());


MinDelegate4 g = Test2;

Console.WriteLine(g("x", 1));

Func<string, int, bool> h = Test2;
Console.WriteLine(h("x", 1));


MinDelegate5 i = Test3;
Console.WriteLine(i(19));

Predicate<long> j = Test3;
Console.WriteLine(j(19));

Func<long, bool> k = Test3;
Console.WriteLine(k(19));

int[] u = { 4, 5, 8, 1, 2 };
//System.Array.FindIndex(

int Test1() {
    return 100;
}

bool Test2(string x, int y)
{
    return true;
}

bool Test3(long y)
{
    return false;
}


public delegate void MinDelegate1();
public delegate void MinDelegate2(int a);

public delegate int MinDelegate3();
public delegate bool MinDelegate4(string a, int b);

public delegate bool MinDelegate5(long a);
