

List<int> l = new List<int>() { 5, 1, 51, 7, 1, 56, 36, 5 };

int i = l.FindIndex(Find);
Console.WriteLine(i);

//foreach (var item in l)
//{
//    Console.WriteLine(item);
//}
l.ForEach(Console.WriteLine);

bool Find(int a)
{
    if (a == 56)
        return true;
    return false;
}

//DelegateVoid a = new DelegateVoid(Console.Beep);
DelegateVoid a = Console.Beep;
a += Console.WriteLine;
a += Console.WriteLine;
a += Console.WriteLine;
a = null;


//DelegateStringIntBool b = new DelegateStringIntBool(Test1);
DelegateStringIntBool b = Test1;

bool res = b.Invoke("", 1);


bool Test1(string b, int c)
{
    return false;
}




void Beregn(int a, int b, DelegateIntIntInt f)
{
    Console.WriteLine($"Resultat {f.Invoke(a, b)}");
}

//Beregn(6, 6, new DelegateIntIntInt(LægSammen));
//Beregn(6, 6, LægSammen);
var beregning = FindBeregning();
//Beregn(6, 6, TrækFra);
//Beregn(6, 6, beregning);
Beregn(5, 5, FindBeregning());
//Beregn(6, 6, (a,c) => a+c);

int LægSammen(int a, int b)
{
    return a + b;
}

int TrækFra(int a, int b)
{
    return a - b;
}

DelegateIntIntInt FindBeregning()
{
    if (DateTime.Now.Millisecond % 2 == 0)
        return new DelegateIntIntInt(TrækFra);
    else
        return LægSammen;
}


public delegate void DelegateVoid();
public delegate void DelegateIntVoid(int a);
public delegate int DelegateInt();
public delegate int DelegateIntIntInt(int a, int b);

public delegate bool DelegateStringIntBool(string a, int b);

public delegate bool DelegateString(string a);
