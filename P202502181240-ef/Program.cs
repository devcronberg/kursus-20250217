using Dumpify;
using Spectre.Console;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Reflection;

// Nødvendig i de sidste nye versioner af `Microsoft.EntityFrameworkCore.Sqlite` (muligvis en fejl der bliver rettet)
SQLitePCL.Batteries.Init();

string? menu = "";
int menuIndex = -1;

while (menu != "Afslut")
{
    Console.Clear();

    var choices = new[] {
        "Hent personer",
        "Find alle over 180 sorteret efter efternavn",
        "Find person (med land) udfra id",
        "Ret person med id = 1",
        "Opret ny person",
        "Slet person",
        "Benyt transaktion",
        "Hent personer asynkront",
        "Find land med personer",
        "Afslut"
    };

    menu = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("EF DEMO")
            .PageSize(20)
            .AddChoices(choices)
    );


    menuIndex = Array.IndexOf(choices, menu);
    switch (menuIndex)
    {
        case 0:
            using (PeopleContext context = new PeopleContext())
            {
                int antal = AnsiConsole.Ask<int>("Hvor mange personer skal hentes: ", 5);
                var persons = context.People.Take(antal).ToList();
                persons.Dump();
                break;
            }
        case 1:
            using (PeopleContext context = new PeopleContext())
            {
                var persons = context.People.Where(i => i.Height > 180).OrderBy(i => i.LastName).ToList();
                persons.Dump();
                break;
            }
        case 2:
            using (PeopleContext context = new PeopleContext())
            {

                int id = AnsiConsole.Ask<int>("Indtast id: ");
                var person = context.People.Include(i => i.Country).FirstOrDefault(i => i.PersonId == id);
                person.Dump();
                break;
            }
        case 3:
            using (PeopleContext context = new PeopleContext())
            {

                var person = context.People.FirstOrDefault(i => i.PersonId == 1);
                if (person != null)
                {
                    person.FirstName = AnsiConsole.Ask<string>("Fornavn: ");
                    person.LastName = AnsiConsole.Ask<string>("Efternavn: ");
                    context.SaveChanges();
                }
                AnsiConsole.WriteLine("Person rettet");
                break;
            }
        case 4:
            using (PeopleContext context = new PeopleContext())
            {
                var countries = context.Countries.ToList();
                var selectedCountry = AnsiConsole.Prompt(
                    new SelectionPrompt<dynamic>()
                        .Title("Vælg et land")
                        .PageSize(20)
                        .AddChoices(countries)
                        .UseConverter(p => p.Name)
                );
                var person = new Person
                {
                    FirstName = AnsiConsole.Ask<string>("Fornavn: ", "Anders"),
                    LastName = AnsiConsole.Ask<string>("Fornavn: ", "And"),
                    DateOfBirth = new DateTime(2000, 1, 1),
                    Gender = GenderType.Male,
                    IsHealthy = true,
                    CountryId = selectedCountry.CountryId,
                };
                context.People.Add(person);
                context.SaveChanges();
                AnsiConsole.WriteLine("Person opretter med id " + person.PersonId);
                break;
            }
        case 5:

            using (PeopleContext context = new PeopleContext())
            {
                var id = AnsiConsole.Ask<int>("Indtast id: ");
                var person = context.People.FirstOrDefault(i => i.PersonId == id);
                if (person != null)
                {
                    context.People.Remove(person);
                    context.SaveChanges();
                    AnsiConsole.WriteLine("Person slettet");
                }
                else
                {
                    AnsiConsole.WriteLine("Person ikke fundet");
                }

                break;
            }
        case 6:
            using (PeopleContext context = new PeopleContext())
            {

                string efternavn = Guid.NewGuid().ToString().Substring(0, 5);
                AnsiConsole.WriteLine("Forsøger at oprette en ny person med efternavn " + efternavn);
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = new Person
                        {
                            LastName = efternavn,
                            CountryId = 1,
                        };
                        context.People.Add(p);
                        context.SaveChanges();
                        bool fejl = AnsiConsole.Ask<bool>("Smid en fejl?: ", true);
                        if (!fejl)
                            transaction.Commit();
                        else
                            throw new Exception("Fejl");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        AnsiConsole.WriteLine("Fejl - transaktion rullet tilbage");
                    }
                }
                break;
            }
        case 7:
            using (PeopleContext context = new PeopleContext())
            {
                int antal = AnsiConsole.Ask<int>("Hvor mange personer skal hentes: ", 5);
                var persons = await context.People.Take(antal).ToListAsync();
                persons.Dump();
                break;
            }
        case 8:
            using (PeopleContext context = new PeopleContext())
            {
                var countries = context.Countries.ToList();
                var selectedCountry = AnsiConsole.Prompt(
                    new SelectionPrompt<dynamic>()
                        .Title("Vælg et land")
                        .PageSize(20)
                        .AddChoices(countries)
                        .UseConverter(p => p.Name)
                );
                int id = selectedCountry.CountryId;
                var countriesPeople = context.Countries.Include(i => i.People).Where(i => i.CountryId == id).ToList();
                countriesPeople.Dump();
                break;
            }
    }
    if (menu != "Afslut" && !AnsiConsole.Confirm("\r\nFortsæt?", true))
        break;
}

public enum GenderType
{
    Male,
    Female
}

public class Person
{
    public int PersonId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsHealthy { get; set; }
    public GenderType Gender { get; set; }
    public int Height { get; set; }
    public int CountryId { get; set; }
    public Country? Country { get; set; }

}


public class Country
{
    public int CountryId { get; set; }
    public string? Name { get; set; }
    public List<Person>? People { get; set; }
}

public class PeopleContext : DbContext
{
    private readonly string pathToDb;

    public DbSet<Person> People { get; set; }
    public DbSet<Country> Countries { get; set; }

    public PeopleContext(string pathToDb = @"c:\temp\people.db")
    {
        this.pathToDb = pathToDb;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=" + pathToDb);
        optionsBuilder.LogTo(a => System.IO.File.AppendAllText(@"c:\temp\efdemo.log", a));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(e =>
        {
            e.ToTable("Person");
            // Binder enum til en int i databasen
            e.Property(i => i.Gender).HasConversion(x => x.ToString(), x => (GenderType)Enum.Parse(typeof(GenderType), x));
            // Hvis man ønsker at have en liste af personer fra Country
            e.HasOne(p => p.Country).WithMany(b => b.People).HasForeignKey(p => p.CountryId);
        });

        modelBuilder.Entity<Country>(e =>
        {
            e.ToTable("Country");
        });

        base.OnModelCreating(modelBuilder);
    }
}