int Solution(int limit)
{
    var sum = Sum(limit);
    var sumOfSquares = SumOfSquares(limit);

    return sum * sum - sumOfSquares;
}

int Sum(int limit) => (limit * (limit + 1)) / 2;

int SumOfSquares(int limit) => (limit * (limit + 1) * (2 * limit + 1)) / 6;


Console.WriteLine(Solution(10)); // 2640
Console.WriteLine(Solution(100));