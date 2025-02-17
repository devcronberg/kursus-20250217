
//System.Timers.Timer t = new System.Timers.Timer(500);
//t.Enabled = true;
//t.Elapsed += Test;

//Console.ReadLine();

//System.IO.FileSystemWatcher a = new FileSystemWatcher(@"c:\temp");
//a.EnableRaisingEvents = true;
//a.Created += A_Created;
//Console.ReadLine();
//void A_Created(object sender, FileSystemEventArgs e)
//{
//    Console.WriteLine("* " + e.FullPath);
//}






Terning t = new Terning();



public class Terning
{

    public Func<int, int, int> TilfældigDelegate;

    public int Værdi { get; private set; }

    public void Ryst()
    {
        if (TilfældigDelegate == null)
        {
            this.Værdi = 1;
        }
        else
        {
            this.Værdi = TilfældigDelegate(1, 7);
        }

    }


    public Terning()
    {
        this.Ryst();
    }
}