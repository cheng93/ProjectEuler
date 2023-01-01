int Solution(string str)
{
    var grid = str
        .Split(Environment.NewLine)
        .Select(l => l
            .Split(" ")
            .Select(int.Parse)
            .ToArray())
        .ToArray();

    var cache = new Dictionary<(int X, int Y), int>();

    return Dfs(0, 0);

    int Dfs(int x, int y)
    {
        var number = grid[y][x];
        if (y == grid.Length - 1)
        {
            return number;
        }

        if (cache.TryGetValue((x, y), out var cached))
        {
            return cached;
        }

        var max = Math.Max(Dfs(x, y + 1), Dfs(x + 1, y + 1)) + number;

        cache[(x, y)] = max;
        return max;
    }
}

var small = """"
3
7 4
2 4 6
8 5 9 3
"""";

var triangle = """
75
95 64
17 47 82
18 35 87 10
20 04 82 47 65
19 01 23 75 03 34
88 02 77 73 07 63 67
99 65 04 28 06 16 70 92
41 41 26 56 83 40 80 70 33
41 48 72 33 47 32 37 16 94 29
53 71 44 65 25 43 91 52 97 51 14
70 11 33 28 77 73 17 78 39 68 17 57
91 71 52 38 17 14 91 43 58 50 27 29 48
63 66 04 68 89 53 67 30 73 16 69 87 40 31
04 62 98 27 23 09 70 98 73 93 38 53 60 04 23
""";

Console.WriteLine(Solution(small)); // 23
Console.WriteLine(Solution(triangle));