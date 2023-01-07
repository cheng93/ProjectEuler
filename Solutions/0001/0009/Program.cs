int Solution(int sum)
{
    for (var i = 1; i < sum / 3; i++)
    {
        for (var j = i + 1; j <= (sum - i - 1) / 2; j++)
        {
            var k = sum - i - j;
            if (i * i + j * j == k * k)
            {
                return i * j * k;
            }
        }
    }

    throw new Exception();
}

Console.WriteLine(Solution(12)); // 60
Console.WriteLine(Solution(1000));