int Solution(int limit)
    => Enumerable
        .Range(0, limit)
        .Where(x => x % 3 == 0 || x % 5 == 0)
        .Sum();

Console.WriteLine(Solution(10)); // 23
Console.WriteLine(Solution(1000));