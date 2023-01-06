long Solution() => Tph().SkipWhile(x => x <= 40755).First();

IEnumerable<long> Tph()
{
    var i = 1;
    while (true)
    {
        var triangle = Triangle(i);
        if (IsPentagonal(triangle))
        {
            Console.WriteLine(triangle);
            yield return triangle;
        }
        i += 2;
    }
}

long Triangle(long n) => (n * (n + 1)) / 2;

bool IsPentagonal(long n)
    => Math.Sqrt(24 * n + 1) % 6 == 5;

Console.WriteLine(Solution());