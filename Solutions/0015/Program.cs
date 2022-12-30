
long Solution(int size)
{
    var start = (X: 0, Y: 0);
    var goal = (X: size, Y: size);

    var directions = new (int X, int Y)[]
    {
        (1, 0),
        (0, 1)
    };

    var cache = new Dictionary<(int X, int Y), long>();

    return Dfs(start);

    long Dfs((int X, int Y) point)
    {
        if (point == goal)
        {
            return 1;
        }

        if (cache.TryGetValue(point, out var cached))
        {
            return cached;
        }

        var paths = 0L;

        foreach (var direction in directions)
        {
            var next = (X: point.X + direction.X, Y: point.Y + direction.Y);
            if (next.X <= size && next.Y <= size)
            {
                paths += Dfs(next);
            }
        }

        cache[point] = paths;
        return paths;
    }
}

Console.WriteLine(Solution(2)); // 6;
Console.WriteLine(Solution(20));

