int Solution(int limit) => Enumerable
    .Range(1, limit - 1)
    .MaxBy(RecurringCycle);

int RecurringCycle(int number)
{
    var remainder = 1;
    var seen = new Dictionary<int, int>();
    var i = 0;
    while (remainder != 0)
    {
        while (remainder / number == 0)
        {
            remainder *= 10;
        }

        if (seen.TryGetValue(remainder, out var index))
        {
            return i - index;
        }

        seen[remainder] = i;
        remainder %= number;
        i++;
    }

    return 0;
}

Console.WriteLine(Solution(10)); // 7
Console.WriteLine(Solution(1000));