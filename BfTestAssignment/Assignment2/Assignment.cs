namespace BfTestAssignment.Assignment2;

/*
 * Задание 2. Напишите реализацию метода, возвращающего частичные суммы ряда
 * IEnumerable<double> GetRowSums(IEnumerable<double> row);
 * Например, для ряда
 * 1, 2, 3, 4, ...
 * он должен вернуть
 * 1, 3, 6, 10, ...
 * Возможность переполнения типа double при суммировании можно не учитывать. (Рекомендация:
 * используйте LINQ).
 */

public static class Assignment
{
    public static IEnumerable<double> GetRowSums(IEnumerable<double> row)
    {
        return row.Aggregate(new List<double>(),
            (sums, element) =>
            {
                var nextSum = sums.LastOrDefault(0) + element;
                sums.Add(nextSum);
                return sums;
            });
    }
}