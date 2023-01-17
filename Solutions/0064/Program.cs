int Solution(int n)
    => PeriodSquareRoot(n)
        .Count(x => x.Periods.Count() % 2 == 1);

IEnumerable<(int Quotient, ICollection<int> Periods)> PeriodSquareRoot(int n)
{
    for (var i = 2; i <= n; i++)
    {
        var quotient = Math.Sqrt(i);
        if (quotient % 1 == 0)
        {
            continue;
        }

        var seen = new HashSet<(int Numerator, int Remainder)>();
        var periods = new List<int>();
        var numerator = 1;
        var remainder = (int)quotient;

        do
        {
            seen.Add((numerator, remainder));
            var denominator = i - (remainder * remainder);
            (numerator, denominator) = Simplify((numerator, denominator));
            var j = 0;
            while (quotient + remainder > denominator)
            {
                remainder -= denominator;
            }
            j++;
            periods.Add(j);

            numerator = denominator;
            remainder = Math.Abs(remainder);
        } while (!seen.Contains((numerator, remainder)));

        yield return ((int)quotient, periods);
    }
}

(int Numerator, int Denominator) Simplify((int Numerator, int Denominator) fraction)
{
    var (numerator, denominator) = fraction;
    var gcd = Gcd(numerator, denominator);
    return (numerator / gcd, denominator / gcd);
}

int Gcd(int a, int b)
{
    while (b != 0)
    {
        var t = b;
        b = a % b;
        a = t;
    }
    return a;
}

Console.WriteLine(Solution(13)); // 4
Console.WriteLine(Solution(10000));
