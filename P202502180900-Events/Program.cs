Maskine m = new Maskine();
m.StartEvent += d => Console.WriteLine("Jeg starter - og er sidste startet " + d);
m.StopEvent += (s,e) => Console.WriteLine("Jeg stopper - " + s.ToString() + " " + e.SidsteDato);

m.Start();
m.Stop();
m.Start();


class Maskine
{
    private DateTime sidsteStart;
    public event Action<DateTime> StartEvent;
    public event Action<object, MitEventArg> StopEvent;

    public void Start() {
        // log
        StartEvent?.Invoke(sidsteStart);
        sidsteStart = DateTime.Now;
    }

    public void Stop() {
        // log
        StopEvent?.Invoke(this, new MitEventArg() { SidsteDato = sidsteStart } );
    }

    public override string ToString()
    {
        return "Jeg er en maskine og er sidst startet kl. " + sidsteStart;
    }

}

class MitEventArg : EventArgs {

    public DateTime SidsteDato { get; set; }
}