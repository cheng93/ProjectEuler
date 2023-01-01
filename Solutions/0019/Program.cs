int Solution() => Sundays()
    .Skip(1)
    .Take(100)
    .Sum();

IEnumerable<int> Sundays()
{
    var offset = 0;
    var year = 1900;
    while (true)
    {
        var sum = 0;
        foreach (var days in MonthDays(year))
        {
            if (offset == 6)
            {
                sum++;
            }
            offset += days;
            offset %= 7;
        }

        yield return sum;
        year++;
    }
}

IEnumerable<int> MonthDays(int year)
{
    var thirty = new HashSet<int>() { 4, 6, 9, 11 };
    for (var i = 1; i <= 12; i++)
    {
        if (thirty.Contains(i))
        {
            yield return 30;
        }
        else if (i == 2)
        {
            if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0))
            {
                yield return 29;
            }
            else
            {
                yield return 28;
            }
        }
        else
        {
            yield return 31;
        }
    }
}

Console.WriteLine(Solution());