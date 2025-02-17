Maskine m = new Maskine();
//m.LogDelegate = new LogDelegate(Console.WriteLine);
m.LogDelegate = new Action<string>(Console.WriteLine);
m.LogDelegate += Test;
m.LogDelegate += Skriv;
m.Start();
m.Slut();


void Test(string a)
{
    Console.WriteLine("******!!!! " + a);
}

void Skriv(string a)
{
    File.AppendAllText(@"c:\temp\data.log", a + "\r\n");
}

//delegate void LogDelegate(string t);

class Maskine
{

    public Action<string> LogDelegate;

    public void Start()
    {
        Log("Start");
    }
    public void Slut()
    {
        Log("Slut");
    }

    public Maskine()
    {
        this.LogDelegate = new Action<string>(Console.WriteLine);
    }


    private void Log(string txt)
    {

        string s = DateTime.Now.ToLongTimeString();
        string l = $"{s} {txt}";
        LogDelegate.Invoke(l);
    }

}