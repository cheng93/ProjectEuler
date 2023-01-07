int Solution()
    => IntegerRightTriangles()
    .GroupBy(x => x)
    .OrderByDescending(x => x.Count())
    .First().Key;

IEnumerable<int> IntegerRightTriangles()
{
    for (var i = 1; i <= 332; i++)
    {
        var pMax = (999 - i) / 2;
        for (var j = i + 1; j <= pMax; j++)
        {
            var h = Math.Sqrt(i * i + j * j);
            var perimeter = i + j + (int)h;
            if (h % 1 == 0 && perimeter <= 1000)
            {
                yield return i + j + (int)h;
            }
        }
    }
}

Console.WriteLine(Solution());