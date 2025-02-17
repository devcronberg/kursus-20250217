
//Person p = new Person { Navn = "a" };
//string json = System.Text.Json.JsonSerializer.Serialize(p);
//Console.WriteLine(json);

//return;

// Udvikler der benytter terning
Terning terning = new Terning();
terning.RandomDelegate = (min, max) => Random.Shared.Next(1,7);
terning.PrintDelegate = r => Console.WriteLine("***** " + r);
terning.ErSekserEvent += antal => Console.WriteLine("66666 - antal " + antal);

for (int i = 0; i < 10; i++)
{
    terning.Print();
    terning.Ryst();

}

// Udvikler der skaber terning
public class Terning
{

    private int antalSeksere;
    public int Værdi { get; set; }
    public Func<int, int, int> RandomDelegate { get; set; } = null;
    public Action<string> PrintDelegate { get; set; }

    public event Action<int> ErSekserEvent;


    //public int Test;

    //private static Random random = new Random();

    public Terning()
    {
        Ryst();
    }

    public void Ryst()
    {
        if (RandomDelegate == null)
            Værdi = 1;
        else
        {
            Værdi = RandomDelegate.Invoke(1, 7);
            if (Værdi == 6)
            {
                antalSeksere++;
                ErSekserEvent?.Invoke(antalSeksere);

            }
        }
    }

    public void Print()
    {
        PrintDelegate($"Terningens værdi er: {Værdi}");
    }
}


//class Person {
//    //public string Navn;
//    public string Navn { get; set; }
//}