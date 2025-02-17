// Direkte kald
Test1();
Test2("a", 1);
Console.WriteLine(Test3());
Console.WriteLine(Test4(1, true));
Console.WriteLine(Test5(1));


Console.WriteLine();

Action d1 = Test1;
Action<string, int> d2 = Test2;
Func<bool> d3 = Test3;
Func<int, bool, int> d4 = Test4;
Func<int, bool> d5 = Test5;

// Inddirekte
d1();
d2("a", 1);
Console.WriteLine(d3());
Console.WriteLine(d4(1, true));
Console.WriteLine(d5(5));


Console.WriteLine();
// ----------
Action l1 = () =>
{
    Console.WriteLine("I Test1 på linie 1");
    Console.WriteLine("I Test1 på linie 2");
};


Action<string, int> l2 = ((a, b) => Console.WriteLine($"I Test2 med {a} og {b}"));
Func<bool> l3 = () => true;
Func<int, bool, int> l4 = (a, b) => b ? a : a - 1;
//Predicate<int> l5 = (int a) => { return true; };
Predicate<int> l5 = a => true; 


// Inddirekte ...
l1();
l2("*", 1);
Console.WriteLine(l3());
Console.WriteLine(l4(1, true));
Console.WriteLine(l5(56));



void Test1()
{
    Console.WriteLine("I Test1 på linie 1");
    Console.WriteLine("I Test1 på linie 2");
}

void Test2(string a, int b)
{
    Console.WriteLine($"I Test2 med {a} og {b}");
}

bool Test3()
{
    return true;
}

int Test4(int a, bool b)
{
    if (b)
        return a;
    else
        return a - 1;
}



bool Test5(int b)
{
    return true;
}


Person p = new Person { Alder = 30 };
Console.WriteLine(p.BeregnetFødselsår);
p.Skriv();
Console.WriteLine(p.ErÆldre(31));

class Person
{

    // Egenskaber (get/set)
    private int alder;
    public int Alder
    {
        get => alder;
        set => alder = value;
    }

    // Egenskab (get)
    public int BeregnetFødselsår => DateTime.Now.Year - Alder;

    // Metoder
    public void Skriv() => Console.WriteLine("Person er fra " + BeregnetFødselsår);
    public bool ErÆldre(int andenAlder) => alder > andenAlder;

}