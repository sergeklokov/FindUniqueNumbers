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
            int[] numbers = new int[] { 1, 2, 1, 3, 0, 15, 15 };


            Console.Write("Distinct values: ");
            Print(GetDistinctNumbers(numbers));

            Console.Write("Only unique numbers (standard): ");
            Print(FindUniqueNumbersStandard(numbers));

            Console.Write("Only unique numbers (LINQ): ");
            Print(FindUniqueNumbersLinq(numbers));


            Console.ReadKey();
        }

        private static void Print(IEnumerable<int> collection) {
            foreach (var n in collection)
                Console.Write(n + " ");

            Console.WriteLine();
        }

        private static IEnumerable<int> FindUniqueNumbersLinq(int[] numbers)
        {

            Dictionary<int, int> counts = numbers
                .GroupBy(n => n)
                .ToDictionary(k => k.Key, v => v.Count());


            var unique = counts
                .Where(c => c.Value == 1)
                .Select(c => c.Key);

            return unique;
        }

        private static IEnumerable<int> FindUniqueNumbersStandard(int[] numbers)
        {
            var distinct = numbers.Distinct(); // let's just use LINQ

            var unique = new List<int>();

            foreach (var d in distinct)
            {
                var count = 0;
                foreach (var n in numbers)
                {
                    if (n == d)
                        count++;
                }

                if (count == 1)
                    unique.Add(d);
            }

            return unique;
        }


        private static IEnumerable<int> GetDistinctNumbers(int[] numbers) {
            var distinctList = new List<int>();

            foreach (var n in numbers)
                if (!distinctList.Contains(n))
                    distinctList.Add(n);

            return distinctList;

        }


    }
}
