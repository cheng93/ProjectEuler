int Solution(int limit)
    => Fibonacci()
        .TakeWhile(x => x <= limit)
        .Where(x => x % 2 == 0)
        .Sum();

IEnumerable<int> Fibonacci()
{
    var a = 1;
    var b = 2;
    yield return a;
    yield return b;
    while (true)
    {
        var sum = a + b;
        yield return sum;
        a = b;
        b = sum;
    }
}

Console.WriteLine(Solution(100)); // 44
Console.WriteLine(Solution(4000000));