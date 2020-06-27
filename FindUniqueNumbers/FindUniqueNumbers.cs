using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class FindUniqueNumbers
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[] { 1, 2, 1, 3, 0, 15, 15, 55, 71, 71 };

            IEnumerable<int> duplicates;

            Console.Write("Only unique numbers (quick): ");
            Print(FindUniqueNumbersQuick(numbers, out duplicates));
            Console.Write("Duplicates (quick): ");
            Print(duplicates);
            Console.WriteLine();

            Console.Write("Only unique numbers (standard): ");
            Print(FindUniqueNumbersStandard(numbers, out duplicates));
            Console.Write("Duplicates (standard): ");
            Print(duplicates);
            Console.WriteLine();

            Console.Write("Only unique numbers (LINQ): ");
            Print(FindUniqueNumbersLinq(numbers, out duplicates));
            Console.Write("Duplicates (LINQ): ");
            Print(duplicates);
            Console.WriteLine();

            Console.Write("Distinct values: ");
            Print(GetDistinctNumbers(numbers));

            Console.WriteLine();
            Console.WriteLine("Press any* key.                                                * ..where is it?");
            Console.ReadKey();
        }

        

        // it would be quicker if I would not return duplicates
        private static IEnumerable<int> FindUniqueNumbersQuick(int[] numbers, out IEnumerable<int> duplicates)
        {
            var unique = new List<int>();
            var dups = new List<int>();

            foreach (var n in numbers)
            {
                if (dups.Contains(n))
                    continue;

                if (unique.Contains(n))
                {
                    unique.Remove(n);
                    dups.Add(n);
                    continue;
                }

                unique.Add(n);
            }

            duplicates = dups;
            return unique;
        }

        // I think it would be the slowest way. But maybe it is the fast to develop
        private static IEnumerable<int> FindUniqueNumbersLinq(int[] numbers, out IEnumerable<int> duplicates)
        {
            Dictionary<int, int> counts = numbers
                .GroupBy(n => n)
                .ToDictionary(k => k.Key, v => v.Count());

            var unique = counts
                .Where(c => c.Value == 1)
                .Select(c => c.Key);

            duplicates = counts
                .Where(c => c.Value > 1)
                .Select(c => c.Key);

            return unique;
        }

        private static IEnumerable<int> FindUniqueNumbersStandard(int[] numbers, out IEnumerable<int> duplicates)
        {
            var distinct = numbers.Distinct(); // let's just use LINQ or you can use GetDistinctNumbers (see below)

            var unique = new List<int>();
            var dups = new List<int>();

            foreach (var d in distinct)
            {
                var count = 0;

                foreach (var n in numbers)
                    if (n == d)
                        count++;

                if (count == 1)
                    unique.Add(d);
                else
                    dups.Add(d);
            }

            duplicates = dups;
            return unique;
        }


        private static IEnumerable<int> GetDistinctNumbers(int[] numbers) {
            var distinctList = new List<int>();

            foreach (var n in numbers)
                if (!distinctList.Contains(n))
                    distinctList.Add(n);

            return distinctList;
        }

        private static void Print(IEnumerable<int> collection)
        {
            foreach (var n in collection)
                Console.Write(n + " ");

            Console.WriteLine();
        }

    }
}
