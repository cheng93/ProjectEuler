int Solution(int limit)
{
    var seen = new HashSet<(int N, int Power)>();
    var powers = new Dictionary<int, (int N, int Power)>();
    for (var i = 2; i <= limit; i++)
    {
        var product = i;
        var n = i;
        var power = 1;
        if (powers.TryGetValue(i, out var p))
        {
            (n, power) = p;
        }

        for (var j = 2; j <= limit; j++)
        {
            seen.Add((n, power * j));
            product *= i;
            if (product <= limit && !powers.ContainsKey(product))
            {
                powers[product] = (i, j);
            }
        }
    }

    return seen.Count;
}

Console.WriteLine(Solution(5)); // 15
Console.WriteLine(Solution(100));