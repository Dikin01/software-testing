using System.Collections.Concurrent;
using System.Diagnostics;

const int n = 1000000;
var primeNumbers = GetPrimeNumbers(n).ToArray();

var result = GetDecompositions(n);
/*foreach (var numberResult in result)
    Console.WriteLine($"{numberResult.Key}: {string.Join(", ", numberResult.Value)}");*/


IEnumerable<int> GetPrimeNumbers(int limit)
{
    var numbers = Enumerable.Range(2, limit - 2).ToArray();
    var primeNumberLimit = (int)Math.Sqrt(limit) + 1;

    var compositeNumbers = new List<int>();
    foreach (var number in numbers[..primeNumberLimit])
    {
        if (compositeNumbers.Contains(number))
            continue;

        var checkNumber = 0;
        for (var i = 0; checkNumber < numbers[^1]; i++)
        {
            checkNumber = number * (number + i);
            compositeNumbers.Add(checkNumber);
        }
    }

    return numbers.Except(compositeNumbers);
}

Dictionary<int, List<int>> GetDecompositions(int maxNumber)
{
    var accumulator = new ConcurrentDictionary<int, List<int>>();

    var parallelOptions = new ParallelOptions
    {
        MaxDegreeOfParallelism = 1
    };
    
    Parallel.For(1, maxNumber + 1, parallelOptions,i =>
    {
        var numbers = GetDecomposition(i);
        accumulator.TryAdd(i, numbers);
    });

    return accumulator.ToDictionary(item => item.Key,
        item => item.Value);
}

List<int> GetDecomposition(int number)
{
    var result = new HashSet<int>();
    foreach (var primeNumber in primeNumbers)
    {
        if (primeNumber * primeNumber > number)
            break;

        if (number % primeNumber == 0)
            result.Add(primeNumber);
    }

    return result.ToList();
}