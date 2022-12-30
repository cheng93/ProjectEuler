long Solution(int limit)
    => Enumerable.Range(1, limit)
        .Aggregate(1L, (agg, cur) => Lcm(agg, cur));

long Lcm(long a, long b)
    => (a * b) / Gcd(a, b);

long Gcd(long a, long b)
{
    while (b != 0)
    {
        var t = b;
        b = a % b;
        a = t;
    }
    return a;
}

Console.WriteLine(Solution(10)); // 2520
Console.WriteLine(Solution(20));