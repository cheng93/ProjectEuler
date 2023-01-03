int Solution(int n)
{
    var cache = new Dictionary<(int Current, int Largest), int>();
    var coins = new[] { 1, 2, 5, 10, 20, 50, 100, 200 };

    return Dfs(0, 200);

    int Dfs(int current, int largest)
    {
        if (current == n)
        {
            return 1;
        }

        if (cache.TryGetValue((current, largest), out var cached))
        {
            return cached;
        }

        var count = 0;
        foreach (var coin in coins)
        {
            if (coin > largest)
            {
                continue;
            }
            var next = current + coin;
            if (next > n)
            {
                continue;
            }
            count += Dfs(next, coin);
        }

        cache[(current, largest)] = count;
        return count;
    }
}

Console.WriteLine(Solution(20)); // 41;
Console.WriteLine(Solution(200));