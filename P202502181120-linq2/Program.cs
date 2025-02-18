using Dumpify;

int[] aa = [9, 52, 1, 4];
aa.Dump();

DateTime a = DateTime.Now;
Console.WriteLine(a.IsWeekend());

//Console.WriteLine(MineDateTimeMetoder.IsWeekend(a));

static class MineDateTimeMetoder {

    public static bool IsWeekend(this DateTime dato) {
        return dato.DayOfWeek == DayOfWeek.Sunday || dato.DayOfWeek == DayOfWeek.Saturday;
    }
}