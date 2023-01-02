int Solution(int limit)
    => Spiral()
        .TakeWhile(x => x <= limit * limit)
        .Sum();

IEnumerable<int> Spiral()
{
    yield return 1;
    var n = 1;
    var factor = 2;
    while (true)
    {
        for (var i = 1; i <= 4; i++)
        {
            n += factor;
            yield return n;
        }
        factor += 2;
    }
}

Console.WriteLine(Solution(5)); // 101
Console.WriteLine(Solution(1001));